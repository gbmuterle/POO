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
                        BuscarTodos(usuario);
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
            DateTime? novaDataCancelamento = pedidoAtual.DataCancelamento;
            Transportadora novaTransportadora = pedidoAtual.Transportadora;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Dados atuais do pedido ---");
                Console.WriteLine(pedidoAtual);
                Console.WriteLine("\nQual campo deseja alterar?");
                Console.WriteLine("1 - Situação");
                Console.WriteLine("2 - Data de entrega");
                Console.WriteLine("3 - Transportadora");
                Console.WriteLine("0 - Voltar");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        Console.Write("Nova situação: ");
                        novaSituacao = Console.ReadLine() ?? pedidoAtual.Situacao;

                        if (novaSituacao.Equals("entregue", StringComparison.OrdinalIgnoreCase))
                        {
                            novaDataEntrega = DateTime.Now;
                            novaDataCancelamento = null;
                        }
                        else if (novaSituacao.Equals("cancelado", StringComparison.OrdinalIgnoreCase))
                        {
                            novaDataCancelamento = DateTime.Now;
                            novaDataEntrega = null;
                        }
                        break;

                    case "2":
                        while (true)
                        {
                            Console.Write("Nova data de entrega (dd/MM/yyyy): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime novaData))
                            {
                                novaDataEntrega = novaData;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Data inválida. Tente novamente.");
                            }
                        }
                        break;

                    case "3":
                        var transportadoras = _servicoTransportadora.BuscarTodos();
                        Console.WriteLine("--- Lista de Transportadoras ---");
                        foreach (var t in transportadoras)
                            Console.WriteLine(t);
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
                                Console.WriteLine("Transportadora não encontrada! Digite um código válido.");
                            }
                            else
                            {
                                Console.WriteLine("Código inválido. Digite um número inteiro.");
                            }
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        break;
                }

                var pedidoAlterado = new Pedido(
                    pedidoAtual.Numero,
                    pedidoAtual.Cliente,
                    pedidoAtual.DataCriacao,
                    novaDataEntrega,
                    novaDataCancelamento,
                    novaSituacao,
                    pedidoAtual.Itens,
                    novaTransportadora,
                    pedidoAtual.Distancia
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
            }
        }

        private void BuscarTodos(Usuario? usuario = null)
        {
            Console.Clear();
            try
            {
                var pedidos = usuario == null
                    ? _servicoPedido.BuscarTodos()
                    : _servicoPedido.BuscarTodos(usuario);

                if (!pedidos.Any())
                    Console.WriteLine("Nenhum pedido encontrado.");
                else
                    foreach (var p in pedidos)
                        Console.WriteLine(p);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void BuscarPorNumero(Usuario? usuario = null)
        {
            Console.Clear();
            int numero;
            while (true)
            {
                Console.Write("Número do pedido: ");
                if (int.TryParse(Console.ReadLine(), out numero))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            try
            {
                var pedido = usuario == null
                    ? _servicoPedido.BuscarPorNumero(numero)
                    : _servicoPedido.BuscarPorNumero(numero, usuario);

                if (pedido != null)
                    Console.WriteLine(pedido);
                else
                    Console.WriteLine("Pedido não encontrado!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void BuscarPorData(Usuario? usuario = null)
        {
            Console.Clear();
            DateTime dataInicial;
            while (true)
            {
                Console.Write("Data inicial (dd/MM/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out dataInicial))
                    break;

                Console.WriteLine("Data inválida! Tente novamente.");
            }

            DateTime dataFinal;
            while (true)
            {
                Console.Write("Data final (dd/MM/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out dataFinal))
                    break;

                Console.WriteLine("Data inválida! Tente novamente.");
            }

            List<Pedido> pedidos;
            try
            {
                pedidos = usuario == null
                    ? _servicoPedido.BuscarPorData(dataInicial, dataFinal)
                    : _servicoPedido.BuscarPorData(dataInicial, dataFinal, usuario);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                PressioneParaContinuar();
                return;
            }

            if (!pedidos.Any())
                Console.WriteLine("Nenhum pedido encontrado para esse período.");
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