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

        public void Criar(Pedido pedido)
        {
            pedidos.Add(pedido);
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            pedidoAtual.DataCriacao = pedidoAlterado.DataCriacao;
            pedidoAtual.DataEntrega = pedidoAlterado.DataEntrega;
            pedidoAtual.Situacao = pedidoAlterado.Situacao;
            pedidoAtual.Transportadora = pedidoAlterado.Transportadora;
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public void Remover(Pedido pedido)
        {
            pedidos.Remove(pedido);
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }

        public Pedido? BuscarPorNumero(int numero)
        {
            return pedidos.FirstOrDefault(p => p.Numero == numero);
        }

        public List<Pedido> BuscarTodos()
        {
            return new List<Pedido>(pedidos);
        }

        public List<Pedido> BuscarPorCliente(Usuario cliente)
        {
            return pedidos
                .Where(p => p.Cliente != null && p.Cliente.Nome == cliente.Nome)
                .ToList();
        }
    }
}