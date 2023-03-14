using Bytebank.Controller;
using Bytebank.Infrastructure.Binding;
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
        ActionBinder _actionBinder = new ActionBinder();

        public void Manipular(HttpListenerResponse resposta, string path)
        {

            var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];

            string baseAssembly = "Bytebank.Controller";
            string controllerNomeCompleto = $"{baseAssembly}.{controllerNome}Controller";

            if (Assembly.GetExecutingAssembly().GetType(controllerNomeCompleto) != null)
            {
                var controllerWrapper = Activator.CreateInstance("Bytebank", controllerNomeCompleto, new object[0]);
                var controller = controllerWrapper.Unwrap();

                //var methodInfo = controller.GetType().GetMethod(actionNome);
                var methodInfo = _actionBinder.ObterMethodInfo(controller, path);


                var resultadoAction = (string)methodInfo.Invoke(controller, new object[0]);

                byte[] bufferArquivo;
                bufferArquivo = Encoding.UTF8.GetBytes(resultadoAction);
                resposta.ContentLength64 = bufferArquivo.Length;
                resposta.ContentType = Utility.ObterTipoDeConteudo(path);
                resposta.StatusCode = 200;
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
