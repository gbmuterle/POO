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
                new Usuario("admin", "admin", "admin")
            };
        }

        public void Cadastrar(Usuario usuario)
        {
            if (_usuarios.Any(u => u.Nome == usuario.Nome))
                throw new InvalidOperationException("Já existe um usuário com esse nome.");

            _usuarios.Add(usuario);
        }

        public void Alterar(Usuario usuario)
        {
            var usuarioExistente = BuscarPorNome(usuario.Nome);
            if (usuarioExistente == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Perfil = usuario.Perfil;
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