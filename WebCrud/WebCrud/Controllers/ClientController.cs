using Microsoft.AspNetCore.Mvc;
using Util.IService;
using Util.Models;
using Util.Service;
using WebCrud.Attributes;

namespace WebCrud.Controllers
{
    [SessionValidate]
    public class ClientController : Controller
    {

        private readonly Util.IService.IConsultaAPI _consultaAPI;
        private readonly Util.IService.IValidateForms _validateForms;

        public ClientController(Util.IService.IConsultaAPI consultaAPI, IValidateForms validateForms)
        {
            _consultaAPI = consultaAPI;
            _validateForms = validateForms;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObterClientes()
        {
            try
            {
                var clients = await _consultaAPI.GetClients();
                clients.clients = clients.clients.OrderBy(s => s.codcliente).ToList();
                return Json(new { success = true, clients = clients });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Não foi possivel obter os clientes, favor entrar em contato com o suporte." });
            }
        }

        public async Task<IActionResult> SalvarCliente(Clients clients)
        {
            try
            {
                var validate = await _validateForms.ValitadeAllClients(clients);

                if (validate.success)
                {
                    var result = await _consultaAPI.CreateClients(clients);
                    return Json(new { success = true, message = "Cliente adicionado com sucesso" });
                }
                else
                {
                    return Json(new { success = false, message = validate.message });
                }                                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Não foi possivel atualizar o cliente, favor entrar em contato com o suporte." });
            }
        }

        public async Task<IActionResult> AtualizarClientes(Clients clients)
        {
            try
            {
                var validate = await _validateForms.ValitadeAllClients(clients);

                if (validate.success)
                {
                    var result = await _consultaAPI.UpdateClients(clients);
                    return Json(new { success = true, message = "Cliente atualizado com sucesso" });
                }
                else
                {
                    return Json(new { success = false, message = validate.message });
                }                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Não foi possivel atualizar o cliente, favor entrar em contato com o suporte." });
            }
        }

        public async Task<IActionResult> DeletarCliente(int codcliente)
        {
            try
            {
                if (codcliente > 0)
                {
                    var result = await _consultaAPI.DeleteClients(codcliente);
                    return Json(new { success = true, message = "Cliente deletado com sucesso" });
                }
                else
                {
                    return Json(new { success = false, message = "Esse cliente não existe" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Não foi possivel deletar o cliente, favor entrar em contato com o suporte." });
            }
        }

        
    }
}
