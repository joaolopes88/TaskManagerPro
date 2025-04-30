using Microsoft.EntityFrameworkCore;
using AuthService.Models; // Import the namespace for RegisterRequest

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
    // Example logic for user registration
    var user = new User
    {
        Username = request.Username,
        Email = request.Email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) 
    };

    dbContext.Users.Add(user);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.MapPost("/login", async (LoginRequest request, AppDbContext dbContext) =>
{
    // Find user by username or email
    var user = await dbContext.Users
        .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);

    if (user == null)
    {
        return Results.BadRequest("Invalid username/email or password.");
    }

    // Verify password
    bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
    if (!isValid)
    {
        return Results.BadRequest("Invalid username/email or password.");
    }

    // For demo: return user info (do not return password hash in production)
    return Results.Ok(new { user.Id, user.Username, user.Email });
});


app.Run();