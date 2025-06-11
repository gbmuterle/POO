namespace Telas
{
    using System;

    public class TelaMenuAdmin
    {
        private readonly TelaProduto _telaProduto;
        private readonly TelaFornecedor _telaFornecedor;
        private readonly TelaTransportadora _telaTransportadora;

        public TelaMenuAdmin(TelaProduto telaProduto, TelaFornecedor telaFornecedor, TelaTransportadora telaTransportadora)
        {
            _telaProduto = telaProduto;
            _telaFornecedor = telaFornecedor;
            _telaTransportadora = telaTransportadora;
        }

        public void Exibir()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE CADASTRO ===");
                Console.WriteLine("1 - Produtos");
                Console.WriteLine("2 - Fornecedores");
                Console.WriteLine("3 - Transportadoras");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        _telaProduto.Menu();
                        break;
                    case "2":
                        _telaFornecedor.Menu();
                        break;
                    case "3":
                        _telaTransportadora.Menu();
                        break;
                    case "0":
                        Console.WriteLine("Encerrando o sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        PressioneParaContinuar();
                        break;
                }
            }
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}