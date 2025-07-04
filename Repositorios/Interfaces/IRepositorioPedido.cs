namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;
    using System;

    public interface IRepositorioPedido
    {
        void Criar(Pedido pedido);
        void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado);
        void Remover(Pedido pedido);
        Pedido? BuscarPorNumero(int numero);
        List<Pedido> BuscarTodos();
        List<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal);
    }
}