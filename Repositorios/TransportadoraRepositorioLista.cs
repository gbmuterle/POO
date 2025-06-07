namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class TransportadoraRepositorioLista : ITransportadoraRepositorio
    {
        private List<Transportadora> transportadoras = new List<Transportadora>();

        public void Adicionar(Transportadora transportadora)
        {
            transportadoras.Add(transportadora);
        }

        public void Alterar(Transportadora transportadora)
        {
            var existente = transportadoras.FirstOrDefault(t => t.Codigo == transportadora.Codigo);
            if (existente != null)
            {
                existente.Nome = transportadora.Nome;
                existente.Telefone = transportadora.Telefone;
                existente.Email = transportadora.Email;
                existente.Cnpj = transportadora.Cnpj;
                existente.PrecoPorKm = transportadora.PrecoPorKm;
            }
        }

        public void Remover(int codigo)
        {
            var existente = transportadoras.FirstOrDefault(t => t.Codigo == codigo);
            if (existente != null)
            {
                transportadoras.Remove(existente);
            }
        }

        public Transportadora BuscarPorCodigo(int codigo)
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