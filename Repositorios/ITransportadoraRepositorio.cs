namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface ITransportadoraRepositorio
    {
        void Adicionar(Transportadora transportadora);
        void Alterar(Transportadora transportadora);
        void Remover(int codigo);
        Transportadora BuscarPorCodigo(int codigo);
        List<Transportadora> BuscarTodos();
        List<Transportadora> BuscarPorNome(string nome);
    }
}