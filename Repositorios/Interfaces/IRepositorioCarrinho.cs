using System;

namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public interface IRepositorioCarrinho
    {
        void Adicionar(Carrinho carrinho, ItemPedido item);
        void Alterar(Carrinho carrinho, ItemPedido itemAtual, ItemPedido itemAlterado);
        void Remover(Carrinho carrinho, ItemPedido item);
        void Limpar(Carrinho carrinho);
        Carrinho ObterCarrinho(Usuario cliente);
        ItemPedido? BuscarItem(Carrinho carrinho, Produto produto);
        List<ItemPedido> BuscarTodos(Carrinho carrinho);
    }
}