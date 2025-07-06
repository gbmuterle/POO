namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioProduto
    {
        void Adicionar(Produto produto);
        void Alterar(Produto produtoAtual, Produto produtoAlterado);
        void Remover(Produto produto);
        Produto? BuscarPorCodigo(int codigo);
        List<Produto> BuscarTodos();
        List<Produto> BuscarPorNome(string nome);
        void Salvar();
    }
}