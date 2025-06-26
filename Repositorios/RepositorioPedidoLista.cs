namespace Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Modelos;

    public class RepositorioPedidoLista : IRepositorioPedido
    {
        private List<Pedido> pedidos = new List<Pedido>();
        private readonly IArmazenamento<Pedido> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioPedidoLista(IArmazenamento<Pedido> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            pedidos = _armazenamento.Carregar(_caminhoArquivo);
        }

        public void Adicionar(Pedido pedido)
        {
            pedidos.Add(pedido);
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            pedidoAtual.Descricao = pedidoAlterado.Descricao;
            pedidoAtual.ValorTotal = pedidoAlterado.ValorTotal;
            pedidoAtual.Cliente = pedidoAlterado.Cliente;
            pedidoAtual.DataCriacao = pedidoAlterado.DataCriacao;
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public void Remover(Pedido pedido)
        {
            pedidos.Remove(pedido);
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
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
