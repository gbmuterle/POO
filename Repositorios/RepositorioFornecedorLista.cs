namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioFornecedorLista : IRepositorioFornecedor
    {
        private List<Fornecedor> fornecedores = new List<Fornecedor>();

        public void Adicionar(Fornecedor fornecedor)
        {
            fornecedores.Add(fornecedor);
        }

        public void Alterar(Fornecedor fornecedorAtual, Fornecedor fornecedorAlterado)
        {
                fornecedorAtual.Nome = fornecedorAlterado.Nome;
                fornecedorAtual.Descricao = fornecedorAlterado.Descricao;
                fornecedorAtual.Telefone = fornecedorAlterado.Telefone;
                fornecedorAtual.Email = fornecedorAlterado.Email;
                fornecedorAtual.Cnpj = fornecedorAlterado.Cnpj;
                fornecedorAtual.Endereco = fornecedorAlterado.Endereco;
        }

        public void Remover(Fornecedor fornecedor)
        {
            fornecedores.Remove(fornecedor);
        }

        public Fornecedor? BuscarPorCodigo(int codigo)
        {
            return fornecedores.FirstOrDefault(f => f.Codigo == codigo);
        }

        public List<Fornecedor> BuscarTodos()
        {
            return new List<Fornecedor>(fornecedores);
        }

        public List<Fornecedor> BuscarPorNome(string nome)
        {
            return fornecedores
                .Where(f => f.Nome.ToLower().Contains(nome.ToLower()))
                .ToList();
        }
    }
}