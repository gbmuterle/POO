namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoFornecedor
    {
        private readonly IFornecedorRepositorio _repositorio;

        public ServicoFornecedor(IFornecedorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(Fornecedor fornecedor)
        {
            _repositorio.Adicionar(fornecedor);
        }

        public void Alterar(Fornecedor fornecedor)
        {
            _repositorio.Alterar(fornecedor);
        }

        public void Remover(int codigo)
        {
            _repositorio.Remover(codigo);
        }

        public Fornecedor BuscarPorCodigo(int codigo)
        {
            return _repositorio.BuscarPorCodigo(codigo);
        }

        public List<Fornecedor> ListarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public List<Fornecedor> BuscarPorNome(string nome)
        {
            return _repositorio.BuscarPorNome(nome);
        }
    }
}