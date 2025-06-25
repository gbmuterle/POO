namespace Telas
{
    using System;
    using Servicos;
    using Modelos;
    using System.Linq;

    public class TelaPedido
    {
        private readonly ServicoPedido _servicoPedido;

        public TelaPedido(ServicoPedido servicoPedido)
        {
            _servicoPedido = servicoPedido;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PEDIDOS ===");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar todos");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("5 - Buscar por código");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        Cadastrar();
                        break;
                    case "2":
                        Listar();
                        break;
                    case "3":
                        Alterar();
                        break;
                    case "4":
                        Remover();
                        break;
                    case "5":
                        BuscarPorCodigo();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        PressioneParaContinuar();
                        break;
                }
            }
        }

        private void Cadastrar()
        {
            Console.Clear();
            Console.Write("Código do pedido: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Descrição do pedido: ");
            string descricao = Console.ReadLine() ?? "";

            double valorTotal;
            while (true)
            {
                Console.Write("Valor total: ");
                if (double.TryParse(Console.ReadLine(), out valorTotal))
                    break;
                Console.WriteLine("Preço inválido. Digite um número válido.");
            }

            Console.Write("Nome do cliente: ");
            string cliente = Console.ReadLine() ?? "";

            var pedido = new Pedido(codigo, descricao, DateTime.Now, valorTotal, cliente);

            try
            {
                _servicoPedido.Cadastrar(pedido);
                Console.WriteLine("Pedido cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var pedidos = _servicoPedido.ListarTodos();

            if (!pedidos.Any())
            {
                Console.WriteLine("Nenhum pedido cadastrado.");
            }
            else
            {
                Console.WriteLine("=== Lista de Pedidos ===");
                foreach (var p in pedidos)
                {
                    Console.WriteLine(p);
                }
            }

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            Console.Write("Código do pedido a alterar: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");

            var pedidoAtual = _servicoPedido.BuscarPorCodigo(codigo);

            if (pedidoAtual == null)
            {
                Console.WriteLine("Pedido não encontrado!");
                PressioneParaContinuar();
                return;
            }

            Console.Write("Nova descrição: ");
            string novaDescricao = Console.ReadLine() ?? "";

            double novoValorTotal;
            while (true)
            {
                Console.Write("Valor total: ");
                if (double.TryParse(Console.ReadLine(), out novoValorTotal))
                    break;
                Console.WriteLine("Preço inválido. Digite um número válido.");
            }

            var pedidoAlterado = new Pedido(codigo, novaDescricao, pedidoAtual.DataCriacao, novoValorTotal, pedidoAtual.Cliente);

            try
            {
                _servicoPedido.Alterar(pedidoAtual, pedidoAlterado);
                Console.WriteLine("Pedido alterado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void Remover()
        {
            Console.Clear();
            Console.Write("Código do pedido a remover: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");

            var pedido = _servicoPedido.BuscarPorCodigo(codigo);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado!");
            }
            else
            {
                _servicoPedido.Remover(pedido);
                Console.WriteLine("Pedido removido com sucesso!");
            }

            PressioneParaContinuar();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            Console.Write("Código do pedido: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");

            var pedido = _servicoPedido.BuscarPorCodigo(codigo);

            if (pedido != null)
                Console.WriteLine(pedido);
            else
                Console.WriteLine("Pedido não encontrado!");

            PressioneParaContinuar();
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
