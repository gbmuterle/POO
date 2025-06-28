using System;

namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioCarrinho : ICarrinhoRepository
    {
        private readonly Dictionary<Usuario, Carrinho> _carrinhos;

        public RepositorioCarrinho()
        {
            _carrinhos = new Dictionary<Usuario, Carrinho>();
        }

        public void Adicionar(Usuario usuario, ItemPedido item)
        {
            if (!_carrinhos.ContainsKey(usuario))
            {
                _carrinhos[usuario] = new Carrinho(usuario);
            }
            
            _carrinhos[usuario].Itens.Add(item);
        }

        public void Alterar(Usuario usuario, ItemPedido item)
        {
            if (!_carrinhos.ContainsKey(usuario))
                return;

            var carrinho = _carrinhos[usuario];
            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.Produto.Codigo == item.Produto.Codigo);
            
            if (itemExistente != null)
            {
                var index = carrinho.Itens.IndexOf(itemExistente);
                carrinho.Itens[index] = item;
            }
        }

        public void Remover(Usuario usuario, ItemPedido item)
        {
            if (!_carrinhos.ContainsKey(usuario))
                return;

            var carrinho = _carrinhos[usuario];
            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.Produto.Codigo == item.Produto.Codigo);
            
            if (itemExistente != null)
            {
                carrinho.Itens.Remove(itemExistente);
            }
        }

        public Carrinho? Listar(Usuario usuario)
        {
            return _carrinhos.TryGetValue(usuario, out var carrinho) ? carrinho : null;
        }
    }
}