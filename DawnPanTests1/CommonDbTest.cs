using DawnPan.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DawnPanTests
{
    public class CommonDbTest
    {
        protected CommonDbTest()
        {
            var connectionString = "server=localhost;user=root;password=12345678;database=dawnpan_test";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            ContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
            Seed();
        }

        protected DbContextOptions<AppDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var root = new FileDirectory()
                {
                    Name = "WD的网盘"
                };
                context.Add(root);

                context.SaveChanges();
            }
        }
    }
}