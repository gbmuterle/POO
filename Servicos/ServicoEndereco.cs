using System;
using Modelos;

namespace Servicos
{
    public class ServicoEndereco
    {
        public void Validar(Endereco endereco)
        {
            if (endereco == null)
                throw new InvalidOperationException("Endereço inválido.");

            if (string.IsNullOrWhiteSpace(endereco.Rua))
                throw new InvalidOperationException("Rua inválida.");

            if (string.IsNullOrWhiteSpace(endereco.Numero))
                throw new InvalidOperationException("Número inválido.");

            if (string.IsNullOrWhiteSpace(endereco.Bairro))
                throw new InvalidOperationException("Bairro inválido.");

            if (string.IsNullOrWhiteSpace(endereco.Cidade))
                throw new InvalidOperationException("Cidade inválida.");

            if (string.IsNullOrWhiteSpace(endereco.Estado))
                throw new InvalidOperationException("Estado inválido.");
            
            if (string.IsNullOrWhiteSpace(endereco.Cep))
                throw new InvalidOperationException("CEP inválido.");

            if (!endereco.Cep.All(char.IsDigit))
                throw new InvalidOperationException("CEP deve conter apenas números.");

            if (endereco.Cep.Length != 8)
                throw new InvalidOperationException("CEP deve conter exatamente 8 números.");
        }
    }
}