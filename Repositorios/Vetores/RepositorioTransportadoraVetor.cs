namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioTransportadoraVetor : IRepositorioTransportadora
    {
        private Transportadora[] transportadoras = new Transportadora[0];
        private readonly IArmazenamento<Transportadora> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioTransportadoraVetor(IArmazenamento<Transportadora> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            transportadoras = _armazenamento.Carregar(_caminhoArquivo).ToArray();
        }

        public void Adicionar(Transportadora transportadora)
        {
            var novos = new Transportadora[transportadoras.Length + 1];
            for (int i = 0; i < transportadoras.Length; i++)
                novos[i] = transportadoras[i];
            novos[novos.Length - 1] = transportadora;
            transportadoras = novos;
            _armazenamento.Salvar(transportadoras, _caminhoArquivo);
        }

        public void Alterar(Transportadora transportadoraAtual, Transportadora transportadoraAlterada)
        {
            for (int i = 0; i < transportadoras.Length; i++)
            {
                if (transportadoras[i].Codigo == transportadoraAlterada.Codigo)
                {
                    transportadoras[i] = transportadoraAlterada;
                    _armazenamento.Salvar(transportadoras, _caminhoArquivo);
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
                _armazenamento.Salvar(transportadoras, _caminhoArquivo);
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
                lista.Add(t);
            }
            return lista;
        }

        public List<Transportadora> BuscarPorNome(string nome)
        {
            var lista = new List<Transportadora>();
            foreach (var t in transportadoras)
            {
                if (t.Nome.ToLower().Contains(nome.ToLower()))
                    lista.Add(t);
            }
            return lista;
        }
    }
}