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
            Salvar();
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            pedidoAtual.DataCriacao = pedidoAlterado.DataCriacao;
            pedidoAtual.DataEntrega = pedidoAlterado.DataEntrega;
            pedidoAtual.DataCancelamento = pedidoAlterado.DataCancelamento;
            pedidoAtual.Situacao = pedidoAlterado.Situacao;
            pedidoAtual.Transportadora = pedidoAlterado.Transportadora;
            Salvar();
        }

        public void Remover(Pedido pedido)
        {
            pedidos.Remove(pedido);
            Salvar();
        }

        public Pedido? BuscarPorNumero(int numero)
        {
            return pedidos.FirstOrDefault(p => p.Numero == numero);
        }

        public List<Pedido> BuscarTodos()
        {
            return new List<Pedido>(pedidos);
        }

        public List<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal)
        {
            return pedidos
                .Where(p => p.DataCriacao >= dataInicial.Date && p.DataCriacao <= dataFinal.Date)
                .ToList();
        }

        public void Salvar()
        {
            _armazenamento.Salvar(pedidos, _caminhoArquivo);
        }
    }
}