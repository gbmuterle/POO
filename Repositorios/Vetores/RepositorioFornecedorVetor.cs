namespace Repositorios
{
    using System;
    using Modelos;

    public class RepositorioFornecedorVetor : IRepositorioFornecedor
    {
        private Fornecedor[] fornecedores = new Fornecedor[0];
        private readonly IArmazenamento<Fornecedor> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioFornecedorVetor(IArmazenamento<Fornecedor> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            fornecedores = _armazenamento.Carregar(_caminhoArquivo).ToArray();
        }

        public void Adicionar(Fornecedor fornecedor)
        {
            var novos = new Fornecedor[fornecedores.Length + 1];
            for (int i = 0; i < fornecedores.Length; i++)
                novos[i] = fornecedores[i];
            novos[novos.Length - 1] = fornecedor;
            fornecedores = novos;
            Salvar();
        }

        public void Alterar(Fornecedor fornecedorAtual, Fornecedor fornecedorAlterado)
        {
            for (int i = 0; i < fornecedores.Length; i++)
            {
                if (fornecedores[i].Codigo == fornecedorAlterado.Codigo)
                {
                    fornecedores[i] = fornecedorAlterado;
                    Salvar();
                    break;
                }
            }
        }

        public void Remover(Fornecedor fornecedor)
        {
            int index = -1;
            for (int i = 0; i < fornecedores.Length; i++)
            {
                if (fornecedores[i].Codigo == fornecedor.Codigo)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var novos = new Fornecedor[fornecedores.Length - 1];
                int j = 0;
                for (int i = 0; i < fornecedores.Length; i++)
                {
                    if (i != index)
                        novos[j++] = fornecedores[i];
                }
                fornecedores = novos;
                Salvar();
            }
        }

        public Fornecedor? BuscarPorCodigo(int codigo)
        {
            for (int i = 0; i < fornecedores.Length; i++)
            {
                if (fornecedores[i].Codigo == codigo)
                    return fornecedores[i];
            }
            return null;
        }

        public List<Fornecedor> BuscarTodos()
        {
            var lista = new List<Fornecedor>();
            foreach (var f in fornecedores)
            {
                lista.Add(f);
            }
            return lista;
        }

        public List<Fornecedor> BuscarPorNome(string nome)
        {
            var lista = new List<Fornecedor>();
            for (int i = 0; i < fornecedores.Length; i++)
            {
                if (fornecedores[i].Nome.ToLower().Contains(nome.ToLower()))
                    lista.Add(fornecedores[i]);
            }
            return lista;
        }

        public void Salvar()
        {
            _armazenamento.Salvar(fornecedores.ToList(), _caminhoArquivo);
        }
    }
}