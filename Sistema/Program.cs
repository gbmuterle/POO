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

            var telaProduto = new TelaProduto(servicoProduto, servicoFornecedor);
            var telaFornecedor = new TelaFornecedor(servicoFornecedor);
            var telaTransportadora = new TelaTransportadora(servicoTransportadora);
            var telaUsuario = new TelaUsuario(servicoUsuario);

            Usuario? usuarioLogado = null;
            do
            {
                usuarioLogado = telaLogin.Executar();
            } while (usuarioLogado == null);

            if (usuarioLogado.Perfil == "admin")
            {
                var telaMenuAdmin = new TelaMenuAdmin(telaProduto, telaFornecedor, telaTransportadora, telaUsuario);
                telaMenuAdmin.Exibir();
            }
            else
            {
                //var telaMenuUsuario = new TelaMenuUsuario(telaProduto, telaFornecedor, telaTransportadora);
                //telaMenuUsuario.Exibir();
            }
        }
    }
}