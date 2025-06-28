using System;

namespace Modelos
{
    public class Pedido
    {
        public int Numero { get; set; }
        public Usuario Cliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEntrega { get; set; }
        public double ValorTotal { get; set; }

        public Pedido(int numero, Usuario cliente, DateTime dataCriacao, DateTime dataEntrega, double valorTotal)
        {
            Numero =  numero;
            Cliente = cliente;
            DataCriacao = dataCriacao;
            ValorTotal = valorTotal;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Descrição: {Descricao}, Data: {DataCriacao:dd/MM/yyyy}, Valor Total: {ValorTotal:C}, Cliente: {Cliente.ToUpper()}";
        }
    }
}
