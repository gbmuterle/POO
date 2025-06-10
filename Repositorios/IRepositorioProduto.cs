namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioProduto
    {
        void Adicionar(Produto produto);
        void Alterar(Produto produto);
        void Remover(int codigo);
        Produto? BuscarPorCodigo(int codigo);
        List<Produto> BuscarTodos();
        List<Produto> BuscarPorNome(string nome);
    }
}