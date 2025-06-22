using System;
using System.Collections.Generic;
using System.Linq;

namespace Modelos
{
    public class Carrinho
    {
        private readonly List<ItemPedido> _itens;

        public Carrinho()
        {
            _itens = new List<ItemPedido>();
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            var itemExistente = _itens.FirstOrDefault(i => i.Produto.Codigo == produto.Codigo);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                _itens.Add(new ItemPedido(produto, quantidade));
            }
        }

        public List<ItemPedido> ListarItens()
        {
            return _itens;
        }

        public double CalcularTotal()
        {
            return _itens.Sum(i => i.ValorTotal);
        }

        public void Limpar()
        {
            _itens.Clear();
        }

        public bool EstaVazio()
        {
            return !_itens.Any();
        }

        public override string ToString()
        {
            if (_itens.Count == 0)
                return "Carrinho vazio.";

            var resultado = "Itens no carrinho:\n";
            foreach (var item in _itens)
            {
                resultado += item + "\n";
            }
            resultado += $"TOTAL: {CalcularTotal():C}";
            return resultado;
        }
    }
}
