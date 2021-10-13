using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Model
{
    public class FileInfo
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public DateTime ModifyTime { get; set; }
        public long Size { get; set; }
        public string FileType { get; set; }
    }
}
