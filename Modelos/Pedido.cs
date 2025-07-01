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
        public DateTime? DataCancelamento { get; set; }
        public string Situacao { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public Transportadora Transportadora { get; set; }
        public double Distancia { get; set; }
        public double ValorFrete => Transportadora.PrecoPorKm * Distancia;
        public double ValorTotal => Itens.Sum(i => i.ValorTotal) + ValorFrete;

        public Pedido(
            int numero,
            Usuario cliente,
            DateTime dataCriacao,
            DateTime? dataEntrega,  
            DateTime? dataCancelamento,
            string situacao,
            List<ItemPedido> itens,
            Transportadora transportadora,
            double distancia
        )
        {
            Numero = numero;
            Cliente = cliente;
            DataCriacao = dataCriacao;
            DataEntrega = dataEntrega;
            DataCancelamento = dataCancelamento;
            Situacao = situacao;
            Itens = itens;
            Transportadora = transportadora;
            Distancia = distancia;
        }

        public override string ToString()
        {
            var dataEntregaStr = DataEntrega.HasValue ? DataEntrega.Value.ToString("dd/MM/yyyy") : "Não definida";
            var pedidoItem = $"Número: {Numero}, Cliente: {Cliente.Nome}, Data de Criação: {DataCriacao:dd/MM/yyyy}, Data de Entrega: {dataEntregaStr}, Situação: {Situacao}\nTransportadora: {Transportadora}, Distância: {Distancia}, Valor Frete: {ValorFrete:C}, Valor Total: {ValorTotal:C}\n";
            foreach (var item in Itens)
            {
                pedidoItem += $"    {item.Produto.Nome} - {item.Quantidade} un. - {item.ValorTotal:C}\n";
            }
            if (Situacao.Equals("cancelado", StringComparison.OrdinalIgnoreCase) && DataCancelamento.HasValue)
            {
                pedidoItem += $"--- PEDIDO CANCELADO EM {DataCancelamento.Value:dd/MM/yyyy} ---\n";
            }
            return pedidoItem;
        }
    }
}