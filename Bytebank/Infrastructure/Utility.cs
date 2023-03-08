using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure
{
    public static class Utility
    {
        public static bool EhArquivo(string path)
        {
            var partesPath = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return partesPath.Last().Contains('.');
        }

        public static string ConverterPathParaNomeAssembly(string path)
        {
            string prefixoAssembly = "Bytebank";
            string pathComPontos = path.Replace("/", ".");

            return $"{prefixoAssembly}{pathComPontos}";
        }

        public static string ObterTipoDeConteudo(string path)
        {
            if (path.ToUpper().EndsWith(".CSS"))
                return "text/css; charset=utf-8";

            if (path.ToUpper().EndsWith(".JS"))
                return "text/js; charset=utf-8";

            if (path.ToUpper().EndsWith(".HTML") || !EhArquivo(path))
                return "text/html; charset=utf-8";

            if (path.ToUpper().EndsWith(".ICO"))
                return "application/html; charset=utf-8";
                       
            throw new NotImplementedException("Tipo de conteúdo não previsto!");
        }
    }
}
