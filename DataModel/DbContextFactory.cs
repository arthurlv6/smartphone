using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    public static class DbContextFactory
    {
        public static InventoryContext Create(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var connStr = $"Server=ARTHUR;Database={email};Trusted_Connection=True;";
                var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();
                optionsBuilder.UseSqlServer(connStr);
                return new InventoryContext(optionsBuilder.Options);
            }
            else
            {
                throw new ArgumentNullException("ConnectionId");
            }
        }
    }
}
