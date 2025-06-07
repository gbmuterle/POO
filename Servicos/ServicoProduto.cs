namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoProduto
    {
        private readonly IProdutoRepositorio _repositorio;

        public ServicoProduto(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(Produto produto)
        {
            _repositorio.Adicionar(produto);
        }

        public void Alterar(Produto produto)
        {
            _repositorio.Alterar(produto);
        }

        public void Remover(int codigo)
        {
            _repositorio.Remover(codigo);
        }

        public Produto BuscarPorCodigo(int codigo)
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