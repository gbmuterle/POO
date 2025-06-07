using System;

namespace Modelos
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public Produto(int codigo, string nome, double valor, int quantidade, Fornecedor fornecedor)
        {
            Codigo = codigo;
            Nome = nome;
            Valor = valor;
            Quantidade = quantidade;
            Fornecedor = fornecedor;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Nome: {Nome}, Preço: {Valor:C}, Quantidade: {Quantidade}, Fornecedor: {Fornecedor.Codigo} - {Fornecedor.Nome.ToUpper()}";
        }
    }
}