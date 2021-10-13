using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Utils
{
    public class FileUtils
    {
        private string savePath;

        public FileUtils(string savePath)
        {
            this.savePath = savePath;
        }

        public async Task<bool> SaveFileAsync(string hash, IFormFile formFile)
        {
            var target = GetFileName(hash);
            if(File.Exists(target))
            {
                return true;
            }
            var temp = Path.GetTempFileName();
            using(var stream = File.Create(temp))
            {
                await formFile.CopyToAsync(stream);
            }
            string newSha = HashUtils.GetShaString(temp);
            if(newSha != hash)
            {
                File.Delete(temp);
                return false;
            }

            File.Move(temp, target);
            return true;

        }

        public long GetFileSize(string hash)
        {
            var name = GetFileName(hash);
            if(File.Exists(name))
            {
                return -1;
            }
            return new FileInfo(name).Length;
        }


        public string GetFileName(string hash)
        {
            string dir = Path.Join(savePath, hash.Substring(0, 2));
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return Path.GetFullPath(Path.Join(dir, hash));
        }
    }
}
