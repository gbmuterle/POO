namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;
    using System;

    public class ServicoTransportadora
    {
        private readonly IRepositorioTransportadora _repositorio;

        public ServicoTransportadora(IRepositorioTransportadora repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(Transportadora transportadora)
        {
            Validar(transportadora, true);
            _repositorio.Adicionar(transportadora);
        }

        public void Alterar(Transportadora transportadoraAtual, Transportadora transportadoraNova)
        {
            Validar(transportadoraNova, false);
            _repositorio.Alterar(transportadoraAtual, transportadoraNova);
        }

        public void Remover(Transportadora transportadora)
        {
            _repositorio.Remover(transportadora);
        }

        public Transportadora? BuscarPorCodigo(int codigo)
        {
            return _repositorio.BuscarPorCodigo(codigo);
        }

        public List<Transportadora> ListarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public List<Transportadora> BuscarPorNome(string nome)
        {
            return _repositorio.BuscarPorNome(nome);
        }

        private void Validar(Transportadora transportadora, bool novo)
        {
            if (transportadora == null)
                throw new InvalidOperationException("Transportadora inválida.");

            if (transportadora.Codigo <= 0)
                throw new InvalidOperationException("Código inválido.");

            if (string.IsNullOrWhiteSpace(transportadora.Nome))
                throw new InvalidOperationException("Nome inválido.");

            if (string.IsNullOrWhiteSpace(transportadora.Cnpj))
                throw new InvalidOperationException("CNPJ inválido.");

            if (!transportadora.Cnpj.All(char.IsDigit))
                throw new InvalidOperationException("CNPJ deve conter apenas números.");

            if (transportadora.Cnpj.Length != 14)
                throw new InvalidOperationException("CNPJ deve conter exatamente 14 números.");
            
            if (transportadora.PrecoPorKm < 0)
                throw new InvalidOperationException("O preço por Km não pode ser negativo.");

            if (novo)
            {
                if (BuscarPorCodigo(transportadora.Codigo) != null)
                    throw new InvalidOperationException("Já existe uma transportadora com esse código.");
            }
            else
            {
                if (BuscarPorCodigo(transportadora.Codigo) == null)
                    throw new InvalidOperationException("Transportadora não encontrada para alteração.");
            }
        }
    }
}