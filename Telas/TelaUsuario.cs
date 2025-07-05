namespace Telas
{
    using System;
    using Modelos;
    using Servicos;

    public class TelaUsuario
    {
        private readonly ServicoUsuario _servicoUsuario;
        private readonly TelaEndereco _telaEndereco;

        public TelaUsuario(ServicoUsuario servicoUsuario, TelaEndereco telaEndereco)
        {
            _servicoUsuario = servicoUsuario;
            _telaEndereco = telaEndereco;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== USUÁRIOS ===");
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Listar");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("0 - Voltar");
                Console.Write("\nEscolha uma opção: ");
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
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        continue;
                }
            }
        }
        private void Cadastrar()
        {
            Console.Clear();
            Console.Write("Nome: ");
            string nome = (Console.ReadLine() ?? "").ToLower();

            Console.Write("Perfil (admin/cliente): ");
            string perfil = (Console.ReadLine() ?? "").ToLower();

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            Console.Write("E-mail: ");
            string telefone = Console.ReadLine() ?? "";

            Console.Write("Telefone: ");
            string email = Console.ReadLine() ?? "";

            Endereco endereco = _telaEndereco.Cadastrar();
            var usuario = new Usuario(nome, senha, perfil, telefone, email, endereco);

            try
            {
                _servicoUsuario.Cadastrar(usuario);
                Console.WriteLine("Usuário cadastrado com sucesso!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar usuário: {ex.Message}");
            }

            PressioneParaContinuar();
        }

        private void Listar()
        {
            Console.Clear();
            var usuarios = _servicoUsuario.BuscarTodos();

            if (!usuarios.Any())
            {
                Console.WriteLine("Nenhum usuário cadastrado.");
                PressioneParaContinuar();
                return;
            }

            Console.WriteLine("--- Lista de usuários ---");
            foreach (var u in usuarios)
            {
                Console.WriteLine(u);
            }

            PressioneParaContinuar();
        }

        private void Alterar()
        {
            Console.Clear();
            Console.Write("Nome do usuário a alterar: ");
            string nome = (Console.ReadLine() ?? "").ToLower();

            var usuarioAtual = _servicoUsuario.BuscarPorNome(nome);

            if (usuarioAtual == null)
            {
                Console.WriteLine("Usuário não encontrado!");
                PressioneParaContinuar();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Dados atuais ---");
                Console.WriteLine(usuarioAtual);
                Console.WriteLine("\nQual campo deseja alterar?");
                Console.WriteLine("1 - Senha");
                Console.WriteLine("2 - Perfil");
                Console.WriteLine("3 - Telefone");
                Console.WriteLine("4 - Email");
                Console.WriteLine("5 - Endereço");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                string novaSenha = usuarioAtual.Senha;
                string novoPerfil = usuarioAtual.Perfil;
                string novoTelefone = usuarioAtual.Telefone;
                string novoEmail = usuarioAtual.Email;
                Endereco novoEndereco = usuarioAtual.Endereco;

                switch (opcao)
                {
                    case "1":
                        Console.Write("Nova senha: ");
                        novaSenha = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        Console.Write("Novo perfil (admin/cliente): ");
                        novoPerfil = (Console.ReadLine() ?? "").ToLower();
                        break;
                    case "3":
                        Console.Write("Novo telefone: ");
                        novoTelefone = (Console.ReadLine() ?? "").ToLower();
                        break;
                    case "4":
                        Console.Write("Novo e-mail: ");
                        novoEmail = (Console.ReadLine() ?? "").ToLower();
                        break;
                    case "5":
                        Console.Write("Novo endereço: ");
                        novoEndereco = _telaEndereco.Cadastrar();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        continue;
                }

                var usuarioAlterado = new Usuario(
                    usuarioAtual.Nome,
                    novaSenha,
                    novoPerfil,
                    novoTelefone,
                    novoEmail,
                    novoEndereco
                );

                try
                {
                    _servicoUsuario.Alterar(usuarioAtual, usuarioAlterado);
                    Console.WriteLine("Usuário alterado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao alterar usuário: {ex.Message}");
                }

                PressioneParaContinuar();
                break;
            }
        }
        private void Remover()
        {
            Console.Clear();
            Console.Write("Informe o nome do usuário que deseja remover: ");
            string nome = (Console.ReadLine() ?? "").ToLower();

            var usuario = _servicoUsuario.BuscarPorNome(nome);

            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                PressioneParaContinuar();
                return;
            }

            try
            {
                _servicoUsuario.Remover(usuario);
                Console.WriteLine("Usuário removido com sucesso!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            PressioneParaContinuar();
        }
        private void PressioneParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}