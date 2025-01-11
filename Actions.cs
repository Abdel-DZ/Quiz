using Npgsql; // Import Npgsql for PostgreSQL database interaction
using System.Collections.Generic; // Import for using Lists in C#
using Microsoft.AspNetCore.Http; // For IResult and related types


namespace quizgame; // Define the quizgame namespace for the classes in this file

public class Actions
{
    private readonly Database _database; // The Database class is injected here to handle database interactions

    // Constructor that accepts a Database instance for later use
    public Actions(Database database)
    {
        _database = database; // Store the database instance to use in methods
    }

    // Method to fetch questions by category from the database
    public async Task<IResult> GetQuestionsByCategory(string category)
    {
        // Create a command to query the database for questions by category
        await using var connection = _database.Connection().CreateCommand("SELECT questiontext, option1, option2, option3, option4 FROM QuestionsByCategory WHERE CategoryName = $1");
        connection.Parameters.AddWithValue("$1", category); // Add the category parameter to the query

        var result = new List<object>(); // Create a list to hold the results from the query

        // Execute the query and read the data
        await using (var reader = await connection.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync()) // Read each row from the database
            {
                // Add the question and options to the result list
                result.Add(new
                {
                    questiontext = reader.GetString(0), // Get the question text
                    option1 = reader.GetString(1), // Get option 1
                    option2 = reader.GetString(2), // Get option 2
                    option3 = reader.GetString(3), // Get option 3
                    option4 = reader.GetString(4), // Get option 4
                });
            }
        }

        return Results.Ok(result); // Return the result as a successful response
    }
}