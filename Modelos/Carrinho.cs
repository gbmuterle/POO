using System;
using System.Collections.Generic;
using System.Linq;

namespace Modelos
{
    public class Carrinho
    {
        public Usuario Cliente { get; set; }
        private readonly List<ItemPedido> _itens;
        public IReadOnlyList<ItemPedido> Itens => _itens.AsReadOnly();
        public List<ItemPedido> ItensInternos => _itens;
        public double ValorTotal => _itens.Sum(i => i.ValorTotal);

        public Carrinho(Usuario cliente)
        {
            Cliente = cliente;
            _itens = new List<ItemPedido>();
        }
        
        public override string ToString()
        {
            if (!_itens.Any())
            {
                return "Carrinho vazio";
            }
            
            var result = $"Itens no carrinho:\n";
            foreach (var item in _itens)
            {
                result += $"{item.Produto.Nome} - {item.Quantidade} un. - {item.ValorTotal:C}\n";
            }
            result += $"\nValor Total: {ValorTotal:C}";
            
            return result;
        }
    }
}
