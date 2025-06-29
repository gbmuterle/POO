namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioPedido
    {
        void Criar(Pedido pedido);
        void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado);
        void Remover(Pedido pedido);
        Pedido? BuscarPorNumero(int numero);
        List<Pedido> BuscarTodos();
        List<Pedido> BuscarPorCliente(Usuario cliente);
    }
}
