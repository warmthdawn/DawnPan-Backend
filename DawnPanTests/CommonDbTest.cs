using DawnPan.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DawnPan.Tests
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

                var root = newDir("WD的网盘", null);
                context.Add(root);


                var doc = newDir("文档", root);
                context.Add(doc);
                context.Add(newDir("图片", root));
                context.Add(newDir("视频", root));

                context.Add(newDir("作业1", doc));
                context.Add(newDir("作业2", doc));

                var homework3 = newDir("作业3", doc);
                context.Add(homework3);

                context.Add(newDir("作业3-详情", homework3));


                context.SaveChanges();
            }
        }



        private FileDirectory newDir(string name, FileDirectory parent)
        {
            return new FileDirectory()
            {
                Name = name,
                Parent = parent
            };
        }
    }
}