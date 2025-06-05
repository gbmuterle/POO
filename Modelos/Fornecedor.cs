namespace Modelos.Classes
{
    public class Fornecedor : Pessoa
    {
        public string Cnpj { get; set; }
        public string Descricao { get; set; }
        public Endereco Endereco { get; set; }

        public Fornecedor(int codigo, string nome, string descricao, string telefone, string email, Endereco endereco, string cnpj)
            : base(codigo, nome, telefone, email)
        {
            Cnpj = cnpj;
            Descricao = descricao;
            Endereco = endereco;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Nome: {Nome}, Descrição: {Descricao}, Telefone: {Telefone}, Email: {Email}, CNPJ: {Cnpj}\nEndereço: {Endereco}";
        }
    }
}