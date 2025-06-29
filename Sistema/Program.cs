using System;
using Telas;
using Servicos;
using Repositorios;
using Autenticacao;
using Modelos;
using Sistema.Configuracoes;

namespace Sistema
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escolha o tipo de repositório:");
            Console.WriteLine("1 - Vetor");
            Console.WriteLine("2 - Lista");
            string tipoRepositorio = "";
            while (true)
            {
                Console.Write("Opção: ");
                tipoRepositorio = Console.ReadLine() ?? "";
                if (tipoRepositorio == "1" || tipoRepositorio == "2")
                    break;
                else
                {
                    Console.WriteLine("Opção inválida. Encerrando o programa.");
                    return;
                }
            }

            var configuracaoArquivos = new ConfiguracaoArquivos(tipoRepositorio);
            configuracaoArquivos.InicializarDiretorios();

            IRepositorioProduto repositorioProduto;
            IRepositorioFornecedor repositorioFornecedor;
            IRepositorioTransportadora repositorioTransportadora;
            IRepositorioPedido repositorioPedido;
            IRepositorioUsuario repositorioUsuario;
            IRepositorioCarrinho repositorioCarrinho;

            if (tipoRepositorio == "1")
            {
                var armazenamentoFornecedores = new ArmazenamentoJson<Fornecedor>();
                var armazenamentoTransportadoras = new ArmazenamentoJson<Transportadora>();
                var armazenamentoProdutos = new ArmazenamentoJson<Produto>();
                var armazenamentoPedidos = new ArmazenamentoJson<Pedido>();
                var armazenamentoUsuarios = new ArmazenamentoJson<Usuario>();

                repositorioProduto = new RepositorioProdutoVetor(armazenamentoProdutos, configuracaoArquivos.ArquivoProdutos);
                repositorioFornecedor = new RepositorioFornecedorVetor(armazenamentoFornecedores, configuracaoArquivos.ArquivoFornecedores);
                repositorioTransportadora = new RepositorioTransportadoraVetor(armazenamentoTransportadoras, configuracaoArquivos.ArquivoTransportadoras);
                repositorioPedido = new RepositorioPedidoVetor(armazenamentoPedidos, configuracaoArquivos.ArquivoPedidos);
                repositorioUsuario = new RepositorioUsuario();
                repositorioCarrinho = new RepositorioCarrinho();
            }
            else
            {
                var armazenamentoFornecedores = new ArmazenamentoJson<Fornecedor>();
                var armazenamentoTransportadoras = new ArmazenamentoJson<Transportadora>();
                var armazenamentoProdutos = new ArmazenamentoJson<Produto>();
                var armazenamentoPedidos = new ArmazenamentoJson<Pedido>();
                var armazenamentoUsuarios = new ArmazenamentoJson<Usuario>();

                repositorioProduto = new RepositorioProdutoLista(armazenamentoProdutos, configuracaoArquivos.ArquivoProdutos);
                repositorioFornecedor = new RepositorioFornecedorLista(armazenamentoFornecedores, configuracaoArquivos.ArquivoFornecedores);
                repositorioTransportadora = new RepositorioTransportadoraLista(armazenamentoTransportadoras, configuracaoArquivos.ArquivoTransportadoras);
                repositorioPedido = new RepositorioPedidoLista(armazenamentoPedidos, configuracaoArquivos.ArquivoPedidos);
                repositorioUsuario = new RepositorioUsuario();
                repositorioCarrinho = new RepositorioCarrinho();
            }

            var autenticador = new Autenticador(repositorioUsuario.ListarTodos());
            var telaLogin = new TelaLogin(autenticador);

            Usuario usuarioLogado = telaLogin.Executar();

            var servicoProduto = new ServicoProduto(repositorioProduto);
            var servicoFornecedor = new ServicoFornecedor(repositorioFornecedor);
            var servicoTransportadora = new ServicoTransportadora(repositorioTransportadora);
            var servicoUsuario = new ServicoUsuario(repositorioUsuario);
            var servicoPedido = new ServicoPedido(repositorioPedido);
            var servicoEndereco = new ServicoEndereco();
            var servicoCarrinho = new ServicoCarrinho(repositorioCarrinho);

            var telaEndereco = new TelaEndereco(servicoEndereco);
            var telaProduto = new TelaProduto(servicoProduto, servicoFornecedor);
            var telaFornecedor = new TelaFornecedor(servicoFornecedor, telaEndereco);
            var telaTransportadora = new TelaTransportadora(servicoTransportadora);
            var telaUsuario = new TelaUsuario(servicoUsuario, telaEndereco);
            var telaPedido = new TelaPedido(servicoPedido, servicoTransportadora);
            var telaCarrinho = new TelaCarrinho(servicoProduto, servicoCarrinho);

            var telaMenu = new TelaMenu(telaProduto, telaFornecedor, telaTransportadora, telaUsuario, telaPedido, telaCarrinho, usuarioLogado);
            telaMenu.Menu();
        }
    }
}