public class DepProfile
{
    public string AccName { get; set; } = string.Empty;
    public string? AccNo { get; set; }
    public string? CustomerID { get; set; }
    public string? RegisteredDate { get; set; }
    public string? Duration { get; set; }
    public string? MaturedDate { get; set; }
    public string? Interest { get; set; }
    public string? SchemeName { get; set; }
    public string? ProductName { get; set; }
}

public class DepTransaction
{
    public string TNO { get; set; } = string.Empty; 
    public string TDATE { get; set; } = string.Empty; 
    public string DESCRIPTION { get; set; } = string.Empty; 
    public decimal DEBIT { get; set; }
    public decimal CREDIT { get; set; }
    public decimal BALANCE { get; set; }
}

public class DepFooter
{
    public decimal intcapt { get; set; } //interest capitalize
    public decimal inttax { get; set; }
}

public class DepositStatementData
{
    public DepProfile? DepProfile { get; set; }
    public List<DepTransaction> ?DepTable { get; set; }//list of transactions
    public DepFooter? DepFooter { get; set; }
}

public class DateRange
{
    public string? FROMDATE { get; set; }  //start date for the report 
    public string? TODATE { get; set; } //End date for the report
}
