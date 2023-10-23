using Application;
using CpfCnpjLibrary;
using Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Repository;

internal class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var clienteService = serviceProvider.GetService<IClienteRepository>();

        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var cliente = Identificacao(clienteService);
        
        int opcao = 0;
        var calculadora = new Calculo();

        while (opcao != 3)
        {
            opcao = Menu(cliente, opcao);
            
            switch (opcao)
            {
                case 1:
                    Deposito(cliente, calculadora, clienteService);
                    break;
                case 2:
                    Saque(cliente, calculadora, clienteService);
                    break;
            }
        }
        
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
    }

    static Cliente Identificacao(IClienteRepository clienteService)
    {
        var cliente = new Cliente();

        List<string> errosCount = new List<string>();
        errosCount.Add(" ");

        while (errosCount.Count() != 0)
        {
            errosCount.Clear();

            Console.WriteLine("Seu número de identificação:");

            var id = Console.ReadLine();

            if (!int.TryParse(id, out _))
            {
                errosCount.Add("Identificador não é válido");
            }
            else
            {
                cliente.Id = int.Parse(id);

                var client = clienteService.Get(cliente.Id);
                if (client is not null)
                    return client;
            }

            IdentificacaoNovoCliente(cliente, int.Parse(id), clienteService, errosCount);

            Console.WriteLine();

            if (errosCount.Count() > 0)
            {
                foreach (string erro in errosCount)
                {
                    Console.WriteLine(erro);
                }
            }

            Console.ReadKey();

            Console.Clear();
        }

        clienteService.Insert(cliente);

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

    static Cliente IdentificacaoNovoCliente(Cliente cliente, int id, IClienteRepository clienteService, List<string> erros)
    {
        cliente.Id = id;
           
        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        ClienteCpf(cliente, clienteService, erros);

        Saldo(cliente, clienteService, erros);

        return cliente;
    }

    static Cliente ClienteCpf(Cliente cliente, IClienteRepository clienteService, List<string> erros)
    {
        Console.WriteLine("Seu CPF:");

        cliente.Cpf = Console.ReadLine();

        cliente.Cpf = Cpf.FormatarSemPontuacao(cliente.Cpf);

        if (Cpf.Validar(cliente.Cpf).Equals(false))
        {
            Erros(erros, "CPF digitado não é válido");
        }

        return cliente;
    }

    static Cliente Saldo(Cliente cliente, IClienteRepository clienteService, List<string> erros)
    {
        Console.WriteLine("Seu Saldo:");

        var saldo = Console.ReadLine();

        if (!float.TryParse(saldo, out _))
        {
            Erros(erros, "Saldo não é válido");
        }
        else
        {
            var conversaoSaldo = float.Parse(saldo);

            if (conversaoSaldo < 0)
                Erros(erros, "Saldo não é válido");
            else
                cliente.Saldo = conversaoSaldo;
        }

        return cliente;
    }

    static Cliente Deposito(Cliente cliente, Calculo calculadora, IClienteRepository clienteService)
    {
        Console.WriteLine("Depósito");

        Console.Write("Digite o valor: ");

        var deposito = Console.ReadLine();

        if (!float.TryParse(deposito, out _))
        {
            Console.WriteLine("Valor digitado é invalido: digite um valor numerico");
            Console.ReadKey();
            return cliente;
        }
        else
        {
            var valorConvertido = float.Parse(deposito);

            if (valorConvertido < 0)
            {
                Console.WriteLine("Valor digitado é invalido: digite um valor positivo");
                Console.ReadKey();
                return cliente;
            }
            else
                cliente.Saldo = calculadora.Soma(cliente.Saldo, valorConvertido);
        }

        clienteService.Update(cliente);

        Console.WriteLine($"Saldo atual é {clienteService.Get(cliente.Id).Saldo.ToString("N2")}");

        Console.ReadLine();

        return cliente;
    }

    static Cliente Saque(Cliente cliente, Calculo calculadora, IClienteRepository clienteService)
    {
        Console.WriteLine("Saque");

        Console.Write("Digite o valor: ");

        var saque = Console.ReadLine();

        if (!float.TryParse(saque, out _))
        {
            Console.WriteLine("Valor digitado é invalido: digite um valor numerico");
            Console.ReadKey();
            return cliente;
        }
        else
        {
            var valorConvertido = float.Parse(saque);

            if (valorConvertido < 0)
            {
                Console.WriteLine("Valor digitado é invalido: digite um valor positivo");
                Console.ReadKey();
                return cliente;
            }
            else
                cliente.Saldo = calculadora.Subtracao(cliente.Saldo, valorConvertido);
        }

        clienteService.Update(cliente);

        Console.WriteLine($"Saldo atual é {clienteService.Get(cliente.Id).Saldo.ToString("N2")}");

        Console.ReadLine();

        return cliente;
    }

    static List<string> Erros(List<string> erros, string erro)
    {
        erros.Add(erro);
        return erros;
    }
}