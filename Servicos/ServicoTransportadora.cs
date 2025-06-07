namespace Servicos
{
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoTransportadora
    {
        private readonly ITransportadoraRepositorio _repositorio;

        public ServicoTransportadora(ITransportadoraRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(Transportadora transportadora)
        {
            _repositorio.Adicionar(transportadora);
        }

        public void Alterar(Transportadora transportadora)
        {
            _repositorio.Alterar(transportadora);
        }

        public void Remover(int codigo)
        {
            _repositorio.Remover(codigo);
        }

        public Transportadora BuscarPorCodigo(int codigo)
        {
            return _repositorio.BuscarPorCodigo(codigo);
        }

        public List<Transportadora> ListarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public List<Transportadora> BuscarPorNome(string nome)
        {
            return _repositorio.BuscarPorNome(nome);
        }
    }
}