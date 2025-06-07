namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class TransportadoraRepositorioVetor : ITransportadoraRepositorio
    {
        private Transportadora[] transportadoras = new Transportadora[0];

        public void Adicionar(Transportadora transportadora)
        {
            var novos = new Transportadora[transportadoras.Length + 1];
            for (int i = 0; i < transportadoras.Length; i++)
                novos[i] = transportadoras[i];
            novos[novos.Length - 1] = transportadora;
            transportadoras = novos;
        }

        public void Alterar(Transportadora transportadora)
        {
            for (int i = 0; i < transportadoras.Length; i++)
            {
                if (transportadoras[i].Codigo == transportadora.Codigo)
                {
                    transportadoras[i] = transportadora;
                    break;
                }
            }
        }

        public void Remover(int codigo)
        {
            int index = -1;
            for (int i = 0; i < transportadoras.Length; i++)
            {
                if (transportadoras[i].Codigo == codigo)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var novos = new Transportadora[transportadoras.Length - 1];
                int j = 0;
                for (int i = 0; i < transportadoras.Length; i++)
                {
                    if (i != index)
                        novos[j++] = transportadoras[i];
                }
                transportadoras = novos;
            }
        }

        public Transportadora BuscarPorCodigo(int codigo)
        {
            for (int i = 0; i < transportadoras.Length; i++)
            {
                if (transportadoras[i].Codigo == codigo)
                    return transportadoras[i];
            }
            return null;
        }

        public List<Transportadora> BuscarTodos()
        {
            var lista = new List<Transportadora>();
            foreach (var t in transportadoras)
            {
                if (t != null) lista.Add(t);
            }
            return lista;
        }

        public List<Transportadora> BuscarPorNome(string nome)
        {
            var lista = new List<Transportadora>();
            foreach (var t in transportadoras)
            {
                if (t != null && t.Nome != null && t.Nome.ToLower().Contains(nome.ToLower()))
                    lista.Add(t);
            }
            return lista;
        }
    }
}