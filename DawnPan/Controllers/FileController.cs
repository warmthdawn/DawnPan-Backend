using DawnPan.Model;
using DawnPan.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private FileService fileService;
        private DirectoryService directoryService;

        public FileController(FileService fileService, DirectoryService directoryService)
        {
            this.fileService = fileService;
            this.directoryService = directoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DirectoryInfo>>> DirectoryRoot()
        {
            return await directoryService.GetDirectoryRoot();
        }
        [HttpGet("{current}")]
        public async Task<ActionResult<List<DirectoryInfo>>> SubDirectories(long current)
        {
            return await directoryService.GetSubDirectories(current);
        }
        [HttpGet("{current}")]
        public async Task<ActionResult<List<FileInfo>>> Files(long current)
        {
            return await fileService.GetFilesInDirectory(current);
        }

        [HttpGet("{current}")]
        public async Task<ActionResult<List<DirectoryInfo>>> ParentDirectories(long current)
        {
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> CreateDirectory(DirectoryInfo info)
        {
            await directoryService.CreateDirectory(info.Id, info.Name);
            return Ok();
        }


    }
}
