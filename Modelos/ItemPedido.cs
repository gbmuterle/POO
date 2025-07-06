using System;
using Modelos;

namespace Modelos
{
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario => Produto.Valor;
        public double ValorTotal => Produto.Valor * Quantidade;

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"Código: {Produto.Codigo}, Nome: {Produto.Nome}, Quantidade: {Quantidade}, Valor Unitário: {Produto.Valor:C}, Valor Total: {ValorTotal:C}";
        }
    }
}
