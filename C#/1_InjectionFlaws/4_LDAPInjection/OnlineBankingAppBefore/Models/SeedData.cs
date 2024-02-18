using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBankingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBankingApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OnlineBankingAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<OnlineBankingAppContext>>()))
            {
                if (context.Customer.Any())
                {
                    return;
                }

                context.Customer.AddRange(
                    new Customer
                    {
                        FirstName = "Lidia",
                        MiddleName = "Maria",
                        LastName = "Sbarcea",
                        DateOfBirth = DateTime.Parse("11/10/1953"),
                        Email = "lidia.sbarcea@gmail.com",
                        Phone = "0712345678",
                        Accounts = new List<Account>{
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
                            }
                    },
                    new Customer
                    {
                        FirstName = "Maria",
                        MiddleName = "Daniela",
                        LastName = "Pitulice",
                        DateOfBirth = DateTime.Parse("11/03/1964"),
                        Email = "maria.pitulice@gmail.com",
                        Phone = "0798765432",
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
                    }

                );

                context.SaveChanges();

                context.FundTransfer.AddRange(
                    new FundTransfer
                    {
                        CustomerID = 1,
                        AccountFrom = 1,
                        AccountTo = 2,
                        Amount = 50.00m,
                        TransactionDate = DateTime.Parse("30/07/2023")
                    },
                    new FundTransfer
                    {
                        CustomerID = 1,
                        AccountFrom = 1,
                        AccountTo = 2,
                        Amount = 100.00m,
                        TransactionDate = DateTime.Parse("05/08/2023")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}