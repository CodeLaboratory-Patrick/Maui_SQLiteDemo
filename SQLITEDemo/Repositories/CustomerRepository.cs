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

    public void AddOrUpdate(Customer customer)
    {
        int result = 0;
        try
        {
            if (customer.ID != 0)
            {
                result = connection.Update(customer);
                StatusMessage = $"{result} row(s) created"; 
            }
            else
            {
                result = connection.Insert(customer);
                StatusMessage = $"{result} row(s) created"; 
            }
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

    public void Delete(int customerID)
    {
        try
        {
            var customer = Get(customerID);
            connection.Delete(customer);
        }
        catch (Exception e)
        {
            StatusMessage = $"Error : {e.Message}";
        }
    }
}