using System;
using Telas;
using Servicos;
using Repositorios;

namespace Sistema
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciar repositórios usando os nomes antigos
            var repositorioProduto = new RepositorioProdutoLista();
            var repositorioFornecedor = new RepositorioFornecedorLista();
            var repositorioTransportadora = new RepositorioTransportadoraLista();

            // Instanciar serviços usando o novo padrão
            var servicoProduto = new ServicoProduto(repositorioProduto);
            var servicoFornecedor = new ServicoFornecedor(repositorioFornecedor);
            var servicoTransportadora = new ServicoTransportadora(repositorioTransportadora);

            // Instanciar telas usando o novo padrão
            var telaProduto = new TelaProduto(servicoProduto, servicoFornecedor);
            var telaFornecedor = new TelaFornecedor(servicoFornecedor);
            var telaTransportadora = new TelaTransportadora(servicoTransportadora);

            // Instanciar tela principal
            var telaMenuPrincipal = new TelaMenuPrincipal(telaProduto, telaFornecedor, telaTransportadora);

            // Executar o menu principal
            telaMenuPrincipal.Exibir();
        }
    }
}