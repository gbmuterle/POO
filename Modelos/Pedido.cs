using System;

namespace Modelos
{
    public class Pedido
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public double ValorTotal { get; set; }
        public string Cliente { get; set; }

        public Pedido(int codigo, string descricao, DateTime dataCriacao, double valorTotal, string cliente)
        {
            Codigo = codigo;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            ValorTotal = valorTotal;
            Cliente = cliente;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Descrição: {Descricao}, Data: {DataCriacao:dd/MM/yyyy}, Valor Total: {ValorTotal:C}, Cliente: {Cliente.ToUpper()}";
        }
    }
}
