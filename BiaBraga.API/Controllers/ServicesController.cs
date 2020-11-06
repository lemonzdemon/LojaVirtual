using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiaBraga.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IBiaBragaRepository _repository;

        public ServicesController(IBiaBragaRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(string folder, string name)
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = string.Empty;
                string newFileName = string.Empty;
                switch (folder)
                {
                    case "products":
                        folderName = Path.Combine("Resources", "images", folder, name);
                        newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                        break;
                    case "users":
                        folderName = Path.Combine("Resources", "images", folder);
                        newFileName = name;
                        break;
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                Directory.CreateDirectory(pathToSave);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                    newFileName += new FileInfo(filename).Extension;
                    newFileName = newFileName.Replace("\"", " ").Trim();
                    var fullPath = Path.Combine(pathToSave, newFileName);

                    if (folder == "users")
                    {
                        var users = await _repository.GetWhereAsync<User>(x => x.Id.ToString() == name);

                        if (users.Any())
                        {
                            var user = users.FirstOrDefault();

                            var fullPathOld = Path.Combine(pathToSave, user.Image);

                            if (System.IO.File.Exists(fullPathOld))
                            {
                                System.IO.File.Delete(fullPathOld);
                            }

                            //se o nome da imagem for igual, nao atualiza na base de dados
                            if (user.Image != newFileName)
                            {
                                user.Image = newFileName;
                                await _repository.UpdateAsync(user);
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(newFileName);
                }
            }
            catch (Exception)
            {
            }

            return BadRequest();
        }

        [HttpPost("delete-files")]
        public IActionResult RemoveFile(string folder, string file)
        {
            try
            {
                var folderName = Path.Combine("Resources", "images", folder, file.Replace("/", " ").Trim());
                var fullpath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }

                return NotFound();
            }
            catch (Exception)
            {
            }

            return BadRequest();
        }
    }
}
