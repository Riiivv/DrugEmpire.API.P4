using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using DrugEmpire.Infrastructure;
using DrugEmpire.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.


builder.Services.AddScoped<IAddress, AddressRepository>();
builder.Services.AddScoped<ICart, CartRepository>();
builder.Services.AddScoped<ICartItem, CartItemRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<IInventoryItem, InventoryItemRepository>();
builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<IOrderItem, OrderItemRepository>();
builder.Services.AddScoped<IPaymentTransAction, PaymentTransactionRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IProductCategory, ProductCategoryRepository>();
builder.Services.AddScoped<IProductImage, ProductImageRepository>();
builder.Services.AddScoped<IShipment, ShipmentRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
