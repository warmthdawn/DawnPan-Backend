using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Model
{
    public class FileUploadMeta
    {
        public string Hash { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public long UploadDirectory { get; set; }
    }
}
