﻿using CustomerService.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options): base(options)
        {
        }

        public DbSet<Customer> Customers {get; set;}

        public DbSet<User> Users {get; set;}
    }
}