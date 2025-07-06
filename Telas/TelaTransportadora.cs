namespace Telas
{
    using Servicos;
    using Modelos;
    using System;

    public class TelaTransportadora
    {
        private readonly ServicoTransportadora _servicoTransportadora;

        public TelaTransportadora(ServicoTransportadora servicoTransportadora)
        {
            _servicoTransportadora = servicoTransportadora;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== TRANSPORTADORAS ===");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar todas");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("5 - Buscar por código");
                Console.WriteLine("6 - Buscar por nome");
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
                    case "6":
                        BuscarPorNome();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        PressioneParaContinuar();
                        continue;
                }
            }
        }

        private void Cadastrar()
        {
            Console.Clear();

            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine() ?? "";

            double precoPorKm;
            while (true)
            {
                Console.Write("Preço por Km: ");
                if (double.TryParse(Console.ReadLine(), out precoPorKm))
                    break;
                Console.WriteLine("Preço inválido. Digite um número válido.");
            }

            var transportadora = new Transportadora(codigo, nome, telefone, email, cnpj, precoPorKm);

            try
            {
                _servicoTransportadora.Cadastrar(transportadora);
                Console.WriteLine("Transportadora cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar transportadora: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var transportadoras = _servicoTransportadora.BuscarTodos();

            if (!transportadoras.Any())
            {
                Console.WriteLine("Nenhuma transportadora cadastrada.");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Lista de Transportadoras ---");
            foreach (var t in transportadoras)
                Console.WriteLine(t);

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var transportadoraAtual = _servicoTransportadora.BuscarPorCodigo(codigo);

            if (transportadoraAtual == null)
            {
                Console.WriteLine("Transportadora não encontrada!");
                PressioneParaContinuar();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Dados atuais ---");
                Console.WriteLine(transportadoraAtual);
                Console.WriteLine("\nQual campo deseja alterar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Telefone");
                Console.WriteLine("3 - Email");
                Console.WriteLine("4 - CNPJ");
                Console.WriteLine("5 - Preço por Km");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                string novoNome = transportadoraAtual.Nome;
                string novoTelefone = transportadoraAtual.Telefone;
                string novoEmail = transportadoraAtual.Email;
                string novoCnpj = transportadoraAtual.Cnpj;
                double novoPrecoPorKm = transportadoraAtual.PrecoPorKm;

                switch (opcao)
                {
                    case "1":
                        Console.Write("Novo nome: ");
                        novoNome = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        Console.Write("Novo telefone: ");
                        novoTelefone = Console.ReadLine() ?? "";
                        break;
                    case "3":
                        Console.Write("Novo email: ");
                        novoEmail = Console.ReadLine() ?? "";
                        break;
                    case "4":
                        Console.Write("Novo CNPJ: ");
                        novoCnpj = Console.ReadLine() ?? "";
                        break;
                    case "5":
                        while (true)
                        {
                            Console.Write("Novo preço por Km: ");
                            if (double.TryParse(Console.ReadLine(), out novoPrecoPorKm))
                                break;
                            Console.WriteLine("Preço inválido. Digite um número válido.");
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        continue;
                }

                var transportadoraAlterada = new Transportadora(
                    transportadoraAtual.Codigo,
                    novoNome,
                    novoTelefone,
                    novoEmail,
                    novoCnpj,
                    novoPrecoPorKm
                );

                try
                {
                    _servicoTransportadora.Alterar(transportadoraAtual, transportadoraAlterada);
                    Console.WriteLine("Transportadora alterada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao alterar transportadora: {ex.Message}");
                }

                PressioneParaContinuar();
                break;
            }
        }

        private void Remover()
        {
            Console.Clear();
            Console.WriteLine("Informe o código da transportadora:");
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var transportadora = _servicoTransportadora.BuscarPorCodigo(codigo);

            if (transportadora == null)
            {
                Console.WriteLine("Transportadora não encontrada.");
                PressioneParaContinuar();
                return;
            }

            try
            {
                _servicoTransportadora.Remover(transportadora);
                Console.WriteLine("Transportadora removida com sucesso!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            Console.WriteLine("Informe o código:");
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }
            var transportadora = _servicoTransportadora.BuscarPorCodigo(codigo);
            if (transportadora != null)
                Console.WriteLine(transportadora);
            else
                Console.WriteLine("Transportadora não encontrada!");
            PressioneParaContinuar();
        }

        private void BuscarPorNome()
        {
            Console.Clear();
            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine() ?? "";
            var transportadoras = _servicoTransportadora.BuscarPorNome(nome);

            if (!transportadoras.Any())
            {
                Console.WriteLine("Nenhuma transportadora encontrada!");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Transportadoras encontradas ---");
            foreach (var t in transportadoras)
                Console.WriteLine(t);

            PressioneParaContinuar();
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}