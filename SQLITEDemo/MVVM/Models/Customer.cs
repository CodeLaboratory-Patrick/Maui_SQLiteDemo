using SQLite;
using SQLITEDemo.Abstractions;
using SQLiteNetExtensions.Attributes;

namespace SQLITEDemo.MVVM.Models;

[System.ComponentModel.DataAnnotations.Schema.Table("Customers")]
public class Customer : TableData
{
    [System.ComponentModel.DataAnnotations.Schema.Column("name"), Indexed, NotNull]
    public string Name { get; set; }
    [Unique]
    public string Phone { get; set; }
    public int Age { get; set; }
    [MaxLength(100)]
    public string Address { get; set; }
    
    [Ignore] 
    public bool IsYoung =>
        Age > 50 ? true : false;
    
    [ManyToMany(typeof(Passport), CascadeOperations = CascadeOperation.All)]
    public List<Passport> Passport { get; set; }
}