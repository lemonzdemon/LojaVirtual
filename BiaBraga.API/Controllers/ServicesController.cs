using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using BiaBraga.API.Models.Enums;
using BiaBraga.API.Services;
using BiaBraga.Domain.Enums;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BiaBraga.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    [AllowAnonymous]
    public class ServicesController : ControllerService
    {
        private readonly IBiaBragaRepository _repository;

        public ServicesController(IBiaBragaRepository repository, ILogger<ServicesController> logger) : base(logger)
        {
            _repository = repository;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(string folder, string name)
        {
            try
            {
                if (!await VerifyAcessAsync(folder, name, null))
                {
                    return Unauthorized();
                }

                NewLog(nameof(Upload), TypeLogger.Other, $"selecionando folder:{folder}; name:{name}");
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
                        NewLog(nameof(Upload), TypeLogger.Other, $"selecionando user para newFileName {newFileName}");
                        var users = await _repository.GetWhereAsync<User>(x => x.Id.ToString() == name);

                        var user = users.FirstOrDefault();

                        var fullPathOld = Path.Combine(pathToSave, user.Image);

                        NewLog(nameof(Upload), TypeLogger.Other, $"userid:{user.Id}; removendo pathOld");
                        if (System.IO.File.Exists(fullPathOld))
                        {
                            System.IO.File.Delete(fullPathOld);
                        }

                        //se o nome da imagem for igual, nao atualiza na base de dados
                        if (user.Image != newFileName)
                        {
                            NewLog(nameof(Upload), TypeLogger.Other, $"userid:{user.Id}; update oldImage:{user.Image} to newImage:{newFileName}");
                            user.Image = newFileName;
                            await _repository.UpdateAsync(user);
                        }
                    }

                    NewLog(nameof(Upload), TypeLogger.Other, $"{fullPath} copiando");
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    NewLog(nameof(Upload), TypeLogger.FinishSucess);
                    return Ok(newFileName);
                }
                else
                {
                    NewLog(nameof(Upload), TypeLogger.FinishDivergence, "formFile notfound");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(Upload));
            }
        }

        [HttpPost("delete-files")]
        public async Task<IActionResult> RemoveFile(string folder, string file)
        {
            try
            {
                NewLog(nameof(RemoveFile), TypeLogger.StartProcess);

                file = file.Replace("/", " ").Trim();

                if (!await VerifyAcessAsync(folder, null, file))
                {
                    return Unauthorized();
                }

                NewLog(nameof(RemoveFile), TypeLogger.Other, "selecionando arquivo");
                var folderName = Path.Combine("Resources", "images", folder, file);
                var fullpath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                    NewLog(nameof(RemoveFile), TypeLogger.FinishSucess, $"{fullpath} removido");
                    return Ok();
                }
                NewLog(nameof(RemoveFile), TypeLogger.FinishDivergence, $"{fullpath} notfound");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(RemoveFile));
            }
        }

        /// <summary>
        /// Verifica se usuario atual tem permissao, e se nao tiver grava log de arquivos que pretende alterar
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <param name="file"></param>
        /// <returns>true se tiver permissao</returns>
        private async Task<bool> VerifyAcessAsync(string folder, string name, string file)
        {
            if (!User.Identity.IsAuthenticated)
            {
                NewLog(nameof(RemoveFile), TypeLogger.FinishDivergence, $"folder:{folder};name:{name};file:{file}; unauthorized");
                return false;
            }

            //Se usuario nao for um administrador, ele só tera acesso para alterar a sua imagem
            if (folder != "users" && !User.IsInRole(Role.Administrador.ToString()))
            {
                NewLog(nameof(RemoveFile), TypeLogger.FinishDivergence, $"folder:{folder};name:{name};file:{file};userEmail:{User.FindFirstValue(ClaimTypes.Email)}; unauthorized");
                return false;
            }

            //Se usuario autenticado não for um administrador, irá verfificar se o name é igual ao seu ID ou file igual sua image
            if (!User.IsInRole(Role.Administrador.ToString()) && folder == "users")
            {
                var usersAuth = await _repository.GetWhereAsync<User>(x => User.FindFirstValue(ClaimTypes.Email) == x.Email);

                var user = usersAuth.First();
                if ((string.IsNullOrEmpty(name) && user.Id.ToString() != name) || (string.IsNullOrEmpty(file) && user.Image != file))
                {
                    NewLog(nameof(RemoveFile), TypeLogger.FinishDivergence, $"folder:{folder};name:{name};file:{file};userEmail:{user.Email}; unauthorized");
                    return false;
                }
            }

            return true;
        }
    }
}
