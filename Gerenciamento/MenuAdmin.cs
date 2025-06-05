using System;
using Telas; // Supondo que suas Telas estão no projeto Operacoes (ou Telas)
using Repositorios.Vetor; // Nome do seu projeto/repositório pode variar
// using Modelos;  // se precisar das entidades

namespace Gerenciamento
{
    public static class MenuAdmin
    {
        public static void Iniciar()
        {
            // Instanciando os repositórios do projeto separado Repositorios
            var repProduto = new RepProdutoVetor();
            var repFornecedor = new RepFornecedorVetor();
            var repTransportadora = new RepTransportadoraVetor();

            // Instanciando as telas/passando o repositório como dependência
            var telaProduto = new TelaProduto(repProduto);
            var telaFornecedor = new TelaFornecedor(repFornecedor);
            var telaTransportadora = new TelaTransportadora(repTransportadora);

            bool continuar = true;

            while (continuar)
            {
                string tipoCadastro = ExibirMenuPrincipal();

                if (tipoCadastro == "0")
                {
                    continuar = false;
                    Console.WriteLine("Encerrando o sistema...");
                    return;
                }

                string titulo = tipoCadastro switch
                {
                    "1" => "Produtos",
                    "2" => "Fornecedores",
                    "3" => "Transportadoras",
                    _ => "Desconhecido"
                };

                string operacao = ExibirSubmenu(titulo);

                switch (tipoCadastro)
                {
                    case "1":
                        ComandoTelaProduto(telaProduto, operacao);
                        break;
                    case "2":
                        ComandoTelaFornecedor(telaFornecedor, operacao);
                        break;
                    case "3":
                        ComandoTelaTransportadora(telaTransportadora, operacao);
                        break;
                }

                if (operacao != "0")
                    Console.Clear();
            }
        }

        // Métodos auxiliares e submenus como antes...

        private static void ComandoTelaProduto(TelaProduto tela, string operacao)
        {
            switch (operacao)
            {
                case "1": tela.Cadastrar(); break;
                case "2": tela.Alterar(); break;
                case "3": tela.Consultar(); break;
                case "4": tela.Excluir(); break;
            }
        }
        private static void ComandoTelaFornecedor(TelaFornecedor tela, string operacao)
        {
            switch (operacao)
            {
                case "1": tela.Cadastrar(); break;
                case "2": tela.Alterar(); break;
                case "3": tela.Consultar(); break;
                case "4": tela.Excluir(); break;
            }
        }
        private static void ComandoTelaTransportadora(TelaTransportadora tela, string operacao)
        {
            switch (operacao)
            {
                case "1": tela.Cadastrar(); break;
                case "2": tela.Alterar(); break;
                case "3": tela.Consultar(); break;
                case "4": tela.Excluir(); break;
            }
        }

        private static string ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Menu Inicial:");
            Console.WriteLine("1 - Produtos");
            Console.WriteLine("2 - Fornecedores");
            Console.WriteLine("3 - Transportadoras");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            return Console.ReadLine()?.Trim() ?? "";
        }

        private static string ExibirSubmenu(string titulo)
        {
            Console.Clear();
            Console.WriteLine($"{titulo}:");
            Console.WriteLine("1 - Novo");
            Console.WriteLine("2 - Alterar");
            Console.WriteLine("3 - Consultar");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");

            return Console.ReadLine()?.Trim() ?? "";
        }
    }
}