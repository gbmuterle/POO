using Modelos;
using Servicos;
using System;
using System.Collections.Generic;
using System.Linq;

public class TelaCarrinho
{
    private readonly ServicoProduto _servicoProduto;
    private List<(Produto Produto, int Quantidade)> _itensCarrinho;

    public TelaCarrinho(ServicoProduto servicoProduto)
    {
        _servicoProduto = servicoProduto;
        _itensCarrinho = new List<(Produto, int)>();
    }

    public void Menu(Usuario usuario)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== CARRINHO DE COMPRAS ===");
            Console.WriteLine("1 - Buscar produto por código");
            Console.WriteLine("2 - Buscar produto por nome");
            Console.WriteLine("3 - Visualizar carrinho");
            Console.WriteLine("0 - Voltar");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    BuscarEAdicionarPorCodigo();
                    break;
                case "2":
                    BuscarEAdicionarPorNome();
                    break;
                case "3":
                    VisualizarCarrinho();
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

    private void BuscarEAdicionarPorCodigo()
    {
        Console.Clear();
        Console.Write("Digite o código do produto: ");
        if (int.TryParse(Console.ReadLine(), out int codigo))
        {
            var produto = _servicoProduto.BuscarPorCodigo(codigo);
            if (produto != null)
            {
                Console.WriteLine("\nProduto encontrado:");
                Console.WriteLine(produto);
                AdicionarAoCarrinho(produto);
            }
            else
            {
                Console.WriteLine("Produto não encontrado!");
                PressioneParaContinuar();
            }
        }
    }

    private void BuscarEAdicionarPorNome()
    {
        Console.Clear();
        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine() ?? "";
        
        var produtos = _servicoProduto.BuscarPorNome(nome).ToList();
        
        if (produtos.Any())
        {
            Console.WriteLine("\nProdutos encontrados:");
            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {produtos[i]}");
            }

            Console.Write("\nEscolha o número do produto desejado (0 para cancelar): ");
            if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= produtos.Count)
            {
                var produtoEscolhido = produtos[escolha - 1];
                AdicionarAoCarrinho(produtoEscolhido);
            }
            else if (escolha != 0)
            {
                Console.WriteLine("Opção inválida!");
                PressioneParaContinuar();
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado!");
            PressioneParaContinuar();
        }
    }

    private void AdicionarAoCarrinho(Produto produto)
    {
        Console.Write("\nQuantidade desejada: ");
        if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0)
        {
            Console.WriteLine($"\nTotal do item: R$ {produto.Valor * quantidade:F2}");
            Console.Write("Confirmar adição ao carrinho? (S/N): ");
            
            if (Console.ReadLine()?.ToUpper() == "S")
            {
                _itensCarrinho.Add((produto, quantidade));
                Console.WriteLine("Produto adicionado ao carrinho!");
            }
        }
        else
        {
            Console.WriteLine("Quantidade inválida!");
        }
        PressioneParaContinuar();
    }

    private void VisualizarCarrinho()
    {
        Console.Clear();
        Console.WriteLine("=== ITENS NO CARRINHO ===");
        
        if (!_itensCarrinho.Any())
        {
            Console.WriteLine("Carrinho vazio!");
        }
        else
        {
            foreach (var item in _itensCarrinho)
            {
                Console.WriteLine($"Produto: {item.Produto.Nome}");
                Console.WriteLine($"Quantidade: {item.Quantidade}");
                Console.WriteLine($"Valor unitário: R$ {item.Produto.Valor:F2}");
                Console.WriteLine($"Subtotal: R$ {item.Produto.Valor * item.Quantidade:F2}");
                Console.WriteLine("------------------------");
            }
        }
        PressioneParaContinuar();
    }

    private void PressioneParaContinuar()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}