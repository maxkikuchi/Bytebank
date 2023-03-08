using Bytebank.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure
{
    public class WebApplication
    {
        private readonly string[] _prefixos;

        public WebApplication(string[] prefixos)
        {
            _prefixos = prefixos ?? throw new ArgumentNullException(nameof(prefixos));
        }

        public void Iniciar()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
            {
                httpListener.Prefixes.Add(prefixo);
            }

            httpListener.Start();

            while (true)
            {

                var contexto = httpListener.GetContext();
                string nomeResource = string.Empty;
                string contentType = string.Empty;
                var requisicao = contexto.Request;
                var path = requisicao.Url.AbsolutePath;

                if (Utility.EhArquivo(path))
                {
                    switch (path)
                    {
                        case "/favicon.ico":
                            string fullPath = $"/Assets/images{path}";

                            nomeResource = Utility.ConverterPathParaNomeAssembly(fullPath);
                            contentType = Utility.ObterTipoDeConteudo(fullPath);
                            ExtractOutputStreamFile(contexto, nomeResource, contentType);

                            break;
                        default:
                            nomeResource = Utility.ConverterPathParaNomeAssembly(path);
                            contentType = Utility.ObterTipoDeConteudo(path);
                            ExtractOutputStreamFile(contexto, nomeResource, contentType);
                            break;
                    }
                }
                else
                {
                    TratarActionController(path, contexto);
                }
            }
                       
            
            //httpListener.Stop();
        }

        private static void TratarActionController(string path, HttpListenerContext contexto)
        {
            var resposta = contexto.Response;
            resposta.ContentType = Utility.ObterTipoDeConteudo(path);
            resposta.StatusCode = 200;
            CambioController controller;
            string paginaConteudo;
            byte[] bufferArquivo;

            switch (path.ToUpper())
            {
                case "/CAMBIO/MXN":
                    controller = new CambioController();
                    paginaConteudo = controller.MXN();

                    bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                    resposta.ContentLength64 = bufferArquivo.Length;
                    resposta.OutputStream.Write(bufferArquivo, 0, (int)bufferArquivo.Length);

                    break;
                case "/CAMBIO/USD":
                    controller = new CambioController();
                    paginaConteudo = controller.USD();

                    bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                    resposta.ContentLength64 = bufferArquivo.Length;
                    resposta.OutputStream.Write(bufferArquivo, 0, (int)bufferArquivo.Length);

                    break;
                default:
                    resposta.StatusCode = 404;
                    break;
            }

            resposta.OutputStream.Close();
        }

        private void ExtractOutputStreamFile(HttpListenerContext contexto, string nomeResource, string contentType)
        {
            var resposta = contexto.Response;
            var assembly = Assembly.GetExecutingAssembly();

            var resourceStream = assembly.GetManifestResourceStream(nomeResource);

            if (resourceStream == null)
            {
                resposta.StatusCode = 404;
            }
            else
            {
                var bytesResource = new byte[resourceStream.Length];

                resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);
                resposta.ContentType = contentType;
                resposta.StatusCode = 200;
                resposta.ContentLength64 = resourceStream.Length;
                resposta.OutputStream.Write(bytesResource, 0, (int)resourceStream.Length);
            }

            resposta.OutputStream.Close();
        }
    }
}
