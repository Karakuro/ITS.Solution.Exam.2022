using ITS.Solution.Exam._2022.DAL;
using ITS.Solution.Exam._2022.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSqlServer<FoodDbContext>(builder.Configuration.GetConnectionString("Default"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Mapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope())
{
    FoodDbContext? ctx = scope.ServiceProvider.GetService<FoodDbContext>();
    if(ctx != null)
    {
        ctx.Database.Migrate();
        if (ctx.Categories.Any())
        {
            ctx.Categories.RemoveRange(ctx.Categories);
            ctx.SaveChanges();
        }

        string productsJson = File.ReadAllText("Products.json");
        List<JsonEntity> products = JsonSerializer.Deserialize<List<JsonEntity>>(productsJson);

        List<Category> categories = products.Select(p => p.category)
            .Distinct()
            .Select(c => new Category() { Name = c })
            .ToList();
        List<Product> productsDb = (from p in products
                                    join c in categories
                                    on p.category equals c.Name
                                    select new Product()
                                    {
                                        Barcode = p.barcode.ToString(),
                                        Category = c,
                                        CategoryId = c.CategoryId,
                                        Ingredients = p.ingredients,
                                        Name = p.product,
                                        Nutriscore_Grade = p.nutriscore_grade,
                                        Nutriscore_Value = p.nutriscore_score,
                                        Producer = p.brand
                                    }).ToList();
        ctx.AddRange(productsDb);
        ctx.SaveChanges();
    }
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
