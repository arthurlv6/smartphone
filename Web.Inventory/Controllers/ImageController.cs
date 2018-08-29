using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly InventoryContext _context;
        public ImageController(IHostingEnvironment hostingEnvironment, InventoryContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var path = _hostingEnvironment.WebRootPath + "\\images\\products\\" + id;
            try
            {
                Byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/jpeg");
            }
            catch (Exception)
            {
                path = _hostingEnvironment.WebRootPath + "\\images\\products\\none.gif";
                Byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/jpeg");
            }

        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Upload(int id)
        {
            try
            {
                var p = _context.Product.FirstOrDefault(d => d.Id == id);
                if (p == null) return BadRequest();
                var fileName = "";
                var httpRequest = HttpContext.Request;
                if (httpRequest.Form.Files.Count > 0)
                {
                    foreach (var file in httpRequest.Form.Files)
                    {
                        fileName=file.FileName.Split("\\").LastOrDefault().Split('/').LastOrDefault();
                        var filePath = _hostingEnvironment.WebRootPath + "\\images\\products\\";
                        p.Profile = fileName;
                        await _context.SaveChangesAsync();
                        using (FileStream fs = System.IO.File.Create(filePath + fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
                return Ok(fileName);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}