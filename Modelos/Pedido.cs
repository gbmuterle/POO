using System;
using System.Collections.Generic;
using System.Linq;

namespace Modelos
{
    public class Pedido
    {
        public int Numero { get; set; }
        public Usuario Cliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string Situacao { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public Transportadora? Transportadora { get; set; }
        public double ValorFrete => Transportadora?.PrecoPorKm ?? 0;
        public double ValorTotal => Itens.Sum(i => i.ValorTotal) + ValorFrete;

        public Pedido(int numero, Usuario cliente, DateTime dataCriacao, DateTime? dataEntrega, string situacao, List<ItemPedido> itens, Transportadora? transportadora)
        {
            Numero = numero;
            Cliente = cliente;
            DataCriacao = dataCriacao;
            DataEntrega = dataEntrega;
            Situacao = situacao;
            Itens = itens;
            Transportadora = transportadora;
        }

        public override string ToString()
        {
            var dataEntregaStr = DataEntrega.HasValue ? DataEntrega.Value.ToString("dd/MM/yyyy") : "Não definida";
            var transportadoraStr = Transportadora != null ? Transportadora.ToString() : "Não definida";
            var pedidoItem = $"Número: {Numero}, Cliente: {Cliente.Nome}, Data de Criação: {DataCriacao:dd/MM/yyyy}, Data de Entrega: {dataEntregaStr}, Situação: {Situacao}, Transportadora: {transportadoraStr}, Valor Frete: {ValorFrete:C}, Valor Total: {ValorTotal:C}\n";
            foreach (var item in Itens)
            {
                pedidoItem += $"{item.Produto.Nome} - {item.Quantidade} un. - {item.ValorTotal:C}\n";
            }
            return pedidoItem;
        }
    }
}