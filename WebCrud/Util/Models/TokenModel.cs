using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
