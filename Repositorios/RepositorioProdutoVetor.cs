namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioProdutoVetor : IRepositorioProduto
    {
        private Produto[] produtos = new Produto[0];

        public void Adicionar(Produto produto)
        {
            var novos = new Produto[produtos.Length + 1];
            for (int i = 0; i < produtos.Length; i++)
                novos[i] = produtos[i];
            novos[novos.Length - 1] = produto;
            produtos = novos;
        }

        public void Alterar(Produto produto)
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i].Codigo == produto.Codigo)
                {
                    produtos[i] = produto;
                    break;
                }
            }
        }

        public void Remover(int codigo)
        {
            int index = -1;
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i].Codigo == codigo)
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
            }
        }

        public Produto? BuscarPorCodigo(int codigo)
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                if (produtos[i].Codigo == codigo)
                    return produtos[i];
            }
            // Não encontrado
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