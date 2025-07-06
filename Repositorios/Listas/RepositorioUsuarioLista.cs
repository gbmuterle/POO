namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioUsuarioLista : IRepositorioUsuario
    {
        private List<Usuario> usuarios = new List<Usuario>();

        private readonly IArmazenamento<Usuario> _armazenamento;
        private readonly string _caminhoArquivo;
        private readonly Usuario admin = new Usuario("admin", "admin", "admin", "11999999999", "admin@email.com", new Endereco(
            "Rua", "1234", "Sede ERP", "Centro", "Flores da Cunha", "RS", "95270000")
        );


        public RepositorioUsuarioLista(IArmazenamento<Usuario> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            usuarios = _armazenamento.Carregar(_caminhoArquivo);
            if (!usuarios.Any())
            {
                Cadastrar(admin);
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            usuarios.Add(usuario);
            Salvar();
        }

        public void Alterar(Usuario usuarioAtual, Usuario usuarioAlterado)
        {
            usuarioAtual.Senha = usuarioAlterado.Senha;
            usuarioAtual.Perfil = usuarioAlterado.Perfil;
            usuarioAtual.Telefone = usuarioAlterado.Telefone;
            usuarioAtual.Email = usuarioAlterado.Email;
            usuarioAtual.Endereco = usuarioAlterado.Endereco;
            Salvar();
        }

        public void Remover(Usuario usuario)
        {
            usuarios.Remove(usuario);
            Salvar();
        }
        public Usuario? BuscarPorNome(string nome)
        {
            return usuarios.FirstOrDefault(u => u.Nome == nome);
        }

        public List<Usuario> BuscarTodos()
        {
            return usuarios;
        }

        public void Salvar()
        {
            _armazenamento.Salvar(usuarios, _caminhoArquivo);
        }
    }
}