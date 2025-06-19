namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuario não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new InvalidOperationException("Nome não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                throw new InvalidOperationException("Senha não pode ser vazia.");

            if (usuario.Perfil != "admin" && usuario.Perfil != "usuario")
                throw new InvalidOperationException("Perfil inválido. Deve ser 'admin' ou 'usuario'.");
            
            if (_repositorioUsuario.BuscarPorNome(usuario.Nome) != null)
                throw new InvalidOperationException("Já existe um usuário com esse nome.");

            _repositorioUsuario.Cadastrar(usuario);
        }

        public void AlterarUsuario(Usuario usuarioAtual, Usuario usuarioAlterado)
        {
            if (usuarioAlterado == null)
                throw new InvalidOperationException("Usuário não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(usuarioAlterado.Senha))
                throw new InvalidOperationException("Senha não pode ser vazia.");

            if (usuarioAlterado.Perfil != "admin" && usuarioAlterado.Perfil != "usuario")
                throw new InvalidOperationException("Perfil inválido. Deve ser 'admin' ou 'usuario'.");
                
            _repositorioUsuario.Alterar(usuarioAtual, usuarioAlterado);
        }

        public void RemoverUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuário não pode ser nulo.");
                
            _repositorioUsuario.Remover(usuario);
        }

        public List<Usuario> BuscarTodos()
        {
            return _repositorioUsuario.BuscarTodos();
        }

        public Usuario? BuscarPorNome(string nome)
        {
            return _repositorioUsuario.BuscarPorNome(nome);
        }
    }
}