using System;
using Modelos;

namespace Repositorios.Vetor
{
    public class RepFornecedorVetor
    {
        private Fornecedor[] todosFornecedores = new Fornecedor[0];

        public void Adicionar(Fornecedor fornecedorNovo)
        {
            if (string.IsNullOrWhiteSpace(fornecedorNovo.Nome))
            {
                throw new Exception("O fornecedor precisa ter um nome para ser cadastrado.");
            }
            // Expande o vetor
            Fornecedor[] novosFornecedores = new Fornecedor[todosFornecedores.Length + 1];
            for (int i = 0; i < todosFornecedores.Length; i++)
            {
                novosFornecedores[i] = todosFornecedores[i];
            }
            novosFornecedores[novosFornecedores.Length - 1] = fornecedorNovo;
            todosFornecedores = novosFornecedores;
        }

        public Fornecedor[] ObterTodos()
        {
            return todosFornecedores.ToArray();
        }

        public Fornecedor? BuscarPorCodigo(int codigo)
        {
            for (int i = 0; i < todosFornecedores.Length; i++)
            {
                if (todosFornecedores[i] != null && todosFornecedores[i].Codigo == codigo)
                {
                    return todosFornecedores[i];
                }
            }
            return null;
        }

        public void Alterar(Fornecedor fornecedorAlterado)
        {
            for (int i = 0; i < todosFornecedores.Length; i++)
            {
                if (todosFornecedores[i] != null && todosFornecedores[i].Codigo == fornecedorAlterado.Codigo)
                {
                    todosFornecedores[i] = fornecedorAlterado;
                    return;
                }
            }
            throw new Exception("Fornecedor não encontrado para alteração.");
        }

        public bool Remover(int codigo)
        {
            int index = -1;
            for (int i = 0; i < todosFornecedores.Length; i++)
            {
                if (todosFornecedores[i] != null && todosFornecedores[i].Codigo == codigo)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return false;

            Fornecedor[] novosFornecedores = new Fornecedor[todosFornecedores.Length - 1];
            for (int i = 0, j = 0; i < todosFornecedores.Length; i++)
            {
                if (i == index) continue;
                novosFornecedores[j] = todosFornecedores[i];
                j++;
            }

            todosFornecedores = novosFornecedores;
            return true;
        }
    }
}