using Bytebank.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure
{
    public class ManipuladorRequisicaoController
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            resposta.ContentType = Utility.ObterTipoDeConteudo(path);
            resposta.StatusCode = 200;
            CambioController controller;
            string paginaConteudo = string.Empty;
            byte[] bufferArquivo;

            switch (path.ToUpper())
            {
                case "/CAMBIO/MXN":
                    controller = new CambioController();
                    paginaConteudo = controller.MXN();
                    resposta.StatusCode = 200;

                    break;
                case "/CAMBIO/USD":
                    controller = new CambioController();
                    paginaConteudo = controller.USD();
                    resposta.StatusCode = 200;

                    break;
                default:
                    resposta.StatusCode = 404;
                    break;
            }

            if (resposta.StatusCode == 200)
            {
                bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                resposta.ContentLength64 = bufferArquivo.Length;
                resposta.OutputStream.Write(bufferArquivo, 0, (int)bufferArquivo.Length);
            }

            resposta.OutputStream.Close();
        }
    }
}
