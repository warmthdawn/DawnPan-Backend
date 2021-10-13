using DawnPan.Entity;
using DawnPan.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Service
{
    public class DirectoryService
    {
        private AppDbContext _context;
        public DirectoryService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<DirectoryInfo>> GetSubDirectories(long parentId)
        {
            return await _context.Directories
                .Include(it => it.Children)
                .Where(it => it.ParentId == parentId)
                .Select(it => new DirectoryInfo()
                {
                    Id = it.Id,
                    Name = it.Name,
                    IsLeafDirectiory = !it.Children.Any()
                }).ToListAsync();
        }

        public async Task<List<DirectoryInfo>> GetDirectoryRoot()
        {
            if(!(await _context.Directories.AnyAsync()))
            {
                await GenerateDefaultRoot();
            }
            //TODO: 关联用户表获取用户的自己的根目录
            var result = await _context.Directories
                .Where(it => it.ParentId == null)
                .Select(it => new DirectoryInfo()
                {
                    Id = it.Id,
                    Name = it.Name,
                    IsLeafDirectiory = false,
                }).ToListAsync();


            return result;
        }

        public async Task GenerateDefaultRoot()
        {
            await _context.AddAsync(new FileDirectory()
            {
                Name = "快速访问",
            });

            await _context.AddAsync(new FileDirectory()
            {
                Name = "我的网盘",
            }); 
            await _context.AddAsync(new FileDirectory()
            {
                Name = "我的分享",
            }); 
            await _context.AddAsync(new FileDirectory()
            {
                Name = "回收站",
            });
            await _context.SaveChangesAsync();
        }


        public async Task<long> CreateDirectory(long parentId, string name)
        {
            return await CreateDirectory(parentId, name, false);
        }
        public async Task<long> CreateDirectory(long parentId, string name, bool forceCreate)
        {
            if (!forceCreate && await ExistDirectory(parentId, name))
            {
                return -1;
            }
            var item = new FileDirectory()
            {
                Name = name,
                ParentId = parentId
            };
            await _context.Directories.AddAsync(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }


        public async Task RenameDirectory(long id, string newName)
        {
            var dir = await _context.Directories.FindAsync(id);
            dir.Name = newName;
            await _context.SaveChangesAsync();
        }


        public async Task MoveDirectoryTo(long dirId, long destination)
        {
            var dir = await _context.Directories.FindAsync(dirId);

            bool exist = await ExistDirectory(destination, dir.Name);
            dir.ParentId = destination;
            if (exist)
            {
                await MergeDirectory(destination, dirId);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistDirectory(long parentId, string name)
        {
            return await _context.Directories.Where(it => it.ParentId == parentId && it.Name == name)
                .AnyAsync();
        }

        public async Task CopyDirectoryTo(long dirId, long destination)
        {
            var dir = await _context.Directories
                .Include(it => it.Children)
                .Include(it => it.Files)
                .FirstAsync(it => it.Id == dirId);
            //复制文件

            //复制子文件夹
            foreach (var item in dir.Children)
            {
                bool exist = await ExistDirectory(destination, item.Name);
                var newId = await CreateDirectory(destination, item.Name, true);
                await CopyDirectoryTo(item.Id, newId);
                if (exist)
                {
                    await MergeDirectory(destination, newId);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task MergeDirectory(long first, long second)
        {
            var dirFirst = await _context.Directories
                .Include(it => it.Children)
                .Include(it => it.Files)
                .FirstAsync(it => it.Id == first);
            var dirSecond = await _context.Directories
                .Include(it => it.Children)
                .Include(it => it.Files)
                .FirstAsync(it => it.Id == second);

            var firstFileNames = dirFirst.Files.Select(it => it.FileName).ToHashSet();
            foreach (var fileSecond in dirSecond.Files)
            {
                if (firstFileNames.Contains(fileSecond.FileName))
                {
                    //TODO: 重复文件名处理
                    fileSecond.DirectoryId = dirFirst.Id;
                }

                fileSecond.DirectoryId = dirFirst.Id;

            }
            var firstChildren = dirFirst.Children.ToDictionary(it => it.Name, it => it.Id);
            foreach (var childSecond in dirSecond.Children)
            {
                if (firstChildren.ContainsKey(childSecond.Name))
                {
                    //TODO: 重复文件夹名处理
                    childSecond.ParentId = dirFirst.Id;
                    //合并文件夹
                    await MergeDirectory(firstChildren[childSecond.Name], childSecond.Id);
                }
                else
                {
                    childSecond.ParentId = dirFirst.Id;
                }
            }

            _context.Remove(dirSecond);
            await _context.SaveChangesAsync();

        }
    }
}
