using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Models
{    
    public class ClientsModel
    {
        public List<Clients> clients { get; set; }
    }

    public class Clients
    {
        public int codcliente { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string datainsercao { get; set; }
    }
}
