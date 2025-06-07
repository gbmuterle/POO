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
            var produtoRepositorio = new ProdutoRepositorioLista();
            var fornecedorRepositorio = new FornecedorRepositorioLista();
            var transportadoraRepositorio = new TransportadoraRepositorioLista();

            // Instanciar serviços usando o novo padrão
            var servicoProduto = new ServicoProduto(produtoRepositorio);
            var servicoFornecedor = new ServicoFornecedor(fornecedorRepositorio);
            var servicoTransportadora = new ServicoTransportadora(transportadoraRepositorio);

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