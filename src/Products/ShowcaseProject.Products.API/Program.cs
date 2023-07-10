using MediatR.NotificationPublishers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShowcaseProject.Products.API.Exceptions;
using ShowcaseProject.Products.Application.Mediator;
using ShowcaseProject.Products.Application.Queries.Products;
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;
using ShowcaseProject.Products.Domain.SeedWork;
using ShowcaseProject.Products.Infrastructure;
using ShowcaseProject.Products.Infrastructure.Repositories;
using SqlKata.Compilers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

if(builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Test")
{
    builder.Services.AddUnsafeCors();
}
else
{
    builder.Services.AddCustomCors(
        allowedOrigins: builder.Configuration.GetSection("Configuration:Cors:AllowedOrigins").Get<string[]>() ?? throw new ConfigurationException("'Cors:AllowedOrigins' configuration missing!")
        );
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => {
    cfg.NotificationPublisher = new TaskWhenAllPublisher();
    cfg.RegisterServicesFromAssemblyContaining<DomainMediator>();
});
builder.Services.AddTransient<IDomainMediator, DomainMediator>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductQueries, ProductQueries>();

builder.Services.AddDbContext<ProductsContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));

builder.Services.AddTransient((serviceProvider) =>
{
    var dbConnection = new SqlConnection(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("ProductDatabase"));
    var queryCompiler = new SqlServerCompiler();
    var logger = serviceProvider.GetService<ILogger<ProductQueryFactory>>();

    return new ProductQueryFactory(dbConnection, queryCompiler, logger);
});

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ProductsContext>();
        db.Database.Migrate();
    }
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();