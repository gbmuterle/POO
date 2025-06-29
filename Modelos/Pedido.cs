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
        public DateTime DataEntrega { get; set; }
        public string Situacao { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public Transportadora Transportadora { get; set; }
        public double ValorFrete => Transportadora.PrecoPorKm;
        public double ValorTotal => Itens.Sum(i => i.ValorTotal) + ValorFrete;

        public Pedido(int numero, Usuario cliente, DateTime dataCriacao, DateTime dataEntrega, string situacao, List<ItemPedido> itens, Transportadora transportadora)
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
            return $"Número: {Numero}, Cliente: {Cliente.Nome}, Data de Criação: {DataCriacao:dd/MM/yyyy}, Data de Entrega: {DataEntrega:dd/MM/yyyy}, Situação: {Situacao}, Transportadora: {Transportadora}, Valor Frete: {ValorFrete:C}, Valor Total: {ValorTotal:C}";
        }
    }
}