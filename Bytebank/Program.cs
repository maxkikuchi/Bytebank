using Bytebank.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixos = new string[] { "http://localhost:5341/", "http://localhost:5342/" };

            var webApplication = new WebApplication(prefixos);
            webApplication.Iniciar();
        }
    }
}
