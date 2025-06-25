namespace Telas
{
    using Servicos;
    using Modelos;
    using System;

    public class TelaFornecedor
    {
        private readonly ServicoFornecedor _servicoFornecedor;

        private readonly TelaEndereco _telaEndereco;

        public TelaFornecedor(ServicoFornecedor servicoFornecedor, TelaEndereco telaEndereco)
        {
            _servicoFornecedor = servicoFornecedor;
            _telaEndereco = telaEndereco;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== FORNECEDORES ===");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar todos");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("5 - Buscar por código");
                Console.WriteLine("6 - Buscar por nome");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        Cadastrar();
                        break;
                    case "2":
                        Listar();
                        break;
                    case "3":
                        Alterar();
                        break;
                    case "4":
                        Remover();
                        break;
                    case "5":
                        BuscarPorCodigo();
                        break;
                    case "6":
                        BuscarPorNome();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        PressioneParaContinuar();
                        continue;
                }
            }
        }

        private void Cadastrar()
        {
            Console.Clear();

            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine() ?? "";

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine() ?? "";

            Endereco endereco = _telaEndereco.Cadastrar();

            var fornecedor = new Fornecedor(codigo, nome, descricao, telefone, email, cnpj, endereco);

            try
            {
                _servicoFornecedor.Cadastrar(fornecedor);
                Console.WriteLine("Fornecedor cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar fornecedor: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var fornecedores = _servicoFornecedor.ListarTodos();

            if (!fornecedores.Any())
            {
                Console.WriteLine("Nenhum fornecedor cadastrado.");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Lista de Transportadoras ---");
            foreach (var f in fornecedores)
                Console.WriteLine(f);

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var fornecedorAtual = _servicoFornecedor.BuscarPorCodigo(codigo);

            if (fornecedorAtual == null)
            {
                Console.WriteLine("Fornecedor não encontrado!");
                PressioneParaContinuar();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Dados atuais ---");
                Console.WriteLine(fornecedorAtual);
                Console.WriteLine("\nQual campo deseja alterar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Descricao");
                Console.WriteLine("3 - Telefone");
                Console.WriteLine("4 - Email");
                Console.WriteLine("5 - CNPJ");
                Console.WriteLine("6 - Endereço");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                string novoNome = fornecedorAtual.Nome;
                string novaDescricao = fornecedorAtual.Descricao;
                string novoTelefone = fornecedorAtual.Telefone;
                string novoEmail = fornecedorAtual.Email;
                string novoCnpj = fornecedorAtual.Cnpj;
                Endereco novoEndereco = fornecedorAtual.Endereco;

                switch (opcao)
                {
                    case "1":
                        Console.Write("Novo nome: ");
                        novoNome = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        Console.Write("Novo descrição: ");
                        novaDescricao = Console.ReadLine() ?? "";
                        break;
                    case "3":
                        Console.Write("Novo telefone: ");
                        novoTelefone = Console.ReadLine() ?? "";
                        break;
                    case "4":
                        Console.Write("Novo email: ");
                        novoEmail = Console.ReadLine() ?? "";
                        break;
                    case "5":
                        Console.Write("Novo CNPJ: ");
                        novoCnpj = Console.ReadLine() ?? "";
                        break;
                    case "6":
                        novoEndereco = _telaEndereco.Cadastrar();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        continue;
                }

                var fornecedorAlterado = new Fornecedor(
                    fornecedorAtual.Codigo,
                    novoNome,
                    novaDescricao,
                    novoTelefone,
                    novoEmail,
                    novoCnpj,
                    novoEndereco
                );

                try
                {
                    _servicoFornecedor.Alterar(fornecedorAtual, fornecedorAlterado);
                    Console.WriteLine("Fornecedor alterado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao alterar fornecedor: {ex.Message}");
                }

                PressioneParaContinuar();
                break;
            }
        }

        private void Remover()
        {
            Console.Clear();
            Console.WriteLine("Informe o código do fornecedor:");
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }

            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codigo);

            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado.");
                PressioneParaContinuar();
                return;
            }

            try
            {
                _servicoFornecedor.Remover(fornecedor);
                Console.WriteLine("Fornecedor removido com sucesso!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void BuscarPorCodigo()
        {
            Console.Clear();
            Console.WriteLine("Informe o código:");
            int codigo;
            while (true)
            {
                Console.Write("Código: ");
                if (int.TryParse(Console.ReadLine(), out codigo))
                    break;
                Console.WriteLine("Código inválido. Digite um número inteiro.");
            }
            var fornecedor = _servicoFornecedor.BuscarPorCodigo(codigo);
            if (fornecedor != null)
                Console.WriteLine(fornecedor);
            else
                Console.WriteLine("Fornecedor não encontrado!");
            PressioneParaContinuar();
        }

        private void BuscarPorNome()
        {
            Console.Clear();
            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine() ?? "";
            var fornecedores = _servicoFornecedor.BuscarPorNome(nome);

            if (!fornecedores.Any())
            {
                Console.WriteLine("Nenhum fornecedor encontrado!");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Fornecedores encontrados ---");
            foreach (var f in fornecedores)
                Console.WriteLine(f);

            PressioneParaContinuar();
        }

        private void PressioneParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}