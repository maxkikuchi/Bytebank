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
                var requisicao = contexto.Request;
                var path = requisicao.Url.AbsolutePath;

                if (Utility.EhArquivo(path))
                {
                    ManipuladorRequisicaoArquivo manipuladorRequisicaoArquivo = new ManipuladorRequisicaoArquivo();
                    manipuladorRequisicaoArquivo.Manipular(contexto.Response, path);
                }
                else
                {
                    ManipuladorRequisicaoController manipuladorRequisicaoController = new ManipuladorRequisicaoController();
                    manipuladorRequisicaoController.Manipular(contexto.Response, path);
                }
            }
            
            //httpListener.Stop();
        }
    }
}
