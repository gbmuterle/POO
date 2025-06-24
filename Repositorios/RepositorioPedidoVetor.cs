namespace Repositorios
{
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioPedidoVetor : IRepositorioPedido
    {
        private Pedido[] pedidos = new Pedido[0];

        public void Adicionar(Pedido pedido)
        {
            var novos = new Pedido[pedidos.Length + 1];
            for (int i = 0; i < pedidos.Length; i++)
                novos[i] = pedidos[i];
            novos[novos.Length - 1] = pedido;
            pedidos = novos;
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i].Codigo == pedidoAlterado.Codigo)
                {
                    pedidos[i] = pedidoAlterado;
                    break;
                }
            }
        }

        public void Remover(Pedido pedido)
        {
            int index = -1;
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i].Codigo == pedido.Codigo)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var novos = new Pedido[pedidos.Length - 1];
                int j = 0;
                for (int i = 0; i < pedidos.Length; i++)
                {
                    if (i != index)
                        novos[j++] = pedidos[i];
                }
                pedidos = novos;
            }
        }

        public Pedido? BuscarPorCodigo(int codigo)
        {
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i].Codigo == codigo)
                    return pedidos[i];
            }
            return null;
        }

        public List<Pedido> BuscarTodos()
        {
            var lista = new List<Pedido>();
            foreach (var p in pedidos)
            {
                if (p != null) lista.Add(p);
            }
            return lista;
        }

        public List<Pedido> BuscarPorCliente(string cliente)
        {
            var lista = new List<Pedido>();
            foreach (var p in pedidos)
            {
                if (p != null && p.Cliente != null && p.Cliente.ToLower().Contains(cliente.ToLower()))
                    lista.Add(p);
            }
            return lista;
        }
    }
}
