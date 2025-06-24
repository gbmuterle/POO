namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioPedidoLista : IRepositorioPedido
    {
        private List<Pedido> pedidos = new List<Pedido>();

        public void Adicionar(Pedido pedido)
        {
            pedidos.Add(pedido);
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            pedidoAtual.Descricao = pedidoAlterado.Descricao;
            pedidoAtual.ValorTotal = pedidoAlterado.ValorTotal;
            pedidoAtual.Cliente = pedidoAlterado.Cliente;
            pedidoAtual.DataCriacao = pedidoAlterado.DataCriacao;
        }

        public void Remover(Pedido pedido)
        {
            pedidos.Remove(pedido);
        }

        public Pedido? BuscarPorCodigo(int codigo)
        {
            return pedidos.FirstOrDefault(p => p.Codigo == codigo);
        }

        public List<Pedido> BuscarTodos()
        {
            return new List<Pedido>(pedidos);
        }

        public List<Pedido> BuscarPorCliente(string cliente)
        {
            return pedidos
                .Where(p => p.Cliente.ToLower().Contains(cliente.ToLower()))
                .ToList();
        }
    }
}
