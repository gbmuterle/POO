namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoProduto
    {
        private readonly IRepositorioProduto _repositorio;

        public ServicoProduto(IRepositorioProduto repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(Produto produto)
        {
            Validar(produto, true);
            _repositorio.Adicionar(produto);
        }

        public void Alterar(Produto produtoAtual, Produto produtoAlterado)
        {
            Validar(produtoAlterado, false);
            _repositorio.Alterar(produtoAtual, produtoAlterado);
        }

        public void Remover(Produto produto)
        {
            if (produto == null)
                throw new InvalidOperationException("Produto não pode ser nulo.");
            _repositorio.Remover(produto);
        }

        public Produto? BuscarPorCodigo(int codigo)
        {
            return _repositorio.BuscarPorCodigo(codigo);
        }

        public List<Produto> ListarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public List<Produto> BuscarPorNome(string nome)
        {
            return _repositorio.BuscarPorNome(nome);
        }

        private void Validar(Produto produto, bool novo)
        {
            if (produto == null)
                throw new InvalidOperationException("Produto inválido.");

            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new InvalidOperationException("Nome do produto inválido.");

            if (produto.Valor < 0)
                throw new InvalidOperationException("Valor do produto não pode ser negativo.");

            if (produto.Quantidade < 0)
                throw new InvalidOperationException("Quantidade não pode ser negativa.");

            if (produto.Fornecedor == null)
                throw new InvalidOperationException("Fornecedor não pode ser nulo.");

            if (novo)
            {
                if (BuscarPorCodigo(produto.Codigo) != null)
                    throw new InvalidOperationException("Já existe um produto com esse código.");
            }
            else
            {
                if (BuscarPorCodigo(produto.Codigo) == null)
                    throw new InvalidOperationException("Produto não encontrado para alteração.");
            }
        }

        public void BaixarEstoque(List<ItemPedido> itens)
        {
            foreach (var item in itens)
            {
                var produtoAlterado = BuscarPorCodigo(item.Produto.Codigo);
                if (produtoAlterado == null)
                    throw new InvalidOperationException($"Produto não encontrado.");

                if (produtoAlterado.Quantidade < item.Quantidade)
                    throw new InvalidOperationException($"Estoque insuficiente para o produto {produtoAlterado.Nome}.");

                produtoAlterado.Quantidade -= item.Quantidade;
                _repositorio.Alterar(item.Produto, produtoAlterado);
            }
        }

        public void ReporEstoque(List<ItemPedido> itens)
        {
            foreach (var item in itens)
            {
                var produtoAlterado = BuscarPorCodigo(item.Produto.Codigo);
                if (produtoAlterado == null)
                    throw new InvalidOperationException($"Produto não encontrado.");

                produtoAlterado.Quantidade += item.Quantidade;
                _repositorio.Alterar(item.Produto, produtoAlterado);
            }
        }
    }
}