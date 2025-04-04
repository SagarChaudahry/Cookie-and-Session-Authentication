using System.ComponentModel.DataAnnotations;

namespace DbFirstCRUD.Data.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName  { get; set; }=string.Empty;
        public string Password { get; set; }= string.Empty;
    }
}
