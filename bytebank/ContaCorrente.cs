using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytebank
{
    public class ContaCorrente
    {
        public ContaCorrente()
        {

        }

        private int _myProperty;

        public int MyProperty 
        { 
            get 
            { 
                return _myProperty; 
            } 
            
            set 
            { 
                _myProperty = value; 
            } 
        }


        public string titular;
        public string conta;
        public string agencia;
        public double saldo;

        public void Depositar(double valor)
        {
            this.saldo += valor;
        }

        public bool Sacar(double valor)
        {
            if (valor > this.saldo)
                return false;
            else
                this.saldo -= valor;

            return true;
        }

        public bool Transferir(double valor, ContaCorrente contaDestino)
        {
            if (valor > this.saldo)
            {
                return false;
            }
            else
            {
                this.saldo -= valor;
                contaDestino.saldo += valor;
                return true;
            }
        }
    }
}
