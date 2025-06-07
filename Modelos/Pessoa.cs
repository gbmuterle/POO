using System;

namespace Modelos
{
    public abstract class Pessoa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Pessoa(int codigo, string nome, string telefone, string email)
        {
            Codigo = codigo;
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Nome: {Nome}, Telefone: {Telefone}, Email: {Email}";
        }
    }
}