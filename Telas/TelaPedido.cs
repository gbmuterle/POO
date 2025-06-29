namespace Telas
{
    using System;
    using Servicos;
    using Modelos;
    using System.Linq;

    public class TelaPedido
    {
        private readonly ServicoPedido _servicoPedido;
        private readonly ServicoTransportadora _servicoTransportadora;

        public TelaPedido(ServicoPedido servicoPedido, ServicoTransportadora servicoTransportadora)
        {
            _servicoPedido = servicoPedido;
            _servicoTransportadora = servicoTransportadora;
        }

        public void Menu(Usuario usuario)
        {
            if (usuario.Perfil == "admin")
                MenuAdmin();
            else
                MenuCliente(usuario);
        }

        private void MenuAdmin()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PEDIDOS ===");
                Console.WriteLine("1 - Listar todos");
                Console.WriteLine("2 - Alterar pedido");
                Console.WriteLine("3 - Buscar por n° do pedido");
                Console.WriteLine("4 - Buscar por data");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        BuscarTodos();
                        break;
                    case "2":
                        Alterar();
                        break;
                    case "3":
                        BuscarPorNumero();
                        break;
                    case "4":
                        BuscarPorData();
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

        private void MenuCliente(Usuario usuario)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MEUS PEDIDOS ===");
                Console.WriteLine("1 - Listar todos");
                Console.WriteLine("2 - Buscar por n° do pedido");
                Console.WriteLine("3 - Buscar por data");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        BuscarPorCliente(usuario);
                        break;
                    case "2":
                        BuscarPorNumero(usuario);
                        break;
                    case "3":
                        BuscarPorData(usuario);
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

        private void BuscarTodos()
        {
            Console.Clear();
            var pedidos = _servicoPedido.BuscarTodos();

            if (!pedidos.Any())
                Console.WriteLine("Nenhum pedido cadastrado.");
            else
                foreach (var p in pedidos)
                    Console.WriteLine(p);

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            Console.Write("Número do pedido a alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("Número inválido!");
                PressioneParaContinuar();
                return;
            }

            var pedidoAtual = _servicoPedido.BuscarPorNumero(numero);

            if (pedidoAtual == null)
            {
                Console.WriteLine("Pedido não encontrado!");
                PressioneParaContinuar();
                return;
            }

            string novaSituacao = pedidoAtual.Situacao;
            DateTime? novaDataEntrega = pedidoAtual.DataEntrega;
            Transportadora? novaTransportadora = pedidoAtual.Transportadora;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Dados atuais do pedido ---");
                Console.WriteLine(pedidoAtual);
                Console.WriteLine("\nQual campo deseja alterar?");
                Console.WriteLine("1 - Situação");
                Console.WriteLine("2 - Data de entrega");
                Console.WriteLine("3 - Transportadora");
                Console.WriteLine("0 - Salvar e sair");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("Situações possíveis: novo, transporte, entregue, cancelado");
                        Console.Write("Nova situação: ");
                        novaSituacao = Console.ReadLine() ?? pedidoAtual.Situacao;
                        break;
                    case "2":
                        Console.Write("Nova data de entrega (dd/MM/yyyy): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out novaDataEntrega))
                        {
                            Console.WriteLine("Data inválida. Mantendo a data atual.");
                            novaDataEntrega = pedidoAtual.DataEntrega;
                        }
                        break;
                    case "3":
                        while (true)
                        {
                            Console.Write("Novo código da transportadora: ");
                            if (int.TryParse(Console.ReadLine(), out int novoCodTransportadora))
                            {
                                var transportadoraEncontrada = _servicoTransportadora.BuscarPorCodigo(novoCodTransportadora);
                                if (transportadoraEncontrada != null)
                                {
                                    novaTransportadora = transportadoraEncontrada;
                                    break;
                                }
                                Console.WriteLine("Fornecedor não encontrado! Digite um código válido.");
                            }
                            else
                            {
                                Console.WriteLine("Código inválido. Digite um número inteiro.");
                            }
                        }
                        break;
                    case "0":
                        var pedidoAlterado = new Pedido(
                            pedidoAtual.Numero,
                            pedidoAtual.Cliente,
                            pedidoAtual.DataCriacao,
                            novaDataEntrega,
                            novaSituacao,
                            pedidoAtual.Itens,
                            novaTransportadora
                        );
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
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        break;
                }
            }
        }

        private void BuscarPorCliente(Usuario usuario)
        {
            Console.Clear();
            var pedidos = _servicoPedido.BuscarPorCliente(usuario);
            if (!pedidos.Any())
                Console.WriteLine("Você não possui pedidos.");
            else
                foreach (var p in pedidos)
                    Console.WriteLine(p);

            PressioneParaContinuar();
        }

        private void BuscarPorNumero()
        {
            Console.Clear();
            int numero;
            while (true)
            {
                Console.Write("Nùmero do pedido: ");
                if (int.TryParse(Console.ReadLine(), out numero))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var pedido = _servicoPedido.BuscarPorNumero(numero);

            if (pedido != null)
                Console.WriteLine(pedido);
            else
                Console.WriteLine("Pedido não encontrado!");
            PressioneParaContinuar();
        }

        private void BuscarPorNumero(Usuario usuario)
        {
            Console.Clear();
            int numero;
            while (true)
            {
                Console.Write("Nùmero do pedido: ");
                if (int.TryParse(Console.ReadLine(), out numero))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var pedido = _servicoPedido.BuscarPorNumero(numero);

            if (pedido != null && pedido.Cliente.Nome == usuario.Nome)
                Console.WriteLine(pedido);
            else
                Console.WriteLine("Pedido não encontrado!");

            PressioneParaContinuar();
        }

        private void BuscarPorData()
        {
            Console.Clear();
            Console.Write("Data (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida!");
                PressioneParaContinuar();
                return;
            }

            var pedidos = _servicoPedido.BuscarTodos()
                .Where(p => p.DataCriacao.Date == data.Date)
                .ToList();

            if (!pedidos.Any())
                Console.WriteLine("Nenhum pedido encontrado para esta data.");
            else
                foreach (var p in pedidos)
                    Console.WriteLine(p);

            PressioneParaContinuar();
        }

        private void BuscarPorData(Usuario usuario)
        {
            Console.Clear();
            Console.Write("Data (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida!");
                PressioneParaContinuar();
                return;
            }

            var pedidos = _servicoPedido.BuscarTodos()
                .Where(p => p.Cliente != null && p.Cliente.Nome == usuario.Nome && p.DataCriacao.Date == data.Date)
                .ToList();

            if (!pedidos.Any())
                Console.WriteLine("Nenhum pedido encontrado para esta data.");
            else
                foreach (var p in pedidos)
                    Console.WriteLine(p);

            PressioneParaContinuar();
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}