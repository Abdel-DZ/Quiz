using quizgame; // Import the quizgame namespace to access other classes like Database and Actions
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;



// Create the WebApplication builder to set up the application
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve static files (like HTML, CSS, JS) from the wwwroot folder
app.UseDefaultFiles(); // This serves the index.html file by default
app.UseStaticFiles(); // This serves other static files like CSS, JS, images

// Middleware to set or retrieve the client identifier cookie
app.Use(async (context, next) =>
{
    const string clientIdCookieName = "ClientId"; // Define the cookie name
    if (!context.Request.Cookies.TryGetValue(clientIdCookieName, out var clientId))
    {
        // If the client doesn't have an ID, generate a unique one
        clientId = GenerateUniqueClientId();
        context.Response.Cookies.Append(clientIdCookieName, clientId, new CookieOptions
        {
            HttpOnly = true, // Ensure that the cookie is accessible only by the server
            Secure = false, // Allow for development environments without HTTPS
            SameSite = SameSiteMode.Strict, // Ensure the cookie is sent with requests from the same site
            MaxAge = TimeSpan.FromDays(365) // The cookie will last for 1 year
        });
    }
    await next(); // Proceed to the next middleware
});

// Helper function to generate a unique client ID
static string GenerateUniqueClientId()
{
    using var rng = RandomNumberGenerator.Create(); // Use a cryptographic RNG for uniqueness
    var bytes = new byte[16]; // Generate a 16-byte array for the client ID
    rng.GetBytes(bytes);
    return Convert.ToBase64String(bytes); // Convert the bytes to a Base64 string
}

// Create an instance of the Database class to handle database connections
Database database = new();

// Create an instance of the Actions class, which uses the database instance to fetch data
Actions actions = new(database);

// Define a route to fetch questions by category
app.MapGet("/api/questions/{category}", actions.GetQuestionsByCategory);

// Run the web application
app.Run(); 
