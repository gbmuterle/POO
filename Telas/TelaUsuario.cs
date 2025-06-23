namespace Telas
{
    using System;
    using Modelos;
    using Servicos;

    public class TelaUsuario
    {
        private readonly ServicoUsuario _servicoUsuario;

        public TelaUsuario(ServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
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

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            Console.Write("Perfil (admin/usuario): ");
            string perfil = (Console.ReadLine() ?? "").ToLower();
            var usuario = new Usuario(nome, senha, perfil);

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
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                string novaSenha = usuarioAtual.Senha;
                string novoPerfil = usuarioAtual.Perfil;

                switch (opcao)
                {
                    case "1":
                        Console.Write("Nova senha: ");
                        novaSenha = Console.ReadLine() ?? "";
                        break;
                    case "2":
                        Console.Write("Novo perfil (admin/usuario): ");
                        novoPerfil = (Console.ReadLine() ?? "").ToLower();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        PressioneParaContinuar();
                        continue;
                }

                var usuarioAlterado = new Usuario(
                    usuarioAtual.Nome,
                    novaSenha,
                    novoPerfil
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