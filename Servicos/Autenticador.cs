using Modelos;

namespace Autenticacao
{
    public class Autenticador
    {
        private readonly List<Usuario> _usuarios;

        public Autenticador(List<Usuario> usuarios)
        {
            _usuarios = usuarios;
        }

        public Usuario? Autenticar(string nome, string senha)
        {
            return _usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
        }
    }
}