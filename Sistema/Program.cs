using System;
using Telas;
using Servicos;
using Repositorios;
using Autenticacao;
using Modelos;

namespace Sistema
{
    class Program
    {
        static void Main(string[] args)
        {
            var repositorioUsuario = new RepositorioUsuario();
            var autenticador = new Autenticador(repositorioUsuario.BuscarTodos());
            var telaLogin = new TelaLogin(autenticador);

            var repositorioProduto = new RepositorioProdutoLista();
            var repositorioFornecedor = new RepositorioFornecedorLista();
            var repositorioTransportadora = new RepositorioTransportadoraLista();

            var servicoProduto = new ServicoProduto(repositorioProduto);
            var servicoFornecedor = new ServicoFornecedor(repositorioFornecedor);
            var servicoTransportadora = new ServicoTransportadora(repositorioTransportadora);
            var servicoUsuario = new ServicoUsuario(repositorioUsuario);

            var servicoEndereco = new ServicoEndereco();
            var telaEndereco = new TelaEndereco(servicoEndereco);

            var telaProduto = new TelaProduto(servicoProduto, servicoFornecedor);
            var telaFornecedor = new TelaFornecedor(servicoFornecedor, telaEndereco);
            var telaTransportadora = new TelaTransportadora(servicoTransportadora);
            var telaUsuario = new TelaUsuario(servicoUsuario);

            Usuario usuarioLogado = telaLogin.Executar();

            if (usuarioLogado.Perfil == "admin")
            {
                var telaMenuAdmin = new TelaMenuAdmin(telaProduto, telaFornecedor, telaTransportadora, telaUsuario);
                telaMenuAdmin.Menu();
            }
            else
            {
                var telaMenuCliente = new TelaMenuCliente(servicoProduto);
                telaMenuCliente.Menu();
            }
        }
    }
}