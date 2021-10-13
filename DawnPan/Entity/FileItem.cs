using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Entity
{
    public class FileItem
    {
        public long Id { get; set; }
        public byte[] Hash { get; set; }
        public string FileName { get; set; }


        public long DirectoryId { get; set; }
        public FileDirectory Directory { get; set; }

        public long PropertyId { get; set; }
        public FileProperty Property { get; set; }
    }
}
