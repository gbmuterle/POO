namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioFornecedorLista : IRepositorioFornecedor
    {
        private List<Fornecedor> fornecedores = new List<Fornecedor>();
        private readonly IArmazenamento<Fornecedor> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioFornecedorLista(IArmazenamento<Fornecedor> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            fornecedores = _armazenamento.Carregar(_caminhoArquivo);
        }
        public void Adicionar(Fornecedor fornecedor)
        {
            fornecedores.Add(fornecedor);
            Salvar();
        }

        public void Alterar(Fornecedor fornecedorAtual, Fornecedor fornecedorAlterado)
        {
            fornecedorAtual.Nome = fornecedorAlterado.Nome;
            fornecedorAtual.Descricao = fornecedorAlterado.Descricao;
            fornecedorAtual.Telefone = fornecedorAlterado.Telefone;
            fornecedorAtual.Email = fornecedorAlterado.Email;
            fornecedorAtual.Cnpj = fornecedorAlterado.Cnpj;
            fornecedorAtual.Endereco = fornecedorAlterado.Endereco;
            Salvar();
        }

        public void Remover(Fornecedor fornecedor)
        {
            fornecedores.Remove(fornecedor);
            Salvar();
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

        public void Salvar()
        {
            _armazenamento.Salvar(fornecedores, _caminhoArquivo);
        }
    }
}