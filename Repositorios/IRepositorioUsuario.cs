namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioUsuario
    {
        void Cadastrar(Usuario usuario);
        void Alterar(Usuario usuario);
        void Remover(Usuario usuario);
        Usuario? BuscarPorNome(string nome);
        List<Usuario> BuscarTodos();
    }
}