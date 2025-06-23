namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioTransportadoraLista : IRepositorioTransportadora
    {
        private List<Transportadora> transportadoras = new List<Transportadora>();

        public void Adicionar(Transportadora transportadora)
        {
            transportadoras.Add(transportadora);
        }

        public void Alterar(Transportadora transportadoraAtual, Transportadora transportadoraAlterada)
        {
                transportadoraAtual.Nome = transportadoraAlterada.Nome;
                transportadoraAtual.Telefone = transportadoraAlterada.Telefone;
                transportadoraAtual.Email = transportadoraAlterada.Email;
                transportadoraAtual.Cnpj = transportadoraAlterada.Cnpj;
                transportadoraAtual.PrecoPorKm = transportadoraAlterada.PrecoPorKm;
        }

        public void Remover(Transportadora transportadora)
        {
            transportadoras.Remove(transportadora);
        }

        public Transportadora? BuscarPorCodigo(int codigo)
        {
            return transportadoras.FirstOrDefault(t => t.Codigo == codigo);
        }

        public List<Transportadora> BuscarTodos()
        {
            return new List<Transportadora>(transportadoras);
        }

        public List<Transportadora> BuscarPorNome(string nome)
        {
            return transportadoras
                .Where(t => t.Nome.ToLower().Contains(nome.ToLower()))
                .ToList();
        }
    }
}