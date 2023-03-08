using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure
{
    public class ManipuladorRequisicaoArquivo
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            string contentType = string.Empty;
            string nomeResource = string.Empty;

            switch (path)
            {
                case "/favicon.ico":
                    string fullPath = $"/Assets/images{path}";

                    nomeResource = Utility.ConverterPathParaNomeAssembly(fullPath);
                    contentType = Utility.ObterTipoDeConteudo(fullPath);
                    
                    break;
                default:
                    nomeResource = Utility.ConverterPathParaNomeAssembly(path);
                    contentType = Utility.ObterTipoDeConteudo(path);
                    
                    break;
            }

            var assembly = Assembly.GetExecutingAssembly();

            var resourceStream = assembly.GetManifestResourceStream(nomeResource);

            if (resourceStream == null)
            {
                resposta.StatusCode = 404;
            }
            else
            {
                int streamLength = (int)resourceStream.Length;
                var bytesResource = new byte[streamLength];

                using (resourceStream)
                {
                    resourceStream.Read(bytesResource, 0, streamLength);
                }

                resposta.ContentType = contentType;
                resposta.StatusCode = 200;
                resposta.ContentLength64 = streamLength;
                resposta.OutputStream.Write(bytesResource, 0, streamLength);
            }

            resposta.OutputStream.Close();
        }
    }
}
