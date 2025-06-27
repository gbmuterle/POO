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
            var enderecoPadrao = new Endereco(
                "Rua", "1234", "Sede ERP", "Centro", "Flores da Cunha", "RS", "95270000"
            );

            _usuarios = new List<Usuario>
            {
                new Usuario("admin", "admin", "admin", "11999999999", "admin@email.com", enderecoPadrao),
                new Usuario("user", "user", "usuario", "11999999999", "cliente@email.com", enderecoPadrao)
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