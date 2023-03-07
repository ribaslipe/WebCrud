using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.IService;
using Util.Models;

namespace Util.Service
{
    public class ValidateForms : IValidateForms
    {

        public async Task<ValidateFormsModel> ValitadeAllClients(Clients request)
        {
            var msg = "";

            if (String.IsNullOrEmpty(request.nome) || request.nome.Length > 500)
            {
                msg = "Nome não pode ser vazio ou maior de 500 caracteres.";
            }

            if (String.IsNullOrEmpty(request.endereco) || request.nome.Length > 1000)
            {
                msg += "\nEndereço não pode ser vazio ou maior de 1000 caracteres.";
            }

            if (String.IsNullOrEmpty(request.cidade) || request.nome.Length > 200)
            {
                msg += "\nCidade não pode ser vazia ou maior de 200 caracteres";
            }

            if (String.IsNullOrEmpty(request.uf) || request.uf.Length > 2)
            {
                msg += "\nUF não pode está vazia ou maior que 2 caracteres";
            }

            if (!String.IsNullOrEmpty(msg))
            {
                var result = new ValidateFormsModel() { message = msg, success = false };
                return await Task.FromResult(result);
            }
            else
            {
                var result = new ValidateFormsModel() { success = true };
                return await Task.FromResult(result);
            }


        }     

    }
}
