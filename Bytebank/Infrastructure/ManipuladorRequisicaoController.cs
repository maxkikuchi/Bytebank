using Bytebank.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure
{
    public class ManipuladorRequisicaoController
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];
            var actionNome = partes[1];

            string baseAssembly = "Bytebank.Controller";
            string controllerNomeCompleto = $"{baseAssembly}.{controllerNome}Controller";

            if (Assembly.GetExecutingAssembly().GetType(controllerNomeCompleto) != null && Assembly.GetExecutingAssembly().GetType(controllerNomeCompleto).GetMember(actionNome).Count() > 0)
            {
                var controllerWrapper = Activator.CreateInstance("Bytebank", controllerNomeCompleto, new object[0]);
                var controllerUnwrap = controllerWrapper.Unwrap();

                var methodInfo = controllerUnwrap.GetType().GetMethod(actionNome);
                var resultadoAction = (string)methodInfo.Invoke(controllerUnwrap, new object[0]);

                resposta.ContentType = Utility.ObterTipoDeConteudo(path);
                resposta.StatusCode = 200;
                string paginaConteudo = resultadoAction;
                byte[] bufferArquivo;

                bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                resposta.ContentLength64 = bufferArquivo.Length;
                resposta.OutputStream.Write(bufferArquivo, 0, (int)bufferArquivo.Length);
            }
            else
            {
                resposta.StatusCode = 404;
            }

            resposta.OutputStream.Close();
        }
    }
}
