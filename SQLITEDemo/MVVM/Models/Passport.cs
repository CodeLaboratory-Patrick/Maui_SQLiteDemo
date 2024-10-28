using SQLITEDemo.Abstractions;

namespace SQLITEDemo.MVVM.Models;

public class Passport : TableData
{
    public DateTime ExpirationDate { get; set; }
}