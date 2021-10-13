using Microsoft.VisualStudio.TestTools.UnitTesting;
using DawnPan.Service;
using System;
using System.Collections.Generic;
using System.Text;
using DawnPan.Tests;
using DawnPan.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace DawnPan.Service.Tests
{
    [TestClass]
    public class DirectoryServiceTests : CommonDbTest
    {

        [TestMethod]
        public async Task CreateDirectoryTest()
        {
            using var db = new AppDbContext(ContextOptions);
            var service = new DirectoryService(db);
            await service.CreateDirectory(1, "新建文件夹");

            var has = db.Directories.Where(it => it.Name == "新建文件夹").Any();
            Assert.IsTrue(has);
        }

        [TestMethod()]
        public async Task GetSubDirectoriesTest()
        {
            using var db = new AppDbContext(ContextOptions);
            var service = new DirectoryService(db);
            var result = await service.GetSubDirectories(2);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(it => it.Name == "作业1"));
            Assert.IsTrue(result.First(it => it.Name == "作业1").IsLeafDirectiory);
            Assert.IsFalse(result.First(it => it.Name == "作业3").IsLeafDirectiory);
        }

        [TestMethod()]
        public async Task RenameDirectoryTest()
        {
            using var db = new AppDbContext(ContextOptions);
            var service = new DirectoryService(db);
            await service.RenameDirectory(3, "照片");
            Assert.AreEqual("照片", db.Directories.First(it=>it.Id == 3).Name);

        }
    }
}