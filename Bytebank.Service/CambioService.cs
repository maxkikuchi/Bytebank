using System;
using System.Collections.Generic;
using System.Text;

namespace Bytebank.Service
{
    public class CambioService : ICambioService
    {
        private readonly Random _rdm = new Random();

        public decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor)
        {
            return valor * (decimal)_rdm.NextDouble();
        }
    }
}
