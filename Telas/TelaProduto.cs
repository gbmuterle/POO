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
                Console.Write("\nEscolha uma opção: ");
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
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Cadastrar()
        {
            Console.Clear();
            int codigo = InputObrigatorioInt("Código");
            string nome = InputObrigatorioString("Nome");

            Console.Write("Valor: ");
            double valor = double.TryParse(Console.ReadLine()?.Trim(), out double v) ? v : 0;

            Console.Write("Quantidade: ");
            int quantidade = int.TryParse(Console.ReadLine()?.Trim(), out int q) ? q : 0;

            int codFornecedor = InputObrigatorioInt("Código do Fornecedor");
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
            }

            else
            {
                string descricao = InputObrigatorioString("Descrição");
                var produto = new Produto(codigo, nome, descricao, valor, quantidade, fornecedor);

                try
                {
                    _servicoProduto.Cadastrar(produto);
                    Console.WriteLine("\nProduto cadastrado com sucesso!");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro ao cadastrar produto: {ex.Message}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
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
                Console.WriteLine("--- PRODUTOS CADASTRADOS ---\n");
                foreach (var p in produtos)
                    Console.WriteLine(p);
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void Alterar()
        {
            Console.Clear();
            int codigo = InputObrigatorioInt("Código");
            var produtoAtual = _servicoProduto.BuscarPorCodigo(codigo);

            if (produtoAtual == null)
            {
                Console.WriteLine("Produto não encontrado!");
            }

            else
            {
                var novoNome = InputAlteracaoString("Nome", produtoAtual.Nome);
                var novoValor = InputAlteracaoDouble("Valor", produtoAtual.Valor);
                var novaQuantidade = InputAlteracaoInt("Quantidade", produtoAtual.Quantidade);
                var novoCodFornecedor = InputAlteracaoInt("Código do Fornecedor", produtoAtual.Fornecedor.Codigo);

                var novoFornecedor = _servicoFornecedor.BuscarPorCodigo(novoCodFornecedor);

                if (novoFornecedor == null)
                {
                    Console.WriteLine("Fornecedor não encontrado!");
                }

                else
                {
                    var novaDescricao = InputAlteracaoString("Descrição", produtoAtual.Descricao);
                    var produtoAlterado = new Produto(codigo, novoNome, novaDescricao, novoValor, novaQuantidade, novoFornecedor);
                    try
                    {
                        _servicoProduto.Alterar(produtoAtual, produtoAlterado);
                        Console.WriteLine("Produto alterado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                }
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void Remover()
        {
            Console.Clear();
            int codigo = InputObrigatorioInt("Código");
            var produto = _servicoProduto.BuscarPorCodigo(codigo);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
            }

            else
            {
                try
                {
                    _servicoProduto.Remover(produto);
                    Console.WriteLine("Produto removido com sucesso!");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }

            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadKey();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            int codigo = InputObrigatorioInt("Código");
            var produto = _servicoProduto.BuscarPorCodigo(codigo);
            if (produto != null)
                Console.WriteLine(produto);
            else
                Console.WriteLine("Produto não encontrado!");

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarPorNome()
        {
            Console.Clear();
            string nome = Console.ReadLine() ?? "";
            var produtos = _servicoProduto.BuscarPorNome(nome);
            if (produtos.Count > 0)
                produtos.ForEach(p => Console.WriteLine(p));
            else
                Console.WriteLine("Nenhum produto encontrado!");

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private int InputObrigatorioInt(string campo)
        {
            int valor;
            while (true)
            {
                Console.Write($"{campo}: ");
                string entrada = Console.ReadLine()?.Trim() ?? "";
                if (int.TryParse(entrada, out valor))
                    return valor;
                Console.WriteLine($"{campo} deve ser um número inteiro válido. Tente novamente.");
            }
        }
        private string InputObrigatorioString(string campo)
        {
            while (true)
            {
                Console.Write($"{campo}: ");
                string valor = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(valor))
                    return valor;
                Console.WriteLine($"{campo} não pode ser vazio. Tente novamente.");
            }
        }
        private string InputAlteracaoString(string campo, string valorAtual)
        {
            Console.Write($"{campo}: ");
            string valor = Console.ReadLine()?.Trim() ?? "";
            return string.IsNullOrEmpty(valor) ? valorAtual : valor;
        }
        private int InputAlteracaoInt(string campo, int valorAtual)
        {
            Console.Write($"{campo} ({valorAtual}): ");
            string entrada = Console.ReadLine()?.Trim() ?? "";
            return int.TryParse(entrada, out int valor) ? valor : valorAtual;
        }

        private double InputAlteracaoDouble(string campo, double valorAtual)
        {
            Console.Write($"{campo} ({valorAtual}): ");
            string entrada = Console.ReadLine()?.Trim() ?? "";
            return double.TryParse(entrada, out double valor) ? valor : valorAtual;
        }
    }
}