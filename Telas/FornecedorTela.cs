namespace Telas
{
    using Servicos;
    using Modelos;
    using System;

    public class TelaFornecedor
    {
        private readonly ServicoFornecedor _servicoFornecedor;

        public TelaFornecedor(ServicoFornecedor servicoFornecedor)
        {
            _servicoFornecedor = servicoFornecedor;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU DE FORNECEDORES ===");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar todos");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("5 - Buscar por código");
                Console.WriteLine("6 - Buscar por nome");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

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
            Console.WriteLine("Cadastro de Fornecedor:");
            Console.Write("Código: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("--- Endereço do Fornecedor ---");
            Console.Write("Rua: ");
            string rua = Console.ReadLine();
            Console.Write("Número: ");
            string numero = Console.ReadLine();
            Console.Write("Complemento: ");
            string complemento = Console.ReadLine();
            Console.Write("Bairro: ");
            string bairro = Console.ReadLine();
            Console.Write("Cidade: ");
            string cidade = Console.ReadLine();
            Console.Write("Estado (UF): ");
            string estado = Console.ReadLine();
            Console.Write("CEP: ");
            string cep = Console.ReadLine();

            var endereco = new Endereco(rua, numero, complemento, bairro, cidade, estado, cep);

            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine();

            var fornecedor = new Fornecedor(codigo, nome, descricao, telefone, email, endereco, cnpj);
            _servicoFornecedor.Cadastrar(fornecedor);

            Console.WriteLine("Fornecedor cadastrado com sucesso!");
            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var fornecedores = _servicoFornecedor.ListarTodos();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado.");
            }
            else
            {
                Console.WriteLine("--- FORNECEDORES CADASTRADOS ---");
                foreach (var f in fornecedores)
                    Console.WriteLine(f);
            }

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            Console.WriteLine("Alteração de Fornecedor:");
            Console.Write("Código do fornecedor a alterar: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codigo);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                PressioneParaContinuar();
                return;
            }

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine();
            Console.Write("Nova descrição: ");
            string descricao = Console.ReadLine();
            Console.Write("Novo telefone: ");
            string telefone = Console.ReadLine();
            Console.Write("Novo email: ");
            string email = Console.ReadLine();

            Console.WriteLine("--- Novo Endereço ---");
            Console.Write("Rua: ");
            string rua = Console.ReadLine();
            Console.Write("Número: ");
            string numero = Console.ReadLine();
            Console.Write("Complemento: ");
            string complemento = Console.ReadLine();
            Console.Write("Bairro: ");
            string bairro = Console.ReadLine();
            Console.Write("Cidade: ");
            string cidade = Console.ReadLine();
            Console.Write("Estado (UF): ");
            string estado = Console.ReadLine();
            Console.Write("CEP: ");
            string cep = Console.ReadLine();

            var endereco = new Endereco(rua, numero, complemento, bairro, cidade, estado, cep);

            Console.Write("Novo CNPJ: ");
            string cnpj = Console.ReadLine();

            var novoFornecedor = new Fornecedor(codigo, nome, descricao, telefone, email, endereco, cnpj);
            _servicoFornecedor.Alterar(novoFornecedor);

            Console.WriteLine("Fornecedor alterado com sucesso!");
            PressioneParaContinuar();
        }

        private void Remover()
        {
            Console.Clear();
            Console.Write("Código do fornecedor a remover: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codigo);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                PressioneParaContinuar();
                return;
            }

            _servicoFornecedor.Remover(codigo);
            Console.WriteLine("Fornecedor removido com sucesso!");
            PressioneParaContinuar();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            Console.Write("Digite o código do fornecedor: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codigo);
            if (fornecedor != null)
                Console.WriteLine(fornecedor);
            else
                Console.WriteLine("Fornecedor não encontrado!");
            PressioneParaContinuar();
        }

        private void BuscarPorNome()
        {
            Console.Clear();
            Console.Write("Digite parte do nome do fornecedor: ");
            string nome = Console.ReadLine();
            var fornecedores = _servicoFornecedor.BuscarPorNome(nome);
            if (fornecedores.Count > 0)
                fornecedores.ForEach(f => Console.WriteLine(f));
            else
                Console.WriteLine("Nenhum fornecedor encontrado!");
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