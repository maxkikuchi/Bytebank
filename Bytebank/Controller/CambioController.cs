using Bytebank.Infrastructure;
using Bytebank.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Controller
{
    public class CambioController : ControllerBase
    {
        private ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioServiceTeste();
        }

        public string MXN()
        {
            var valorConvertido = _cambioService.Calcular("MXN", "BRL", 100.65M);
          
            string conteudoLeitura = View();

            return conteudoLeitura.Replace("VALOR_CONVERTIDO_REAIS", valorConvertido.ToString("0.##"));
        }

        public string USD()
        {
            var valorConvertido = _cambioService.Calcular("USD", "BRL", 100.65M);
            
            string conteudoLeitura = View();

            return conteudoLeitura.Replace("VALOR_CONVERTIDO_REAIS", valorConvertido.ToString("0.##"));
        }

        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorConvertido = _cambioService.Calcular(moedaOrigem, moedaDestino, valor);

            string conteudoLeitura = View();

            return conteudoLeitura
                .Replace("VALOR_ORIGEM", valor.ToString("0.##"))
                .Replace("MOEDA_ORIGEM", moedaOrigem)
                .Replace("VALOR_CONVERTIDO",  $"{valorConvertido.ToString("0.##")} {moedaDestino}");
        }
    }
}
