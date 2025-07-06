namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioTransportadoraLista : IRepositorioTransportadora
    {
        private List<Transportadora> transportadoras = new List<Transportadora>();

        private readonly IArmazenamento<Transportadora> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioTransportadoraLista(IArmazenamento<Transportadora> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            transportadoras = _armazenamento.Carregar(_caminhoArquivo);
        }

        public void Adicionar(Transportadora transportadora)
        {
            transportadoras.Add(transportadora);
            Salvar();
        }

        public void Alterar(Transportadora transportadoraAtual, Transportadora transportadoraAlterada)
        {
            transportadoraAtual.Nome = transportadoraAlterada.Nome;
            transportadoraAtual.Telefone = transportadoraAlterada.Telefone;
            transportadoraAtual.Email = transportadoraAlterada.Email;
            transportadoraAtual.Cnpj = transportadoraAlterada.Cnpj;
            transportadoraAtual.PrecoPorKm = transportadoraAlterada.PrecoPorKm;
            Salvar();
        }

        public void Remover(Transportadora transportadora)
        {
            transportadoras.Remove(transportadora);
            Salvar();
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

        public void Salvar()
        {
            _armazenamento.Salvar(transportadoras, _caminhoArquivo);
        }
    }
}