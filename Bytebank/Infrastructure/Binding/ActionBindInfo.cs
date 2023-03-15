using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Infrastructure.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<ArgumentoNomeValor> ArgumentosNomeValor { get; private set; }

        public ActionBindInfo(MethodInfo methodInfo, IEnumerable<ArgumentoNomeValor> argumentosNomeValor)
        {
            MethodInfo = methodInfo ?? throw new ArgumentException(nameof(methodInfo));

            if (argumentosNomeValor == null)
                throw new ArgumentException(nameof(argumentosNomeValor));

            ArgumentosNomeValor = new ReadOnlyCollection<ArgumentoNomeValor>(argumentosNomeValor.ToList());
        }

        public object Invoke(object controller)
        {
            var argumentos = ArgumentosNomeValor;
            var parametros = MethodInfo.GetParameters();
            object[] parametrosInvoke = new object[parametros.Length];

            if (argumentos == null || argumentos.Count == 0)
            {
                return MethodInfo.Invoke(controller, new object[0]);
            }
            
            for (int i = 0; i < parametros.Length; i++)
            {
                var nomeParametro = parametros[i].Name;
                var argumento = argumentos.Single(a => a.Nome == nomeParametro);

                parametrosInvoke[i] = Convert.ChangeType(argumento.Valor, parametros[i].ParameterType);
            }

            return MethodInfo.Invoke(controller, parametrosInvoke);
        }

    }
}
