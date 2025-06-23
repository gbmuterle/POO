namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoFornecedor
    {
        private readonly IRepositorioFornecedor _repositorio;

        public ServicoFornecedor(IRepositorioFornecedor repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(Fornecedor fornecedor)
        {
            Validar(fornecedor, true);
            _repositorio.Adicionar(fornecedor);
        }

        public void Alterar(Fornecedor fornecedorAtual, Fornecedor fornecedorAlterado)
        {
            Validar(fornecedorAlterado, false);
            _repositorio.Alterar(fornecedorAtual, fornecedorAlterado);
        }

        public void Remover(Fornecedor fornecedor)
        {
            _repositorio.Remover(fornecedor);
        }

        public Fornecedor? BuscarPorCodigo(int codigo)
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

        private void Validar(Fornecedor fornecedor, bool novo)
        {
            if (fornecedor == null)
                throw new InvalidOperationException("Fornecedor inválido.");

            if (fornecedor.Codigo <= 0)
                throw new InvalidOperationException("Código inválido.");

            if (string.IsNullOrWhiteSpace(fornecedor.Nome))
                throw new InvalidOperationException("Nome inválido.");

            if (string.IsNullOrWhiteSpace(fornecedor.Cnpj))
                throw new InvalidOperationException("CNPJ inválido.");

            if (!fornecedor.Cnpj.All(char.IsDigit))
                throw new InvalidOperationException("CNPJ deve conter apenas números.");

            if (fornecedor.Cnpj.Length != 14)
                throw new InvalidOperationException("CNPJ deve conter exatamente 14 números.");

            if (novo)
            {
                if (BuscarPorCodigo(fornecedor.Codigo) != null)
                    throw new InvalidOperationException("Já existe um fornecedor com esse código.");
            }
            else
            {
                if (BuscarPorCodigo(fornecedor.Codigo) == null)
                    throw new InvalidOperationException("Fornecedor não encontrado para alteração.");
            }
        }
    }
}