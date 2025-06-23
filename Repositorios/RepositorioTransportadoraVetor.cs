namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioTransportadoraVetor : IRepositorioTransportadora
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

        public void Alterar(Transportadora transportadoraAtual, Transportadora transportadoraAlterada)
        {
            for (int i = 0; i < transportadoras.Length; i++)
            {
                if (transportadoras[i].Codigo == transportadoraAlterada.Codigo)
                {
                    transportadoras[i] = transportadoraAlterada;
                    break;
                }
            }
        }

        public void Remover(Transportadora transportadora)
        {
            int index = -1;
            for (int i = 0; i < transportadoras.Length; i++)
            {
                if (transportadoras[i].Codigo == transportadora.Codigo)
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

        public Transportadora? BuscarPorCodigo(int codigo)
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