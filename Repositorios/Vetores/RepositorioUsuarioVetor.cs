namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioUsuarioVetor : IRepositorioUsuario
    {
        private Usuario[] usuarios = new Usuario[0];
        private readonly IArmazenamento<Usuario> _armazenamento;
        private readonly string _caminhoArquivo;
        private readonly Usuario admin = new Usuario("admin", "admin", "admin", "11999999999", "admin@email.com", new Endereco(
            "Rua", "1234", "Sede ERP", "Centro", "Flores da Cunha", "RS", "95270000")
        );

        public RepositorioUsuarioVetor(IArmazenamento<Usuario> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            usuarios = _armazenamento.Carregar(_caminhoArquivo).ToArray();
             if (usuarios.Length == 0)
            {
                Cadastrar(admin);
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            var novos = new Usuario[usuarios.Length + 1];
            for (int i = 0; i < usuarios.Length; i++)
                novos[i] = usuarios[i];
            novos[novos.Length - 1] = usuario;
            usuarios = novos;
            _armazenamento.Salvar(usuarios, _caminhoArquivo);
        }

        public void Alterar(Usuario usuarioAtual, Usuario usuarioAlterado)
        {
            for (int i = 0; i < usuarios.Length; i++)
            {
                if (usuarios[i].Nome == usuarioAlterado.Nome)
                {
                    usuarios[i] = usuarioAlterado;
                    _armazenamento.Salvar(usuarios, _caminhoArquivo);
                    break;
                }
            }
        }

        public void Remover(Usuario usuario)
        {
            int index = -1;
            for (int i = 0; i < usuarios.Length; i++)
            {
                if (usuarios[i].Nome == usuario.Nome)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var novos = new Usuario[usuarios.Length - 1];
                int j = 0;
                for (int i = 0; i < usuarios.Length; i++)
                {
                    if (i != index)
                        novos[j++] = usuarios[i];
                }
                usuarios = novos;
                _armazenamento.Salvar(usuarios, _caminhoArquivo);
            }
        }

        public Usuario? BuscarPorNome(string nome)
        {
            for (int i = 0; i < usuarios.Length; i++)
            {
                if (usuarios[i].Nome == nome)
                    return usuarios[i];
            }
            return null;
        }

        public List<Usuario> BuscarTodos()
        {
            var lista = new List<Usuario>();
            foreach (var u in usuarios)
            {
                lista.Add(u);
            }
            return lista;
        }
    }
}