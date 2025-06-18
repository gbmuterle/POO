namespace Modelos
{
    public class Usuario
    {
        public string Nome { get; set; } = "";
        public string Senha { get; set; } = "";
        public string Perfil { get; set; } = "";

        public Usuario(string nome, string senha, string perfil)
        {
            Nome = nome;
            Senha = senha;
            Perfil = perfil;
        }
        public override string ToString()
        {
            return $"Nome: {Nome}, Perfil: {Perfil}";
        }
    }
}