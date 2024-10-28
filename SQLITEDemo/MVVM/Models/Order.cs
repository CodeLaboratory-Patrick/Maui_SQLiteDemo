using SQLITEDemo.Abstractions;

namespace SQLITEDemo.MVVM.Models;

public class Order : TableData
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
}