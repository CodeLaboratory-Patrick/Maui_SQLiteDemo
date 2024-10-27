using SQLite;
using SQLITEDemo.MVVM.Models;

namespace SQLITEDemo.Repositories;

public class CustomerRepository
{
    SQLiteConnection connection { get; set; }
    public string StatusMessage { get; set; }

    public CustomerRepository()
    {
        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        connection.CreateTable<Customer>();
    }

    public void Add(Customer newCustomer)
    {
        int result = 0;
        try
        {
            result = connection.Insert(newCustomer);
            StatusMessage = $"{result} row(s) created"; 
        }
        catch (Exception e)
        {
            StatusMessage = $"Error : {e.Message}";
        }
    }
}