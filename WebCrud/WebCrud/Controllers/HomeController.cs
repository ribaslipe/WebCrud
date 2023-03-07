using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Diagnostics;
using Util.Models;
using WebCrud.Models;

namespace WebCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Util.IService.IConsultaAPI _consultaAPI;
        private readonly Util.Memory _memory;

        public HomeController(ILogger<HomeController> logger, Util.IService.IConsultaAPI consultaAPI, Util.Memory memory)
        {
            _logger = logger;
            _consultaAPI = consultaAPI;
            _memory = memory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Logar(LoginModel model)
        {
            try
            {
                var logar = await _consultaAPI.GetLogin(model);
                if (logar != null && logar.Success)
                {                    
                    _memory.SetMemoryItem<string>("Token", logar.AccessToken);                    
                    return Json(new { success = true });
                }                    
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Usuário inválido ou senha inválido." });
            }

        }

        public async Task<IActionResult> Sair()
        {
            try
            {
                _memory.RemoveMemoryItem("Token");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Houve um erro ao realizar o logoff." });
            }

        }

    }
}