using Application;
using Domain.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var cliente = Identificacao();

        int opcao = 0;
        var calculo = new Calculo();

        while (opcao != 3)
        {
            opcao = Menu(cliente, opcao);

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Depósito");
                    var deposito = float.Parse(Console.ReadLine());
                    cliente.Saldo = calculo.Soma(cliente.Saldo, deposito);
                    Console.WriteLine($"Saldo atual é {cliente.Saldo.ToString("N2")}");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Saque");
                    var saque = float.Parse(Console.ReadLine());
                    cliente.Saldo = calculo.Subtracao(cliente.Saldo, saque);
                    Console.WriteLine($"Saldo atual é {cliente.Saldo.ToString("N2")}");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static Cliente Identificacao()
    {
        var cliente = new Cliente();

        Console.WriteLine("Seu número de identificação:");
        cliente.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = Console.ReadLine();
        
        Console.WriteLine("Seu Saldo:");
        cliente.Saldo = float.Parse(Console.ReadLine());
        Console.Clear();
       
        return cliente;
    }

    static int Menu(Cliente cliente, int opcao)
    {
        Console.Clear();
        Console.WriteLine($"Como posso ajudar {cliente.Nome}?");
        Console.WriteLine($"1 - Depósito");
        Console.WriteLine($"2 - Saque");
        Console.WriteLine($"3 - Sair");
        Console.WriteLine("-----------------");
        Console.WriteLine("Selecione uma opção");
        var key = Console.ReadKey();
        Console.WriteLine("");

        if (char.IsDigit(key.KeyChar))
            opcao = int.Parse(key.KeyChar.ToString());
        else
            opcao = 0;
        
        return opcao;
    }
    
}