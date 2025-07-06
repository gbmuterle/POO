namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioFornecedor
    {
        void Adicionar(Fornecedor fornecedor);
        void Alterar(Fornecedor fornecedorAtual, Fornecedor fornecedorAlterado);
        void Remover(Fornecedor fornecedor);
        Fornecedor? BuscarPorCodigo(int codigo);
        List<Fornecedor> BuscarTodos();
        List<Fornecedor> BuscarPorNome(string nome);
        void Salvar();
    }
}