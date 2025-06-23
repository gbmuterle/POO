using Autenticacao;
using Modelos;

namespace Telas
{
    public class TelaLogin
    {
        private readonly Autenticador _autenticador;

        public TelaLogin(Autenticador autenticador)
        {
            _autenticador = autenticador;
        }

        public Usuario Executar()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== LOGIN ===");
                Console.Write("Usuário: ");
                string nome = Console.ReadLine() ?? "";
                Console.Write("Senha: ");
                string senha = Console.ReadLine() ?? "";

                var usuario = _autenticador.Autenticar(nome, senha);

                if (usuario != null)
                {
                    return usuario;
                }
                else
                {
                    Console.WriteLine("Usuário ou senha inválidos.");
                    Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                }
            }
        }
    }
}