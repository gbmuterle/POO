namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly List<Usuario> _usuarios;

        public RepositorioUsuario()
        {
            _usuarios = new List<Usuario>
            {
                new Usuario("admin", "admin", "admin", "", ""),
                new Usuario("user", "user", "usuario", "", "")
            };
        }

        public void Cadastrar(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }

        public void Alterar(Usuario usuarioAtual, Usuario usuarioAlterado)
        {
            usuarioAtual.Senha = usuarioAlterado.Senha;
            usuarioAtual.Perfil = usuarioAlterado.Perfil;
        }

        public void Remover(Usuario usuario)
        {
            _usuarios.Remove(usuario);
        }

        public Usuario? BuscarPorNome(string nome)
        {
            return _usuarios.FirstOrDefault(u => u.Nome == nome);
        }

        public List<Usuario> BuscarTodos()
        {
            return _usuarios;
        }
    }
}