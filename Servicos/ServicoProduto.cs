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
            if (produto == null)
                throw new InvalidOperationException("Produto não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new InvalidOperationException("Nome do produto não pode ser vazio.");

            if (produto.Valor < 0)
                throw new InvalidOperationException("Valor do produto não pode ser negativo.");

            if (produto.Quantidade < 0)
                throw new InvalidOperationException("Quantidade não pode ser negativa.");

            if (produto.Fornecedor == null)
                throw new InvalidOperationException("Fornecedor não pode ser nulo.");

            if (_repositorio.BuscarPorCodigo(produto.Codigo) != null)
                throw new InvalidOperationException("Já existe um produto com esse código.");

            _repositorio.Adicionar(produto);
        }

        public void Alterar(Produto produtoAtual, Produto produtoAlterado)
        {
            if (produtoAlterado == null)
                throw new InvalidOperationException("Produto não pode ser nulo.");

            if (produtoAlterado == null)
                throw new InvalidOperationException("Produto não encontrado.");

            if (string.IsNullOrWhiteSpace(produtoAlterado.Nome))
                throw new InvalidOperationException("Nome do produto não pode ser vazio.");

            if (produtoAlterado.Valor < 0)
                throw new InvalidOperationException("Valor do produto não pode ser negativo.");

            if (produtoAlterado.Quantidade < 0)
                throw new InvalidOperationException("Quantidade não pode ser negativa.");

            if (produtoAlterado.Fornecedor == null)
                throw new InvalidOperationException("Fornecedor não pode ser nulo.");

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
    }
}