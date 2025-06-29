using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace Repositorios
{
    public class RepositorioCarrinho : IRepositorioCarrinho
    {
        private readonly List<Carrinho> _carrinhos = new();

        public void Adicionar(Carrinho carrinho, ItemPedido item)
        {
            carrinho.ItensInternos.Add(item);
        }

        public void Alterar(Carrinho carrinho, ItemPedido itemAtual, ItemPedido itemAlterado)
        {
            itemAtual.Quantidade = itemAlterado.Quantidade;
        }

        public void Remover(Carrinho carrinho, ItemPedido item)
        {
            carrinho.ItensInternos.Remove(item);
        }

        public void Limpar(Carrinho carrinho)
        {
            carrinho.ItensInternos.Clear();
        }

        public Carrinho ObterCarrinho(Usuario cliente)
        {
            var carrinho = _carrinhos.FirstOrDefault(c => c.Cliente.Nome == cliente.Nome);
            if (carrinho == null)
            {
                carrinho = new Carrinho(cliente);
                _carrinhos.Add(carrinho);
            }
            return carrinho;
        }

        public ItemPedido? BuscarItem(Carrinho carrinho, Produto produto)
        {
            return carrinho.ItensInternos.FirstOrDefault(i => i.Produto.Codigo == produto.Codigo);
        }

        public List<ItemPedido> ListarTodos(Carrinho carrinho)
        {
            return carrinho.ItensInternos.ToList();
        }
    }
}