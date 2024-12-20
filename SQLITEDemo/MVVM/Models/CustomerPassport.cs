using SQLiteNetExtensions.Attributes;

namespace SQLITEDemo.MVVM.Models;

public class CustomerPassport
{
    [ForeignKey(typeof(Customer))]
    public int CustomerId { get; set; }
    
    [ForeignKey(typeof(Passport))]
    public int PassportId { get; set; }
}