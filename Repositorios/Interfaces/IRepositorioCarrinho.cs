using System.Collections.Generic;
using Modelos;

namespace Repositorios
{
    public interface IRepositorioCarrinho
    {
        Carrinho ObterCarrinho(Usuario cliente);
        List<Carrinho> BuscarTodos();
        void Salvar();
    }
}