using System;
using Modelos;

namespace Modelos
{
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal => Produto.Valor * Quantidade;

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"{Produto.Nome} - {Quantidade}x - Unitário: {Produto.Valor:C} - Total: {ValorTotal:C}";
        }
    }
}
