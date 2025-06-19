namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioProdutoLista : IRepositorioProduto
    {
        private List<Produto> produtos = new List<Produto>();

        public void Adicionar(Produto produto)
        {
            produtos.Add(produto);
        }

        public void Alterar(Produto produtoAtual, Produto produtoAlterado)
        {
                produtoAtual.Nome = produtoAlterado.Nome;
                produtoAtual.Valor = produtoAlterado.Valor;
                produtoAtual.Quantidade = produtoAlterado.Quantidade;
                produtoAtual.Fornecedor = produtoAlterado.Fornecedor;
        }

        public void Remover(Produto produto)
        {
            produtos.Remove(produto);
        }

        public Produto? BuscarPorCodigo(int codigo)
        {
            return produtos.FirstOrDefault(p => p.Codigo == codigo);
        }

        public List<Produto> BuscarTodos()
        {
            return new List<Produto>(produtos);
        }

        public List<Produto> BuscarPorNome(string nome)
        {
            return produtos
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .ToList();
        }
    }
}