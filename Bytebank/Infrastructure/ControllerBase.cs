using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName]string nomeArquivo = null)
        {
            var type = GetType();
            string nomeDiretorio = $"{type.Name.Replace("Controller", string.Empty)}";

            var nomeCompletoResource = $"Bytebank.View.{nomeDiretorio}.{nomeArquivo}.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);

            var streamLeitura = new StreamReader(streamRecurso);
            return streamLeitura.ReadToEnd();
        }
    }
}
