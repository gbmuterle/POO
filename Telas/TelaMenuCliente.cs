namespace Telas
{
    using System;

    public class TelaMenuCliente
    {
        private readonly TelaProduto _telaProduto;
        private readonly TelaPedido _telaPedido;

        public TelaMenuCliente(TelaProduto telaProduto, TelaPedido telaPedido)
        {
            _telaProduto = telaProduto;
            _telaPedido = telaPedido;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU DO CLIENTE ===");
                Console.WriteLine("1 - Consulta de Produtos");
                Console.WriteLine("2 - Pedidos");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        _telaProduto.Menu();
                        break;
                    case "2":
                        _telaPedido.Menu();
                        break;
                    case "0":
                        Console.WriteLine("Retornando...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        PressioneParaContinuar();
                        continue;
                }
            }
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
