namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioPedidoVetor : IRepositorioPedido
    {
        private Pedido[] pedidos = new Pedido[0];
        private readonly IArmazenamento<Pedido> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioPedidoVetor(IArmazenamento<Pedido> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            pedidos = _armazenamento.Carregar(_caminhoArquivo).ToArray();
        }

        public void Adicionar(Pedido pedido)
        {
            var novos = new Pedido[pedidos.Length + 1];
            for (int i = 0; i < pedidos.Length; i++)
                novos[i] = pedidos[i];
            novos[novos.Length - 1] = pedido;
            pedidos = novos;
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i].Numero == pedidoAtual.Numero)
                {
                    pedidos[i] = pedidoAlterado;
                    break;
                }
            }
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public void Remover(Pedido pedido)
        {
            int index = -1;
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i].Numero == pedido.Numero)
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
                _armazenamento.Salvar(pedidos, _caminhoArquivo);
            }
        }

        public Pedido? BuscarPorNumero(int numero)
        {
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i].Numero == numero)
                    return pedidos[i];
            }
            return null;
        }

        public List<Pedido> BuscarTodos()
        {
            return pedidos.ToList();
        }

        public List<Pedido> BuscarPorCliente(Usuario cliente)
        {
            var lista = new List<Pedido>();
            foreach (var p in pedidos)
            {
                if (p.Cliente.Nome == cliente.Nome)
                    lista.Add(p);
            }
            return lista;
        }
    }
}