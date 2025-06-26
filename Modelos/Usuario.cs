namespace Modelos
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }

        public Usuario(string nome, string senha, string perfil, string telefone, string email, Endereco endereco)
        {
            Nome = nome;
            Senha = senha;
            Perfil = perfil;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Perfil: {Perfil}, Telefone: {Telefone}, Email: {Email}\nEndereço: {Endereco}";
        }
    }
}