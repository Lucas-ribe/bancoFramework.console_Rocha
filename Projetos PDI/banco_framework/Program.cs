using Domain.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var pessoa = Identificacao();

        int opcao = 0;

        while (opcao != 3)
        {
            opcao = Menu(pessoa, opcao);

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Depósito");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Saque");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static Pessoa Identificacao()
    {
        var pessoa = new Pessoa();

        Console.WriteLine("Seu número de identificação:");
        pessoa.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        pessoa.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        pessoa.Cpf = Console.ReadLine();
        Console.Clear();
       
        return pessoa;
    }

    static int Menu(Pessoa pessoa, int opcao)
    {
        Console.Clear();
        Console.WriteLine($"Como posso ajudar {pessoa.Nome}?");
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