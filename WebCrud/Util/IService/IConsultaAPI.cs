using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Models;

namespace Util.IService
{
    public interface IConsultaAPI
    {
        Task<TokenModel?> GetLogin(LoginModel login);
        Task<ClientsModel?> GetClients();
        Task<bool> UpdateClients(Clients clients);
        Task<bool> CreateClients(Clients clients);
        Task<bool> DeleteClients(int codcliente);
    }
}
