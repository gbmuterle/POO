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

            _repositorioUsuario.Cadastrar(usuario);
        }

        public void AlterarUsuario(Usuario usuario)
        {
            var usuarioExistente = _repositorioUsuario.BuscarPorNome(usuario.Nome);
            if (usuarioExistente == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            if (usuario.Perfil != "admin" && usuario.Perfil != "usuario")
                throw new InvalidOperationException("Perfil inválido. Deve ser 'admin' ou 'usuario'.");

            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Perfil = usuario.Perfil;

            _repositorioUsuario.Alterar(usuarioExistente);
        }

        public void RemoverUsuario(Usuario usuario)
        {
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