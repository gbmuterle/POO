namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioPedido
    {
        void Adicionar(Pedido pedido);
        void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado);
        void Remover(Pedido pedido);
        Pedido? BuscarPorCodigo(int codigo);
        List<Pedido> BuscarTodos();
        List<Pedido> BuscarPorCliente(string cliente);
    }
}
