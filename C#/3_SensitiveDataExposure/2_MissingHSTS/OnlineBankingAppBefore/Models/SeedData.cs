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
                    Id = "1",
                    FirstName = "Lidia",
                    MiddleName = "Maria",
                    LastName = "Sbarcea",
                    DateOfBirth = DateTime.ParseExact("11/10/1953", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Email = "lidia.sbarcea@gmail.com",
                    UserName = "lidia.sbarcea@gmail.com",
                    PhoneNumber = "0712345678",
                    Accounts = new List<Account> {
                            new Account {
                                    Name = "Savings",
                                    AccountType = AccountType.Savings,
                                    Balance = 1250.55m
                                } ,
                            new Account {
                                    Name = "Checking",
                                    AccountType = AccountType.Checking,
                                    Balance = 2340.10m
                                }
                        },
                    FundTransfers = new List<FundTransfer> {
                            new FundTransfer {
                                    AccountFrom = 1,
                                    AccountTo = 2,
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
                    Id = "2",
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
                        },
                    FundTransfers = new List<FundTransfer>
                    {
                            new FundTransfer {
                                    AccountFrom = 1,
                                    AccountTo = 2,
                                    Amount = 100.00m,
                                    TransactionDate = DateTime.ParseExact("05/08/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)
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