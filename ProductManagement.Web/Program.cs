using ProductManagement.Application;
using ProductManagement.Core.Entities;
using ServiceImplementation;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
   // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", async (IProductRepository productRepository) =>
{
  var products =await productRepository.GetAllAsync();
    return Results.Ok(new {products});

});
app.MapPost("/products", (Product product, IProductRepository productRepu ) => 
{ 
});

app.Run();

