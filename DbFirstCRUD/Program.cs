using Dapper;
using DbFirstCRUD;
using DbFirstCRUD.CustomJwtFilter;
using DbFirstCRUD.Data.Entities;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Rotativa.AspNetCore;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IJwtAuthenticationRepository, JwtAuthenticationRepository>();

builder.Services.AddScoped<JwtAuthorizeFilter>();

// Register IDbConnection with SqlConnection
builder.Services.AddScoped<IDbConnection>(serviceProvider =>
{
    // Get the connection string from appsettings.json
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});


//Register authentication
//Cookies
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Authentication/Login";
//        options.LogoutPath = "/Authentication/Logout";
//        options.AccessDeniedPath = "/Authentication/AccessDenied";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//        options.Cookie.HttpOnly = true;
//    });

//Session
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(5);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});


//JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["jwt setting:SecretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Extract JWT from Cookie instead of Authorization header
                var token = context.Request.Cookies["AuthToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuer = builder.Configuration["jwt setting:Issuer"],
            ValidAudience = builder.Configuration["jwt setting:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });


//Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});


//builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<ApplicationDbContext>();

// Register repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Middleware
//app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//string wwwroot = app.Environment.WebRootPath;
//Rotativa.AspNetCore.RotativaConfiguration.Setup(wwwroot, "Rotativa");

RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");


app.Run();
