using Microsoft.EntityFrameworkCore;
using AuthService.Models; // Import the namespace for RegisterRequest
using System.ComponentModel.DataAnnotations; // Required for ValidationResult

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// ...rest of your code...
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/register", async (RegisterRequest request, AppDbContext dbContext) =>
{
    // Validate the request model
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(request);
    if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults.Select(vr => vr.ErrorMessage));
    }

    // Check if the username or email already exists
    var existingUser = await dbContext.Users
        .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);

    if (existingUser != null)
    {
        return Results.BadRequest("Username or email is already in use.");
    }

    // Create a new user
    var user = new User
    {
        Username = request.Username,
        Email = request.Email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) // Hash the password
    };

    await dbContext.Users.AddAsync(user);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.MapPost("/login", async (LoginRequest request, AppDbContext dbContext) =>
{
    if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
    {
        return Results.BadRequest("Username and password are required.");
    }

    var user = await dbContext.Users
        .FirstOrDefaultAsync(u => u.Username == request.Username);

    if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash) ||
        !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    {
        return Results.BadRequest("Invalid username or password.");
    }

    return Results.Ok(new { user.Username, user.Email });
});

app.Run();