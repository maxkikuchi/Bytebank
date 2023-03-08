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
    public class CambioController
    {
        private ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioServiceTeste();
        }

        public string MXN()
        {
            var valorConvertido = _cambioService.Calcular("MXN", "BRL", 100.65M);
            var nomeCompletoResource = "Bytebank.View.Cambio.MXN.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);

            var streamLeitura = new StreamReader(streamRecurso);
            string conteudoLeitura = streamLeitura.ReadToEnd();

            return conteudoLeitura.Replace("VALOR_CONVERTIDO_REAIS", valorConvertido.ToString("0.##"));
        }

        public string USD()
        {
            var valorConvertido = _cambioService.Calcular("USD", "BRL", 100.65M);
            var nomeCompletoResource = "Bytebank.View.Cambio.USD.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);

            var streamLeitura = new StreamReader(streamRecurso);
            string conteudoLeitura = streamLeitura.ReadToEnd();

            return conteudoLeitura.Replace("VALOR_CONVERTIDO_REAIS", valorConvertido.ToString("0.##"));
        }
    }
}
