using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBankingApp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingApp.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<Customer>>();

            var user1 = await userManager.FindByNameAsync("lidia.sbarcea@gmail.com");
            if (user1 == null) {
                user1 = new Customer
                {
                    FirstName = "Lidia",
                    MiddleName = "Maria",
                    LastName = "Sbarcea",
                    DateOfBirth = DateTime.ParseExact("11/10/1953", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Email = "lidia.sbarcea@gmail.com",
                    UserName = "lidia.sbarcea@gmail.com",
                    PhoneNumber = "0712345678",
                    Accounts = new List<Account> {
                            new Account {
                                    ID = new Guid("79f5c2ef-cd92-4542-8478-deeac12a75cb"),
                                    Name = "Savings",
                                    AccountType = AccountType.Savings,
                                    Balance = 1250.55m
                                } ,
                            new Account {
                                    ID = new Guid("c70cdd5b-0a5d-4891-99f3-065cedbce0f2"),
                                    Name = "Checking",
                                    AccountType = AccountType.Checking,
                                    Balance = 2340.10m
                                }
                        },
                    FundTransfers = new List<FundTransfer> {
                            new FundTransfer {
                                    ID = new Guid("7C281D46-F2AB-4027-A4D4-3BB97A60012C"),
                                    AccountFrom = new Guid("79f5c2ef-cd92-4542-8478-deeac12a75cb"),
                                    AccountTo = new Guid("c70cdd5b-0a5d-4891-99f3-065cedbce0f2"),
                                    Amount = 50.00m,
                                    TransactionDate = DateTime.ParseExact("30/07/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                            }
                    }
                };

                user1.PasswordHash = userManager.PasswordHasher.HashPassword(user1, "password");

                var result = await userManager.CreateAsync(user1);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user1. Reason: " + result.Errors.FirstOrDefault()?.Description);
                }
            }

            var user2 = await userManager.FindByNameAsync("maria.pitulice@gmail.com");
            if (user2 == null) {
                user2 = new Customer
                {
                    FirstName = "Maria",
                    MiddleName = "Daniela",
                    LastName = "Pitulice",
                    DateOfBirth = DateTime.ParseExact("11/03/1964", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Email = "maria.pitulice@gmail.com",
                    UserName = "maria.pitulice@gmail.com",
                    PhoneNumber = "0798765432",
                    Accounts = new List<Account>{
                            new Account {                   
                                    Name = "Savings",
                                    AccountType = AccountType.Savings,
                                    Balance = 15030.00m
                                } ,
                            new Account {            
                                    Name = "Checking",
                                    AccountType = AccountType.Checking,
                                    Balance = 2010.35m
                                }
                        }
                };

                user2.PasswordHash = userManager.PasswordHasher.HashPassword(user2, "password");

                var result = await userManager.CreateAsync(user2);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user2. Reason: " + result.Errors.FirstOrDefault()?.Description);
                }
            }
        }
    }
}