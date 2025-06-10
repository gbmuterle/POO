namespace Telas
{
    using Servicos;
    using Modelos;
    using System;

    public class TelaProduto
    {
        private readonly ServicoProduto _servicoProduto;
        private readonly ServicoFornecedor _servicoFornecedor;

        public TelaProduto(ServicoProduto servicoProduto, ServicoFornecedor servicoFornecedor)
        {
            _servicoProduto = servicoProduto;
            _servicoFornecedor = servicoFornecedor;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU DE PRODUTOS ===");
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
            Console.WriteLine("Cadastro de Produto:");
            Console.Write("Código: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Valor: ");
            double valor = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Quantidade: ");
            int quantidade = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Código do Fornecedor: ");
            int codFornecedor = int.Parse(Console.ReadLine() ?? "0");
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                PressioneParaContinuar();
                return;
            }

            var produto = new Produto(codigo, nome, valor, quantidade, fornecedor);
            _servicoProduto.Cadastrar(produto);

            Console.WriteLine("Produto cadastrado com sucesso!");
            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var produtos = _servicoProduto.ListarTodos();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
            }
            else
            {
                Console.WriteLine("--- PRODUTOS CADASTRADOS ---");
                foreach (var p in produtos)
                    Console.WriteLine(p);
            }

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            Console.WriteLine("Alteração de Produto:");
            Console.Write("Código do produto a alterar: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var produto = _servicoProduto.BuscarPorCodigo(codigo);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                PressioneParaContinuar();
                return;
            }

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Novo valor: ");
            double valor = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nova quantidade: ");
            int quantidade = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Novo código do Fornecedor: ");
            int codFornecedor = int.Parse(Console.ReadLine() ?? "0");
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                PressioneParaContinuar();
                return;
            }

            var novoProduto = new Produto(codigo, nome, valor, quantidade, fornecedor);
            _servicoProduto.Alterar(novoProduto);

            Console.WriteLine("Produto alterado com sucesso!");
            PressioneParaContinuar();
        }

        private void Remover()
        {
            Console.Clear();
            Console.Write("Código do produto a remover: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var produto = _servicoProduto.BuscarPorCodigo(codigo);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                PressioneParaContinuar();
                return;
            }

            _servicoProduto.Remover(codigo);
            Console.WriteLine("Produto removido com sucesso!");
            PressioneParaContinuar();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            Console.Write("Digite o código do produto: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");
            var produto = _servicoProduto.BuscarPorCodigo(codigo);
            if (produto != null)
                Console.WriteLine(produto);
            else
                Console.WriteLine("Produto não encontrado!");
            PressioneParaContinuar();
        }

        private void BuscarPorNome()
        {
            Console.Clear();
            Console.Write("Digite parte do nome do produto: ");
            string nome = Console.ReadLine() ?? "";
            var produtos = _servicoProduto.BuscarPorNome(nome);
            if (produtos.Count > 0)
                produtos.ForEach(p => Console.WriteLine(p));
            else
                Console.WriteLine("Nenhum produto encontrado!");
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