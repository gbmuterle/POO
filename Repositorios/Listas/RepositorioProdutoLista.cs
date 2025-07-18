namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioProdutoLista : IRepositorioProduto
    {
        private List<Produto> produtos = new List<Produto>();
        private readonly IArmazenamento<Produto> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioProdutoLista(IArmazenamento<Produto> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            produtos = _armazenamento.Carregar(_caminhoArquivo);
        }

        public void Adicionar(Produto produto)
        {
            produtos.Add(produto);
            Salvar();
        }

        public void Alterar(Produto produtoAtual, Produto produtoAlterado)
        {
            produtoAtual.Nome = produtoAlterado.Nome;
            produtoAtual.Valor = produtoAlterado.Valor;
            produtoAtual.Quantidade = produtoAlterado.Quantidade;
            produtoAtual.Fornecedor = produtoAlterado.Fornecedor;
            Salvar();
        }

        public void Remover(Produto produto)
        {
            produtos.Remove(produto);
            Salvar();
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

        public void Salvar()
        {
            _armazenamento.Salvar(produtos, _caminhoArquivo);
        }
    }
}