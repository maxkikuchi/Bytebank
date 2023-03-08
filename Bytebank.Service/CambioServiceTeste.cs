using System;
using System.Collections.Generic;
using System.Text;

namespace Bytebank.Service
{
    public class CambioServiceTeste : ICambioService
    {
        private readonly Random _rdm = new Random();

        public CambioServiceTeste()
        {

        }

        public decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor) =>
            valor * (decimal)_rdm.NextDouble();
        
    }
}
