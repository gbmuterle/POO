using Modelos;

class Program
{
    static void Main()
    {
        Pessoa p = new Pessoa();
        p.Nome = "Maria";
        p.Idade = 28;

        Console.WriteLine($"Nome: {p.Nome}, Idade: {p.Idade}");
    }
}