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
                Console.WriteLine("=== MENU DE USUÁRIOS ===");
                Console.WriteLine("1 - Cadastrar usuário");
                Console.WriteLine("2 - Listar usuários");
                Console.WriteLine("3 - Alterar usuário");
                Console.WriteLine("4 - Remover usuário");
                Console.WriteLine("0 - Voltar");

                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();

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
                        Console.WriteLine("Opção inválida. Pressione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }
        private void Cadastrar()
        {

            string nome = InputObrigatorio("Nome");
            string senha = InputObrigatorio("Senha");
            string perfil = InputObrigatorio("Perfil").ToLower();
            var usuario = new Usuario(nome, senha, perfil);

            try
            {
                _servicoUsuario.CadastrarUsuario(usuario);
                Console.WriteLine("\nUsuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar usuário: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void Listar()
        {
            var usuarios = _servicoUsuario.BuscarTodos();
            Console.WriteLine("--- Lista de usuários ---");
            foreach (var user in usuarios)
            {
                Console.WriteLine($"Nome: {user.Nome}, Perfil: {user.Perfil}");
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void Alterar()
        {
            Console.Write("Informe o nome do usuário que deseja alterar: ");
            var nomeBusca = InputObrigatorio("Usuario");
            var usuario = _servicoUsuario.BuscarPorNome(nomeBusca);

            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
            }
            else
            {
                var novaSenha = InputAlteracao("Senha", usuario.Senha);
                var novoPerfil = InputAlteracao("Perfil", usuario.Perfil);
                usuario.Senha = novaSenha;
                usuario.Perfil = novoPerfil;
                try
                {
                    _servicoUsuario.AlterarUsuario(usuario);

                    Console.WriteLine("Usuário alterado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private void Remover()
        {
            Console.Write("Informe o nome do usuário que deseja remover: ");
            var nomeBusca = InputObrigatorio("Usuario");
            var usuario = _servicoUsuario.BuscarPorNome(nomeBusca);

            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
            }
            else
            {
                try
                {
                    _servicoUsuario.RemoverUsuario(usuario);
                    Console.WriteLine("Usuário removido com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        public void Executar()
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRAR USUÁRIOS ===");

            string nome = InputObrigatorio("Nome");
            string senha = InputObrigatorio("Senha");
            string perfil = InputObrigatorio("Perfil").ToLower();


        }

        private string InputObrigatorio(string campo)
        {
            string valor;
            do
            {
                Console.Write($"{campo}: ");
                valor = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(valor))
                {
                    Console.WriteLine($"{campo} não pode ser vazio. Tente novamente.");
                }
            } while (string.IsNullOrWhiteSpace(valor));
            return valor;
        }
        private string InputAlteracao(string campo, string valorAtual)
        {
            Console.Write($"{campo} ({valorAtual}): ");
            string valor = Console.ReadLine()?.Trim() ?? "";
            return string.IsNullOrEmpty(valor) ? valorAtual : valor;
        }
    }
}