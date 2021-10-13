using DawnPan.Entity;
using DawnPan.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Service
{
   
    public class FileService
    {
        private AppDbContext _context;
        public FileService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<FileInfo>> GetFilesInDirectory(long dirId)
        {
            return await _context.Files
                .Include(it=>it.Property)
                .Where(it => it.DirectoryId == dirId)
                .Select(it => new FileInfo
                {
                    FileName = it.FileName,
                    Id = it.Id,
                    FileType = GetFileType(it.FileName),
                    Size = it.Property.Size,
                    ModifyTime = it.Property.LastModified,

                }).ToListAsync();
        }

        public async Task CopyFile(long fileId, long destinationDirId)
        {
            var file = await _context.Files.Include(it => it.Property)
                .FirstAsync(it => it.Id == fileId);
            var propNew = new FileProperty()
            {
                Size = file.Property.Size,
                LastModified = file.Property.LastModified,
            };
            var fileNew = new FileItem()
            {
                FileName = file.FileName,
                DirectoryId = destinationDirId,
                Property = propNew,
                Hash = file.Hash,

            };
            await _context.Files.AddAsync(fileNew);
            await _context.SaveChangesAsync();
        }

        public async Task MoveFile(long fileId, long destinationDirId)
        {
            var file = await _context.Files.Include(it => it.Property)
                .FirstAsync(it => it.Id == fileId);
            file.DirectoryId = destinationDirId;
            await _context.SaveChangesAsync();
        }


        public static string GetFileType(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.')) + " 文件";
        }
    }
}
