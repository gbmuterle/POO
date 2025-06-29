namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioProdutoVetor : IRepositorioProduto
    {
        private Produto[] produtos = new Produto[0];
        private readonly IArmazenamento<Produto> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioProdutoVetor(IArmazenamento<Produto> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            produtos = _armazenamento.Carregar(_caminhoArquivo).ToArray();
        }


        public void Adicionar(Produto produto)
        {
            var novos = new Produto[produtos.Length + 1];
            for (int i = 0; i < produtos.Length; i++)
                novos[i] = produtos[i];
            novos[novos.Length - 1] = produto;
            produtos = novos;
            _armazenamento.Salvar(produtos, _caminhoArquivo);
        }

        public void Alterar(Produto produtoAtual, Produto produtoAlterado)
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i].Codigo == produtoAlterado.Codigo)
                {
                    produtos[i] = produtoAlterado;
                    _armazenamento.Salvar(produtos, _caminhoArquivo);
                    break;
                }
            }
        }

        public void Remover(Produto produto)
        {
            int index = -1;
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i].Codigo == produto.Codigo)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var novos = new Produto[produtos.Length - 1];
                int j = 0;
                for (int i = 0; i < produtos.Length; i++)
                {
                    if (i != index)
                        novos[j++] = produtos[i];
                }
                produtos = novos;
                _armazenamento.Salvar(produtos, _caminhoArquivo);
            }
        }

        public Produto? BuscarPorCodigo(int codigo)
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i].Codigo == codigo)
                    return produtos[i];
            }
            return null;
        }

        public List<Produto> BuscarTodos()
        {
            var lista = new List<Produto>();
            foreach (var p in produtos)
            {
                if (p != null) lista.Add(p);
            }
            return lista;
        }

        public List<Produto> BuscarPorNome(string nome)
        {
            var lista = new List<Produto>();
            foreach (var p in produtos)
            {
                if (p != null && p.Nome != null && p.Nome.ToLower().Contains(nome.ToLower()))
                    lista.Add(p);
            }
            return lista;
        }
    }
}