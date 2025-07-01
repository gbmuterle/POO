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

        public void Menu(Usuario usuario)
        {
            if (usuario.Perfil == "admin")
                MenuAdmin();
            else
                MenuCliente();
        }
        private void MenuAdmin()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PRODUTOS ===");
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
                        continue;
                }
            }
        }

        private void MenuCliente()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PRODUTOS ===");
                Console.WriteLine("1 - Listar todos");
                Console.WriteLine("2 - Buscar por código");
                Console.WriteLine("3 - Buscar por nome");
                Console.WriteLine("0 - Voltar");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        Listar();
                        break;
                    case "2":
                        BuscarPorCodigo();
                        break;
                    case "3":
                        BuscarPorNome();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        continue;
                }
            }
        }

        private void Cadastrar()
        {
            Console.Clear();

            var fornecedores = _servicoFornecedor.ListarTodos();
            if (fornecedores == null || !fornecedores.Any())
            {
                Console.WriteLine("Não há fornecedores cadastrados. Cadastre um fornecedor antes de cadastrar produtos.");
                PressioneParaContinuar();
                return;
            }

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

            double valor;
            while (true)
            {
                Console.Write("Valor: ");
                if (double.TryParse(Console.ReadLine(), out valor))
                    break;
                Console.WriteLine("Valor inválido. Digite um número válido.");
            }

            int quantidade;
            while (true)
            {
                Console.Write("Quantidade: ");
                if (int.TryParse(Console.ReadLine(), out quantidade))
                    break;
                Console.WriteLine("Quantidade inválida. Digite um número inteiro.");
            }

            int codFornecedor;
            Fornecedor fornecedor;
            while (true)
            {
                Console.Write("Código do fornecedor: ");
                if (int.TryParse(Console.ReadLine(), out codFornecedor))
                {
                    var fornecedorEncontrado = _servicoFornecedor.BuscarPorCodigo(codFornecedor);
                    if (fornecedorEncontrado!= null)
                    {
                        fornecedor = fornecedorEncontrado;
                        break;
                    }
                    Console.WriteLine("Fornecedor não encontrado! Digite um código válido.");
                }
                else
                {
                    Console.WriteLine("Código inválido. Digite um número inteiro.");
                }
            }

            var produto = new Produto(codigo, nome, valor, quantidade, fornecedor!);

            try
            {
                _servicoProduto.Cadastrar(produto);
                Console.WriteLine("\nProduto cadastrado com sucesso!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar produto: {ex.Message}");
            }

            PressioneParaContinuar();
        }
        private void Listar()
        {
            Console.Clear();
            var produtos = _servicoProduto.ListarTodos();

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhuma produto cadastrado.");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Lista de Produtos ---");
            foreach (var p in produtos)
                Console.WriteLine(p);

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

            var produtoAtual = _servicoProduto.BuscarPorCodigo(codigo);

            if (produtoAtual == null)
            {
                Console.WriteLine("Produto não encontrado!");
                PressioneParaContinuar();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Dados atuais ---");
                Console.WriteLine(produtoAtual);
                Console.WriteLine("\nQual campo deseja alterar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Valor");
                Console.WriteLine("3 - Quantidade");
                Console.WriteLine("4 - Fornecedor");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                string novoNome = produtoAtual.Nome;
                double novoValor = produtoAtual.Valor;
                int novaQuantidade = produtoAtual.Quantidade;
                Fornecedor novoFornecedor = produtoAtual.Fornecedor;

                switch (opcao)
                {
                    case "1":
                        Console.Write("Novo nome: ");
                        novoNome = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        while (true)
                        {
                            Console.Write("Novo valor: ");
                            if (double.TryParse(Console.ReadLine(), out novoValor))
                                break;
                            Console.WriteLine("Valor inválido. Digite um número válido.");
                        }
                        break;
                    case "3":
                        while (true)
                        {
                            Console.Write("Nova quantidade: ");
                            if (int.TryParse(Console.ReadLine(), out novaQuantidade))
                                break;
                            Console.WriteLine("Quantidade inválida. Digite um número válido.");
                        }
                        break;
                    case "4":
                        while (true)
                        {
                            Console.Write("Novo código do fornecedor: ");
                            if (int.TryParse(Console.ReadLine(), out int novoCodFornecedor))
                            {
                                var fornecedorEncontrado = _servicoFornecedor.BuscarPorCodigo(novoCodFornecedor);
                                if (fornecedorEncontrado != null)
                                {
                                    novoFornecedor = fornecedorEncontrado;
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
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        continue;
                }

                var produtoAlterado = new Produto(
                    produtoAtual.Codigo,
                    novoNome,
                    novoValor,
                    novaQuantidade,
                    novoFornecedor
                );

                try
                {
                    _servicoProduto.Alterar(produtoAtual, produtoAlterado);
                    Console.WriteLine("Produto alterado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao alterar produto: {ex.Message}");
                }

                PressioneParaContinuar();
                break;
            }
        }

        private void Remover()
        {
            Console.Clear();
            Console.WriteLine("Informe o código do produto:");
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var produto = _servicoProduto.BuscarPorCodigo(codigo);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                PressioneParaContinuar();
                return;
            }

            try
            {
                _servicoProduto.Remover(produto);
                Console.WriteLine("Produto removido com sucesso!");
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
            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine() ?? "";
            var produtos = _servicoProduto.BuscarPorNome(nome);

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto encontrado!");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Produtos encontrados ---");
            foreach (var p in produtos)
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