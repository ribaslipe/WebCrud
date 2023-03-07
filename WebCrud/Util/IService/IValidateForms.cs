using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Models;

namespace Util.IService
{
    public interface IValidateForms
    {
        Task<ValidateFormsModel> ValitadeAllClients(Clients request);
    }
}
