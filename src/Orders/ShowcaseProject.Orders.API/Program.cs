var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Test")
{
    builder.Services.AddUnsafeCors();
}
else
{
    builder.Services.AddCustomCors(
        allowedOrigins: builder.Configuration.GetSection("Configuration:Cors:AllowedOrigins").Get<string[]>() ?? throw new Exception("'Cors:AllowedOrigins' configuration missing!")
        );
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
