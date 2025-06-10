namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioFornecedor
    {
        void Adicionar(Fornecedor fornecedor);
        void Alterar(Fornecedor fornecedor);
        void Remover(int codigo);
        Fornecedor? BuscarPorCodigo(int codigo);
        List<Fornecedor> BuscarTodos();
        List<Fornecedor> BuscarPorNome(string nome);
    }
}