using Modelos;
using Servicos;
using System;
using System.Collections.Generic;
using System.Linq;

public class TelaCarrinho
{
    private readonly ServicoProduto _servicoProduto;
    private readonly ServicoCarrinho _servicoCarrinho;
    private readonly ServicoTransportadora _servicoTransportadora;
    private readonly List<ItemPedido> _itensCarrinho = new();

    public TelaCarrinho(ServicoProduto servicoProduto, ServicoCarrinho servicoCarrinho, ServicoTransportadora servicoTransportadora)
    {
        _servicoProduto = servicoProduto;
        _servicoCarrinho = servicoCarrinho;
        _servicoTransportadora = servicoTransportadora;
    }

    public void Menu(Usuario usuario)
    {
        var carrinho = _servicoCarrinho.ObterCarrinho(usuario);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== CARRINHO DE COMPRAS ===");
            Console.WriteLine("1 - Adicionar Produto");
            Console.WriteLine("2 - Alterar Produto");
            Console.WriteLine("3 - Remover Produto");
            Console.WriteLine("4 - Finalizar compra");
            Console.WriteLine("0 - Voltar");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    Adicionar(carrinho);
                    break;
                case "2":
                    Alterar(carrinho);
                    break;
                case "3":
                    Remover(carrinho);
                    break;
                case "4":
                    Finalizar(carrinho);
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

    private void Adicionar(Carrinho carrinho)
    {
        var produto = BuscarProduto();
        Console.Clear();
        Console.WriteLine(produto.InfoProduto());

        int quantidade;
        while (true)
        {
            Console.Write("Digite a quantidade: ");
            if (int.TryParse(Console.ReadLine(), out quantidade) && quantidade > 0)
                break;
            Console.WriteLine("Quantidade inválida!");
        }

        var item = new ItemPedido(produto, quantidade);
        try
        {
            _servicoCarrinho.AdicionarItem(carrinho, item);
            Console.WriteLine("Produto adicionado ao carrinho!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        PressioneParaContinuar();
    }

    private void Alterar(Carrinho carrinho)
    {
        var itens = _servicoCarrinho.BuscarTodos(carrinho);
        if (!itens.Any())
        {
            Console.WriteLine("Carrinho vazio!");
            PressioneParaContinuar();
            return;
        }

        Console.WriteLine("Produtos no carrinho:");
        foreach (var i in itens)
            Console.WriteLine(i);

        int codigo;
        while (true)
        {
            Console.Write("Digite o código do produto que deseja alterar: ");
            if (int.TryParse(Console.ReadLine(), out codigo))
                break;
            Console.WriteLine("Código inválido. Digite um número inteiro.");
        }

        var itemAtual = itens.FirstOrDefault(i => i.Produto.Codigo == codigo);
        if (itemAtual == null)
        {
            Console.WriteLine("Produto não encontrado no carrinho!");
            PressioneParaContinuar();
            return;
        }

        Console.Write("Nova quantidade: ");
        int novaQuantidade;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out novaQuantidade) && novaQuantidade > 0)
                break;
            Console.WriteLine("Quantidade inválida!");
        }

        var itemAlterado = new ItemPedido(
            itemAtual.Produto,
            novaQuantidade
        );

        try
        {
            _servicoCarrinho.AlterarItem(carrinho, itemAtual, itemAlterado);
            Console.WriteLine("Quantidade alterada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        PressioneParaContinuar();
    }

    private void Remover(Carrinho carrinho)
    {
        var itens = _servicoCarrinho.BuscarTodos(carrinho);
        if (!itens.Any())
        {
            Console.WriteLine("Carrinho vazio!");
            PressioneParaContinuar();
            return;
        }

        Console.WriteLine("Produtos no carrinho:");
        foreach (var i in itens)
            Console.WriteLine(i);

        int codigo;
        while (true)
        {
            Console.Write("Digite o código do produto que deseja alterar: ");
            if (int.TryParse(Console.ReadLine(), out codigo))
                break;
            Console.WriteLine("Código inválido. Digite um número inteiro.");
        }

        var item = itens.FirstOrDefault(i => i.Produto.Codigo == codigo);
        if (item == null)
        {
            Console.WriteLine("Produto não encontrado no carrinho!");
            PressioneParaContinuar();
            return;
        }

        try
        {
            _servicoCarrinho.RemoverItem(carrinho, item);
            Console.WriteLine("Produto removido do carrinho!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        PressioneParaContinuar();
    }

    private void Finalizar(Carrinho carrinho)
    {
        double distancia;
        while (true)
            {
                Console.Write("Informe a distância em km: ");
                if (double.TryParse(Console.ReadLine(), out distancia) && distancia > 0 && distancia <= 5000)
                    break;
                Console.WriteLine("Distância inválida.");
            }

        var transportadoras = _servicoTransportadora.BuscarTodos();
        if (!transportadoras.Any())
        {
            Console.WriteLine("Nenhuma transportadora disponível!");
            PressioneParaContinuar();
            return;
        }

        Console.WriteLine("Transportadoras:");
        foreach (var t in transportadoras)
            Console.WriteLine(t.InfoTransportadora());

        int codTransportadora;
        Transportadora transportadora;
        while (true)
        {
            Console.Write("Código da transportadora: ");
            if (int.TryParse(Console.ReadLine(), out codTransportadora))
            {
                var transportadoraEncontrada = _servicoTransportadora.BuscarPorCodigo(codTransportadora);
                if (transportadoraEncontrada != null)
                {
                    transportadora = transportadoraEncontrada;
                    break;
                }
                Console.WriteLine("Transportadora não encontrada! Digite um código válido.");
            }
            else
            {
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }
        }

        try
        {
            _servicoCarrinho.Finalizar(carrinho, distancia, transportadora);
            Console.WriteLine("Compra finalizada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao finalizar a compra: {ex.Message}");
        }
        PressioneParaContinuar();
    }

    private Produto BuscarProduto()
    {
        Console.Clear();
        while (true)
        {
            Console.Write("Digite o código ou nome do produto: ");
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int codigo))
            {
                var produto = _servicoProduto.BuscarPorCodigo(codigo);
                if (produto != null)
                    return produto;
                Console.WriteLine("Produto não encontrado!");
            }
            else
            {
                var produtos = _servicoProduto.BuscarPorNome(entrada);
                if (produtos.Count == 1)
                    return produtos[0];
                if (produtos.Count > 1)
                {
                    Console.WriteLine("Produtos encontrados:");
                    foreach (var p in produtos)
                        Console.WriteLine(p.InfoProduto());
                    Console.Write("Digite o código do produto desejado: ");
                    if (int.TryParse(Console.ReadLine(), out int codEscolhido))
                    {
                        var produto = produtos.FirstOrDefault(p => p.Codigo == codEscolhido);
                        if (produto != null)
                            return produto;
                    }
                    Console.WriteLine("Código inválido.");
                }
                else
                {
                    Console.WriteLine("Nenhum produto encontrado!");
                }
            }
        }
    }

    private void PressioneParaContinuar()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}