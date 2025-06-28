using System;

namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface ICarrinhoRepository
    {
        void Adicionar(Usuario usuario, ItemPedido item);
        void Alterar(Usuario usuario, ItemPedido item);
        void Remover(Usuario usuario, ItemPedido item);
        Carrinho? Listar(Usuario usuario);
    }
}