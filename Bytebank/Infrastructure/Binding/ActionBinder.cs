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
        public ActionBindInfo ObterActionBindInfo(object controller, string path)
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
                var argumentos = ObterArgumentoNomeValores(queryString);
                var nomeArgumentos = argumentos.Select(a => a.Nome).ToArray();

                var methodInfo = ObterMethodInfoAPartirDeNomeEArgumentos(nomeAction, nomeArgumentos, controller);
                
                return new ActionBindInfo(methodInfo, argumentos);
            }
            else
            {
                var methodInfo = controller.GetType().GetMethod(nomeAction);

                return new ActionBindInfo(methodInfo, Enumerable.Empty<ArgumentoNomeValor>());
            }
        }

        private MethodInfo ObterMethodInfoAPartirDeNomeEArgumentos(string nomeAction, string[] argumentos, object controller )
        {

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic;
            var methodsInfo = controller.GetType().GetMethods(bindingFlags);


            var overrides = methodsInfo.Where(m => m.Name == nomeAction);

            foreach (var methodInfo in overrides)
            {
                var parameters = methodInfo.GetParameters();

                if (parameters.Count() != argumentos.Count())
                    continue;

                var math = parameters.All(p => argumentos.Contains(p.Name));

                if (math)
                    return methodInfo;

            }

            throw new ArgumentException($"A sobrecarga do método {nomeAction} não foi encontrada!");
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