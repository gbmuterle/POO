namespace Repositorios
{
    using System;
    using Modelos;

    public class RepositorioFornecedorVetor : IRepositorioFornecedor
    {
        private Fornecedor[] fornecedores = new Fornecedor[0];

        public void Adicionar(Fornecedor fornecedor)
        {
            var novos = new Fornecedor[fornecedores.Length + 1];
            for (int i = 0; i < fornecedores.Length; i++)
                novos[i] = fornecedores[i];
            novos[novos.Length - 1] = fornecedor;
            fornecedores = novos;
        }

        public void Alterar(Fornecedor fornecedor)
        {
            for (int i = 0; i < fornecedores.Length; i++)
            {
                if (fornecedores[i].Codigo == fornecedor.Codigo)
                {
                    fornecedores[i] = fornecedor;
                    break;
                }
            }
        }

        public void Remover(int codigo)
        {
            int index = Array.FindIndex(fornecedores, f => f.Codigo == codigo);
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

        public System.Collections.Generic.List<Fornecedor> BuscarTodos()
        {
            return new System.Collections.Generic.List<Fornecedor>(fornecedores);
        }

        public System.Collections.Generic.List<Fornecedor> BuscarPorNome(string nome)
        {
            var lista = new System.Collections.Generic.List<Fornecedor>();
            for (int i = 0; i < fornecedores.Length; i++)
            {
                if (fornecedores[i].Nome.ToLower().Contains(nome.ToLower()))
                    lista.Add(fornecedores[i]);
            }
            return lista;
        }
    }
}