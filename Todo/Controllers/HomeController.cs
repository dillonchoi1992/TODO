using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using Todo.Models;

namespace Todo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public void Insert(TodoItem todo)
    {
        var cnstr = "Data Source=db.sqlite";
        var connection = new SqliteConnection(cnstr);
        if(connection != null && connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }
        var cmd = new SqliteCommand();
        cmd.Connection = connection;
        cmd.CommandText = $"INSERT INTO todo (name) VALUES ('{todo.Name}')";
        cmd.ExecuteNonQuery();

        if(connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
        }

        cmd = null;
        connection = null;
        // using (SqliteConnection con = 
        //     new SqliteConnection(cnstr))
        // {
        //     using (var tableCmd = con.CreateCommand())
        //     {
        //         con.Open();
        //         tableCmd.CommandText = $"INSERT INTO todo (name) VALUES ('{todo.Name}')";
        //         try
        //         {
        //             tableCmd.ExecuteNonQuery();
        //         }
        //         catch(Exception ex)
        //         {
        //             System.Console.WriteLine(ex.Message);
        //         }
        //     }
        // }
    }
    
}
