namespace Telas
{
    using System;
    using Modelos;

    public class TelaMenu
    {
        private readonly TelaProduto _telaProduto;
        private readonly TelaFornecedor _telaFornecedor;
        private readonly TelaTransportadora _telaTransportadora;
        private readonly TelaUsuario _telaUsuario;
        private readonly TelaPedido _telaPedido;
        private readonly TelaCarrinho _telaCarrinho;
        private readonly Usuario _usuario;

        public TelaMenu(TelaProduto telaProduto, TelaFornecedor telaFornecedor, TelaTransportadora telaTransportadora, TelaUsuario telaUsuario, TelaPedido telaPedido, TelaCarrinho telaCarrinho, Usuario usuario)
        {
            _telaProduto = telaProduto;
            _telaFornecedor = telaFornecedor;
            _telaTransportadora = telaTransportadora;
            _telaUsuario = telaUsuario;
            _telaPedido = telaPedido;
            _telaCarrinho = telaCarrinho;
            _usuario = usuario;
        }

        public void Menu()
        {
            if (_usuario.Perfil == "admin")
                MenuAdmin();
            else
                MenuCliente();
        }
        private void MenuAdmin()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU DO ADMINISTRADOR ===");
                Console.WriteLine("1 - Usuários");
                Console.WriteLine("2 - Produtos");
                Console.WriteLine("3 - Fornecedores");
                Console.WriteLine("4 - Transportadoras");
                Console.WriteLine("5 - Pedidos");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        _telaUsuario.Menu();
                        break;
                    case "2":
                        _telaProduto.Menu(_usuario);
                        break;
                    case "3":
                        _telaFornecedor.Menu();
                        break;
                    case "4":
                        _telaTransportadora.Menu();
                        break;
                    case "5":
                        _telaPedido.Menu(_usuario);
                        break;
                    case "0":
                        Console.WriteLine("Encerrando o sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        PressioneParaContinuar();
                        continue;
                }
            }
        }

        private void MenuCliente()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU DO CLIENTE ===");
                Console.WriteLine("1 - Carrinho");
                Console.WriteLine("2 - Pedidos");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        _telaCarrinho.Menu(_usuario);
                        break;
                    case "2":
                        _telaPedido.Menu(_usuario);
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