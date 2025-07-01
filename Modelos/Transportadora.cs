namespace Modelos
{
    public class Transportadora : Pessoa
    {
        public string Cnpj { get; set; }
        public double PrecoPorKm { get; set; }

        public Transportadora(int codigo, string nome, string telefone, string email, string cnpj, double precoPorKm)
            : base(codigo, nome, telefone, email)
        {
            Cnpj = cnpj;
            PrecoPorKm = precoPorKm;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Nome: {Nome}, Telefone: {Telefone}, Email: {Email}, CNPJ: {Cnpj}, Preço por Km: {PrecoPorKm:C}";
        }

        public string InfoTransportadora()
        {
            return $"{Codigo} - {Nome} - Preço por km: {PrecoPorKm:C}";
        }
    }
}