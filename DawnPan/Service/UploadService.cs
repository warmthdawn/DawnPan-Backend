using DawnPan.Entity;
using DawnPan.Model;
using DawnPan.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Service
{
    public class UploadService
    {
        private AppDbContext _context;
        private FileUtils _fileUtils;
        public UploadService(AppDbContext context, FileUtils fileUtils)
        {
            this._context = context;
            this._fileUtils = fileUtils;
        }

        public async Task<bool> UploadFile(FileUploadMeta meta, IFormFile file)
        {
            bool success = await _fileUtils.SaveFileAsync(meta.Hash, file);
            if(!success)
            {
                return false;
            }

            if(meta.UploadDirectory < 0)
            {
                return false;
            }


            var fileItem = new FileItem()
            {
                FileName = meta.FileName,
                DirectoryId = meta.UploadDirectory,
                Property = new FileProperty()
                {
                    LastModified = DateTime.Now,
                    Size = _fileUtils.GetFileSize(meta.Hash),
                },
            };
            await _context.AddAsync(fileItem);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
