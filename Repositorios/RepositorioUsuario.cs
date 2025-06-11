using Modelos;

namespace Repositorios
{
    public class RepositorioUsuario
    {
        private readonly List<Usuario> _usuarios;

        public RepositorioUsuario()
        {
            _usuarios = new List<Usuario>
            {
                new Usuario { Nome = "admin", Senha = "admin123", Perfil = "admin" },
                new Usuario { Nome = "user", Senha = "user123", Perfil = "usuario" }
            };
        }

        public List<Usuario> ObterTodos()
        {
            return _usuarios;
        }
    }
}