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
                Console.WriteLine("=== MENU DE TRANSPORTADORAS ===");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar todos");
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
                        break;
                }
            }
        }

        private void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de Transportadora:");
            Console.Write("Código: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine() ?? "";
            Console.Write("Preço por Km: ");
            double precoPorKm = double.Parse(Console.ReadLine() ?? "0");

            var transportadora = new Transportadora(codigo, nome, telefone, email, cnpj, precoPorKm);
            _servicoTransportadora.Cadastrar(transportadora);

            Console.WriteLine("Transportadora cadastrada com sucesso!");
            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var transportadoras = _servicoTransportadora.ListarTodos();

            if (transportadoras.Count == 0)
            {
                Console.WriteLine("Nenhuma transportadora cadastrada.");
            }
            else
            {
                Console.WriteLine("--- TRANSPORTADORAS CADASTRADAS ---");
                foreach (var t in transportadoras)
                    Console.WriteLine(t);
            }

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            Console.WriteLine("Alteração de Transportadora:");
            Console.Write("Código da transportadora a alterar: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var transportadora = _servicoTransportadora.BuscarPorCodigo(codigo);

            if (transportadora == null)
            {
                Console.WriteLine("Transportadora não encontrada!");
                PressioneParaContinuar();
                return;
            }

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Novo telefone: ");
            string telefone = Console.ReadLine() ?? "";
            Console.Write("Novo email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Novo CNPJ: ");
            string cnpj = Console.ReadLine() ?? "";
            Console.Write("Novo preço por Km: ");
            double precoPorKm = double.Parse(Console.ReadLine() ?? "0");

            var novaTransportadora = new Transportadora(codigo, nome, telefone, email, cnpj, precoPorKm);
            _servicoTransportadora.Alterar(novaTransportadora);

            Console.WriteLine("Transportadora alterada com sucesso!");
            PressioneParaContinuar();
        }

        private void Remover()
        {
            Console.Clear();
            Console.Write("Código da transportadora a remover: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var transportadora = _servicoTransportadora.BuscarPorCodigo(codigo);

            if (transportadora == null)
            {
                Console.WriteLine("Transportadora não encontrada!");
                PressioneParaContinuar();
                return;
            }

            _servicoTransportadora.Remover(codigo);
            Console.WriteLine("Transportadora removida com sucesso!");
            PressioneParaContinuar();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            Console.Write("Digite o código da transportadora: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
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
            Console.Write("Digite parte do nome da transportadora: ");
            string nome = Console.ReadLine() ?? "";
            var transportadoras = _servicoTransportadora.BuscarPorNome(nome);
            if (transportadoras.Count > 0)
                transportadoras.ForEach(t => Console.WriteLine(t));
            else
                Console.WriteLine("Nenhuma transportadora encontrada!");
            PressioneParaContinuar();
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}