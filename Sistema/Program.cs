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
            var repositorioUsuario = new RepositorioUsuario();
            var autenticador = new Autenticador(repositorioUsuario.BuscarTodos());
            var telaLogin = new TelaLogin(autenticador);

            ConfiguracaoArquivos.InicializarDiretorios();
            var armazenamentoFornecedores = new ArmazenamentoJson<Fornecedor>();
            var armazenamentoTransportadoras = new ArmazenamentoJson<Transportadora>();
            var armazenamentoProdutos = new ArmazenamentoJson<Produto>();
            var armazenamentoPedidos = new ArmazenamentoJson<Pedido>();
            var armazenamentoUsuarios = new ArmazenamentoJson<Usuario>();

            Usuario usuarioLogado = telaLogin.Executar();

            var repositorioProduto = new RepositorioProdutoLista();
            var repositorioFornecedor = new RepositorioFornecedorLista(armazenamentoFornecedores, ConfiguracaoArquivos.ArquivoFornecedores);
            var repositorioTransportadora = new RepositorioTransportadoraLista();
            var repositorioPedido = new RepositorioPedidoLista();

            var servicoProduto = new ServicoProduto(repositorioProduto);
            var servicoFornecedor = new ServicoFornecedor(repositorioFornecedor);
            var servicoTransportadora = new ServicoTransportadora(repositorioTransportadora);
            var servicoUsuario = new ServicoUsuario(repositorioUsuario);
            var servicoPedido = new ServicoPedido(repositorioPedido);
            var servicoEndereco = new ServicoEndereco();

            var telaEndereco = new TelaEndereco(servicoEndereco);
            var telaProduto = new TelaProduto(servicoProduto, servicoFornecedor);
            var telaFornecedor = new TelaFornecedor(servicoFornecedor, telaEndereco);
            var telaTransportadora = new TelaTransportadora(servicoTransportadora);
            var telaUsuario = new TelaUsuario(servicoUsuario, telaEndereco);
            var telaPedido = new TelaPedido(servicoPedido);
            var telaCarrinho = new TelaCarrinho(); // estou fazendo

            var telaMenu = new TelaMenu(telaProduto, telaFornecedor, telaTransportadora, telaUsuario, telaPedido, telaCarrinho, usuarioLogado);
            telaMenu.Menu();
        }
    }
}