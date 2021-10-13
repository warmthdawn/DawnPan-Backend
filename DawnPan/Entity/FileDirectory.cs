using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Entity
{
    public class FileDirectory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public FileDirectory Parent { get; set; }
        public List<FileDirectory> Children { get; set; }
        public List<FileItem> Files { get; set; }
    }
}
