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

        public void Alterar(Fornecedor fornecedor)
        {
            var fornecedorExistente = fornecedores.FirstOrDefault(f => f.Codigo == fornecedor.Codigo);
            if (fornecedorExistente != null)
            {
                fornecedorExistente.Nome = fornecedor.Nome;
                fornecedorExistente.Descricao = fornecedor.Descricao;
                fornecedorExistente.Telefone = fornecedor.Telefone;
                fornecedorExistente.Email = fornecedor.Email;
                fornecedorExistente.Cnpj = fornecedor.Cnpj;
                fornecedorExistente.Endereco = fornecedor.Endereco;
            }
        }

        public void Remover(int codigo)
        {
            var fornecedor = fornecedores.FirstOrDefault(f => f.Codigo == codigo);
            if (fornecedor != null)
            {
                fornecedores.Remove(fornecedor);
            }
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