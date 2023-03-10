using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure.Binding
{
    public class ActionBinder
    {
        public MethodInfo ObterMethodInfo(object controller, string path)
        {
            int indexInterrogacao = path.IndexOf('?');

            //Recuperar nome Controller e nome Action
            var partesControllerActionNome = path.Substring(0, indexInterrogacao).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var nomeController = partesControllerActionNome[0];
            var nomeAction = partesControllerActionNome[1];

            //Recuperar a query string quando exisitr
            if (indexInterrogacao > 0)
            {
                var queryString = path.Substring(indexInterrogacao + 1);
                var listaArgumentos = ObterArgumentoNomeValores(queryString);
            }
            else
            {
                return controller.GetType().GetMethod(nomeAction);
            }
        }

        private IEnumerable<ArgumentoNomeValor> ObterArgumentoNomeValores(string queryString)
        {
            var partesQueryString = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var parteQueryString in partesQueryString)
            {
                var partesArgumento = parteQueryString.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                yield return new ArgumentoNomeValor(partesArgumento[0], partesArgumento[1]);
            }
        }
    }
}
