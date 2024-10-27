using SQLite;
using SQLITEDemo.MVVM.Models;

namespace SQLITEDemo.Repositories;

public class CustomerRepository
{
    SQLiteConnection connection { get; set; }

    public CustomerRepository()
    {
        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        connection.CreateTable<Customer>();
    }
}