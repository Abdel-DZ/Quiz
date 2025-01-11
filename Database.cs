using Npgsql; // Import Npgsql for PostgreSQL database connection
using Microsoft.AspNetCore.Http; // For IResult and related types

namespace quizgame; // Define the quizgame namespace for the classes in this file

public class Database
{
    // Database connection parameters (you may want to keep these private and secure)
    private readonly string _host = "localhost"; // The host where the PostgreSQL database is running
    private readonly string _port = "5432"; // The port for PostgreSQL (default is 5432)
    private readonly string _username = "postgres"; // Your PostgreSQL username
    private readonly string _password = "Palestro!2024!"; // Your PostgreSQL password
    private readonly string _database = "quizgame"; // The name of the database to connect to

    private NpgsqlDataSource _connection; // This will hold the database connection

    // Constructor that builds the connection string and initializes the connection
    public Database()
    {
        // Build the connection string from the parameters above
        string connectionString = $"Host={_host};Port={_port};Username={_username};Password={_password};Database={_database}";
        
        // Create a connection to the database using NpgsqlDataSource
        _connection = NpgsqlDataSource.Create(connectionString);
    }

    // Method to get the current database connection
    public NpgsqlDataSource Connection()
    {
        return _connection; // Return the connection for use in other parts of the application
    }
}