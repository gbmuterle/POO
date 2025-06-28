using System;
using System.Collections.Generic;
using System.Linq;

namespace Modelos
{
    public class Carrinho
    {
        private readonly List<ItemPedido> _itens;
        public Usuario Usuario { get; private set; }

        public Carrinho(Usuario usuario)
        {
            Usuario = usuario;
            _itens = new List<ItemPedido>();
        }
        
        public override string ToString()
        {
            if (!_itens.Any())
            {
                return "Carrinho vazio";
            }
            
            var result = $"Carrinho de {Usuario.Nome}\n";
            double valorTotal = 0;
            foreach (var item in _itens)
            {
                valorTotal += item.ValorTotal;
                result += $"{item.Produto.Nome}, {item.Quantidade} unid. - {item.ValorTotal:C}\n";
            }
            result += $"\nValor Total: {valorTotal:C}";
            
            return result;
        }
    }
}
