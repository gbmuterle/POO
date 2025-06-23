using System;
using Servicos;
using Modelos;
using System.Linq;
using System.Collections.Generic;

namespace Telas
{
    public class TelaMenuCliente
    {
        private readonly ServicoProduto _servicoProduto;
        private readonly Carrinho _carrinho;

        public TelaMenuCliente(ServicoProduto servicoProduto)
        {
            _servicoProduto = servicoProduto;
            _carrinho = new Carrinho();
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== BEM-VINDO ===");
                Console.WriteLine("1 - Buscar produtos");
                Console.WriteLine("2 - Ver carrinho");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        BuscarProdutos();
                        break;
                    case "2":
                        ExibirCarrinho();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        continue;
                }
            }
        }

        private void BuscarProdutos()
        {
            Console.Clear();
            Console.Write("Digite um código, nome ou parte da descrição: ");
            string termo = Console.ReadLine()?.Trim().ToLower() ?? "";

            var todosProdutos = _servicoProduto.ListarTodos();
            var encontrados = todosProdutos
                .Where(p =>
                    p.Codigo.ToString().Contains(termo) ||
                    p.Nome.ToLower().Contains(termo) ||
                .ToList();

            if (encontrados.Count == 0)
            {
                Console.WriteLine("Nenhum produto encontrado.");
            }
            else
            {
                Console.WriteLine("\n--- PRODUTOS ENCONTRADOS ---");
                for (int i = 0; i < encontrados.Count; i++)
                {
                    var p = encontrados[i];
                    string disponibilidade = p.Quantidade > 0 ? "Disponível" : "Indisponível";
                    Console.WriteLine($"{i + 1}. {p} - {disponibilidade}");
                }

                Console.Write("\nDeseja adicionar algum item ao carrinho? (S/N): ");
                var opcao = Console.ReadLine()?.Trim().ToUpper();

                if (opcao == "S")
                {
                    Console.Write("Digite o número do item desejado: ");
                    if (int.TryParse(Console.ReadLine(), out int indice) &&
                        indice >= 1 && indice <= encontrados.Count)
                    {
                        var produtoEscolhido = encontrados[indice - 1];

                        if (produtoEscolhido.Quantidade == 0)
                        {
                            Console.WriteLine("Produto está indisponível no estoque.");
                        }
                        else
                        {
                            Console.Write($"Quantidade (máx. {produtoEscolhido.Quantidade}): ");
                            if (int.TryParse(Console.ReadLine(), out int qtd) && qtd > 0)
                            {
                                if (qtd <= produtoEscolhido.Quantidade)
                                {
                                    _carrinho.AdicionarItem(produtoEscolhido, qtd);
                                    produtoEscolhido.Quantidade -= qtd; // Atualiza o estoque!
                                    Console.WriteLine("Produto adicionado ao carrinho!");
                                }
                                else
                                {
                                    Console.WriteLine($"Quantidade indisponível. Máximo: {produtoEscolhido.Quantidade}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Quantidade inválida.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Índice inválido.");
                    }
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void ExibirCarrinho()
        {
            Console.Clear();
            Console.WriteLine(_carrinho);
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
