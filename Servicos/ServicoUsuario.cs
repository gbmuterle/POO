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

        public void Cadastrar(Usuario usuario)
        {
            Validar(usuario, true);
            _repositorioUsuario.Cadastrar(usuario);
        }

        public void Alterar(Usuario usuarioAtual, Usuario usuarioAlterado)
        {
            Validar(usuarioAlterado, false);
            _repositorioUsuario.Alterar(usuarioAtual, usuarioAlterado);
        }

        public void Remover(Usuario usuario)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuário inválido.");
            _repositorioUsuario.Remover(usuario);
        }

        public List<Usuario> ListarTodos()
        {
            return _repositorioUsuario.ListarTodos();
        }

        public Usuario? BuscarPorNome(string nome)
        {
            return _repositorioUsuario.BuscarPorNome(nome);
        }
        private void Validar(Usuario usuario, bool novo)
        {
            if (usuario == null)
                throw new InvalidOperationException("Usuário inválido");

            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new InvalidOperationException("Nome do usuário inválido.");

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                throw new InvalidOperationException("Senha inválida.");
            
            if (string.IsNullOrWhiteSpace(usuario.Telefone))
                throw new InvalidOperationException("Telefone inválido.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new InvalidOperationException("E-mail inválido.");

            if (usuario.Perfil != "admin" && usuario.Perfil != "cliente")
                throw new InvalidOperationException("Perfil inválido.");

            if (novo)
            {
                if (BuscarPorNome(usuario.Nome) != null)
                    throw new InvalidOperationException("Já existe um usuário com esse nome.");
            }
            else
            {
                if (BuscarPorNome(usuario.Nome) == null)
                    throw new InvalidOperationException("Usuário não encontrado para alteração.");
            }
        }
    }
}