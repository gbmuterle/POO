using System;
using Telas;
using Servicos;
using Repositorios;
using Autenticacao;
using Modelos;

namespace Sistema
{
    public class Gerenciador
    {
        public void Executar()
        {
            Console.WriteLine("Escolha o tipo de repositório:");
            Console.WriteLine("1 - Vetor");
            Console.WriteLine("2 - Lista");

            string tipoRepositorio;
            while (true)
            {
                Console.Write("Opção: ");
                tipoRepositorio = Console.ReadLine() ?? "";
                if (tipoRepositorio == "1" || tipoRepositorio == "2")
                    break;
                else
                {
                    Console.WriteLine("Opção inválida.");
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

            var armazenamentoFornecedores = new ArmazenamentoJson<Fornecedor>();
            var armazenamentoTransportadoras = new ArmazenamentoJson<Transportadora>();
            var armazenamentoProdutos = new ArmazenamentoJson<Produto>();
            var armazenamentoPedidos = new ArmazenamentoJson<Pedido>();
            var armazenamentoUsuarios = new ArmazenamentoJson<Usuario>();
            var armazenamentoCarrinhos = new ArmazenamentoJson<Carrinho>();

            if (tipoRepositorio == "1")
            {
                repositorioProduto = new RepositorioProdutoVetor(armazenamentoProdutos, configuracaoArquivos.ArquivoProdutos);
                repositorioFornecedor = new RepositorioFornecedorVetor(armazenamentoFornecedores, configuracaoArquivos.ArquivoFornecedores);
                repositorioTransportadora = new RepositorioTransportadoraVetor(armazenamentoTransportadoras, configuracaoArquivos.ArquivoTransportadoras);
                repositorioPedido = new RepositorioPedidoVetor(armazenamentoPedidos, configuracaoArquivos.ArquivoPedidos);
                repositorioUsuario = new RepositorioUsuarioVetor(armazenamentoUsuarios, configuracaoArquivos.ArquivoUsuarios);
                repositorioCarrinho = new RepositorioCarrinhoVetor(armazenamentoCarrinhos, configuracaoArquivos.ArquivoCarrinhos);
            }
            else
            {
                repositorioProduto = new RepositorioProdutoLista(armazenamentoProdutos, configuracaoArquivos.ArquivoProdutos);
                repositorioFornecedor = new RepositorioFornecedorLista(armazenamentoFornecedores, configuracaoArquivos.ArquivoFornecedores);
                repositorioTransportadora = new RepositorioTransportadoraLista(armazenamentoTransportadoras, configuracaoArquivos.ArquivoTransportadoras);
                repositorioPedido = new RepositorioPedidoLista(armazenamentoPedidos, configuracaoArquivos.ArquivoPedidos);
                repositorioUsuario = new RepositorioUsuarioLista(armazenamentoUsuarios, configuracaoArquivos.ArquivoUsuarios);
                repositorioCarrinho = new RepositorioCarrinhoLista(armazenamentoCarrinhos, configuracaoArquivos.ArquivoCarrinhos);
            }

            var autenticador = new Autenticador(repositorioUsuario.BuscarTodos());
            var telaLogin = new TelaLogin(autenticador);

            Usuario usuarioLogado = telaLogin.Executar();

            var servicoProduto = new ServicoProduto(repositorioProduto);
            var servicoFornecedor = new ServicoFornecedor(repositorioFornecedor);
            var servicoTransportadora = new ServicoTransportadora(repositorioTransportadora);
            var servicoUsuario = new ServicoUsuario(repositorioUsuario);
            var servicoPedido = new ServicoPedido(repositorioPedido);
            var servicoEndereco = new ServicoEndereco();
            var servicoCarrinho = new ServicoCarrinho(repositorioCarrinho, servicoPedido);

            var telaEndereco = new TelaEndereco(servicoEndereco);
            var telaProduto = new TelaProduto(servicoProduto, servicoFornecedor);
            var telaFornecedor = new TelaFornecedor(servicoFornecedor, telaEndereco);
            var telaTransportadora = new TelaTransportadora(servicoTransportadora);
            var telaUsuario = new TelaUsuario(servicoUsuario, telaEndereco);
            var telaPedido = new TelaPedido(servicoPedido, servicoTransportadora);
            var telaCarrinho = new TelaCarrinho(servicoProduto, servicoCarrinho, servicoTransportadora);

            var telaMenu = new TelaMenu(
                telaProduto,
                telaFornecedor,
                telaTransportadora,
                telaUsuario,
                telaPedido,
                telaCarrinho,
                usuarioLogado
            );
            telaMenu.Menu();
        }
    }
}