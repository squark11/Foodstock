using System.Security.Cryptography;
using System.Text;
using FoodStock.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodStock.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
     private readonly IServiceProvider _serviceProvider;
     public DatabaseInitializer(IServiceProvider serviceProvider)
     {
         _serviceProvider = serviceProvider;
     }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<FoodStockDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();   
            }

            var roles = dbContext.Roles.ToList();
            if (!roles.Any())
            {
                roles = new List<Role>
                {
                    new()
                    {
                        Id = Guid.Parse("b2cd66ff-1c66-4e0a-a145-3fd9f408a99c"),
                        Name = "Admin"
                    },
                    new()
                    {
                        Id = Guid.Parse("744f2398-54b7-46b3-91ad-d88199b8e600"),
                        Name = "Employee"
                    },
                    new()
                    {
                        Id = Guid.Parse("494fbfca-ff6f-4f16-b16a-58e14b6a2354"),
                        Name = "Supplier"
                    }
                };
                dbContext.Roles.AddRange(roles);
                dbContext.SaveChanges();
            }
            
            var users = dbContext.Users.ToList();
            if (!users.Any())
            {
                users = new List<User>
                {
                    new()
                    {
                        Id = Guid.Parse("94b1ee6e-16cc-4735-8c37-8661b0a3342e"),
                        Email = "jan.kowalski@mailito.com",
                        Password = "AQAAAAIAAYagAAAAEMvuhMtSlGK9avPjzAYdUXIobFB3dsmcMAKygLF6UBC1flZYi0XWLsTq48bkCjEZdg==",
                        FirstName = "Jan",
                        Surname = "Kowalski",
                        RoleId = Guid.Parse("b2cd66ff-1c66-4e0a-a145-3fd9f408a99c")
                    },
                    new()
                    {
                        Id = Guid.Parse("800ff2de-c9a2-4c5d-a90b-c585728a13ff"),
                        Email = "adam.nowak@mailito.com",
                        Password = "AQAAAAIAAYagAAAAEMvuhMtSlGK9avPjzAYdUXIobFB3dsmcMAKygLF6UBC1flZYi0XWLsTq48bkCjEZdg==",
                        FirstName = "Adam",
                        Surname = "Nowak",
                        RoleId = Guid.Parse("744f2398-54b7-46b3-91ad-d88199b8e600")
                    }
                };
                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }
            var suppliers = dbContext.Suppliers.ToList();
            if (!suppliers.Any())
            {
                suppliers = new List<Supplier>
                {
                    new()
                    {
                        Id = Guid.Parse("1b2674ac-ee2b-48d2-a14d-7419f5878d63"),
                        Name = "Food Supply Company"
                    },
                    new()
                    {
                        Id = Guid.Parse("fcd5ccaa-feee-47c6-8536-1f30ccc27634"),
                        Name = "Mirek - dostawa żywnośći"
                    }
                };
                dbContext.Suppliers.AddRange(suppliers);
                dbContext.SaveChanges();
            }

            var producents = dbContext.Producents.ToList();
            if (!producents.Any())
            {
                producents = new List<Producent>
                {
                    new()
                    {
                        Id = Guid.Parse("f22ef231-a443-4559-a46b-8d343aaa7623"),
                        Name = "PrimeEats"
                    },
                    new()
                    {
                        Id = Guid.Parse("38d44961-86ba-4b43-881d-0dbd2af54758"),
                        Name = "ProviFood"
                    },
                    new()
                    {
                        Id = Guid.Parse("f8f17129-2b17-426c-bfc1-d8d6f65d2e40"),
                        Name = "NutriSense"
                    }
                };
                dbContext.Producents.AddRange(producents);
                dbContext.SaveChanges();
            }

            var categories = dbContext.Categories.ToList();
            if (!categories.Any())
            {
                categories = new List<Category>
                {
                    new()
                    {
                      Id = Guid.Parse("f32d7313-f596-4684-a6b5-11b30d9cec0d"),
                      CategoryName = "Fruit"
                    },
                    new()
                    {
                        Id = Guid.Parse("abdd0783-041b-4fd1-943d-75dcc58289d5"),
                        CategoryName = "Vegetables"
                    },
                    new()
                    {
                        Id = Guid.Parse("6a651e06-9361-46d7-829a-b195f3e6ea43"),
                        CategoryName = "Dairy"
                    },
                    new()
                    {
                        Id = Guid.Parse("485cd09e-81e2-43a8-8a15-97dd37cd8f2b"),
                        CategoryName = "Bread"
                    },
                    new()
                    {
                        Id = Guid.Parse("ca5f361c-ac3e-4bcf-a72f-76a9ff8bd78f"),
                        CategoryName = "Candy"
                    },
                    new()
                    {
                        Id = Guid.Parse("8cff5fb4-af73-405a-a818-4e22102d26f5"),
                        CategoryName = "Confectionery"
                    },
                    new()
                    {
                        Id = Guid.Parse("a1e6f417-1bf7-4473-822a-0a1221755c20"),
                        CategoryName = "Meat"
                    },
                    new()
                    {
                        Id = Guid.Parse("350cd147-4a36-4169-a65b-b1f07ca81d14"),
                        CategoryName = "Drink"
                    }
                };
                dbContext.Categories.AddRange(categories);
                dbContext.SaveChanges();
            }
            
            return Task.CompletedTask;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
