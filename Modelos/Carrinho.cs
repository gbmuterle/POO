using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Modelos
{
    public class Carrinho
    {
        public Usuario Cliente { get; private set; }
        public List<ItemPedido> Itens { get; private set; }
        public double ValorTotal => Itens.Sum(i => i.ValorTotal);

        public Carrinho(Usuario cliente, List<ItemPedido> itens)
        {
            Cliente = cliente;
            Itens = itens;
        }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
        }

        public void AlterarItem(ItemPedido itemAtual, ItemPedido itemAlterado)
        {
            itemAtual.Quantidade = itemAlterado.Quantidade;
        }

        public void RemoverItem(ItemPedido item)
        {
            Itens.Remove(item);
        }

        public void Limpar()
        {
            Itens.Clear();
        }

        public ItemPedido? BuscarItem(Produto produto)
        {
            return Itens.FirstOrDefault(i => i.Produto.Codigo == produto.Codigo);
        }

        public override string ToString()
        {
            if (!Itens.Any())
                return "Carrinho vazio";

            var result = $"Itens no carrinho:\n";
            foreach (var item in Itens)
                result += $"{item.Produto.Nome} - {item.Quantidade} un. - {item.ValorTotal:C}\n";
            result += $"\nValor Total: {ValorTotal:C}";
            return result;
        }
    }
}