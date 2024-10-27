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

    public List<Customer> GetAll()
    {
        try
        {
            return connection.Table<Customer>().ToList();
        }
        catch (Exception e)
        {
            StatusMessage = $"Error : {e.Message}";
        }

        return null;
    }

    public Customer Get(int id)
    {
        try
        {
            return connection.Table<Customer>()
                .FirstOrDefault(x => x.ID == id);
        }
        catch (Exception e)
        {
            StatusMessage = $"Error : {e.Message}";
        }
        return null;
    }
    
    public List<Customer> GetAllTheSecondVersion()
    {
        try
        {
            return connection.Query<Customer>("SELECT * FROM Customers").ToList();
        }
        catch (Exception e)
        {
            StatusMessage = $"Error : {e.Message}";
        }

        return null;
    }
}