using Servicos;
using Modelos;
using System;

namespace Telas
{
    public class TelaEndereco
    {
        private readonly ServicoEndereco _servicoEndereco;

        public TelaEndereco(ServicoEndereco servicoEndereco)
        {
            _servicoEndereco = servicoEndereco;
        }

        public Endereco Cadastrar()
        {
            while (true)
            {
                Console.WriteLine("--- Endereço ---\n");

                Console.Write("Rua: ");
                string rua = Console.ReadLine() ?? "";

                Console.Write("Número: ");
                string numero = Console.ReadLine() ?? "";

                Console.Write("Complemento: ");
                string complemento = Console.ReadLine() ?? "";

                Console.Write("Bairro: ");
                string bairro = Console.ReadLine() ?? "";

                Console.Write("Cidade: ");
                string cidade = Console.ReadLine() ?? "";

                Console.Write("Estado (UF): ");
                string estado = Console.ReadLine() ?? "";

                Console.Write("CEP: ");
                string cep = Console.ReadLine() ?? "";

                var endereco = new Endereco(rua, numero, complemento, bairro, cidade, estado, cep);

                try
                {
                    _servicoEndereco.Validar(endereco);
                    return endereco;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Endereço inválido: {ex.Message}");
                    Console.WriteLine("Por favor, tente novamente.\n");
                }
            }
        }
    }
}