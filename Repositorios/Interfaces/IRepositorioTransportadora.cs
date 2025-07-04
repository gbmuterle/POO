namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioTransportadora
    {
        void Adicionar(Transportadora transportadora);
        void Alterar(Transportadora transportadoraAtual, Transportadora transportadoraAlterada);
        void Remover(Transportadora transportadora);
        Transportadora? BuscarPorCodigo(int codigo);
        List<Transportadora> BuscarTodos();
        List<Transportadora> BuscarPorNome(string nome);
    }
}