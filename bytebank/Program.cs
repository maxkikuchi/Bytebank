// See https://aka.ms/new-console-template for more information


using bytebank;

bool finalizar = true;

ContaCorrente contaMax = new ContaCorrente()
{
    agencia = "4092",
    conta = "001",
    saldo = 1000
};

ContaCorrente contaDestino = new ContaCorrente()
{
    agencia = "4092",
    conta = "002",
    saldo = 0
};

while (finalizar)
{
    Console.WriteLine("1-Sacar");
    Console.WriteLine("2-Depositar");
    Console.WriteLine("3-Transferir");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("Informe o valor (saque):");
            double.TryParse(Console.ReadLine(), out double valorSaque);
            if (!contaMax.Sacar(valorSaque))
                Console.WriteLine("Saldo insuficiente!");
            break;
        case "2":
            Console.WriteLine("Informe o valor (depósito):");
            double.TryParse(Console.ReadLine(), out double valorDeposito);
            contaMax.Depositar(valorDeposito);
            break;
        case "3":
            Console.WriteLine("Informe o valor (transferência):");
            double.TryParse(Console.ReadLine(), out double valorTransferir);
            if (!contaMax.Transferir(valorTransferir, contaDestino))
                Console.WriteLine("Saldo insuficiente!");
            break;
        default:
            break;
    }

    Console.WriteLine(string.Format("Valor saldo Max: {0}", contaMax.saldo.ToString("0.00")));
    Console.WriteLine(string.Format("Valor saldo Destino: {0}", contaDestino.saldo.ToString("0.00")));

    Console.WriteLine("Deseja continuar? (S/N)");
    if (Console.ReadLine() == "N")
    {
        break;
    }
};



