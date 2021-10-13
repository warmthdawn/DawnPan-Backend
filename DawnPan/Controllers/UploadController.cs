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
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private UploadService uploadService;

        public UploadController(UploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileUploadMeta meta)
        {
            bool success = await uploadService.UploadFile(meta, meta.File);
            if(success)
            {
                return Ok();
            }
            return Problem();
        }
    }
}
