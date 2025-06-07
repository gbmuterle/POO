namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class ProdutoRepositorioLista : IProdutoRepositorio
    {
        private List<Produto> produtos = new List<Produto>();

        public void Adicionar(Produto produto)
        {
            produtos.Add(produto);
        }

        public void Alterar(Produto produto)
        {
            var produtoExistente = produtos.FirstOrDefault(p => p.Codigo == produto.Codigo);
            if (produtoExistente != null)
            {
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Valor = produto.Valor;
                produtoExistente.Quantidade = produto.Quantidade;
                produtoExistente.Fornecedor = produto.Fornecedor;
            }
        }

        public void Remover(int codigo)
        {
            var produto = produtos.FirstOrDefault(p => p.Codigo == codigo);
            if (produto != null)
            {
                produtos.Remove(produto);
            }
        }

        public Produto BuscarPorCodigo(int codigo)
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