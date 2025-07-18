namespace Servicos
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Modelos;
    using Repositorios;

    public class ServicoPedido
    {
        private readonly IRepositorioPedido _repositorio;
        private readonly ServicoProduto _servicoProduto;

        public ServicoPedido(IRepositorioPedido repositorio, ServicoProduto servicoProduto)
        {
            _repositorio = repositorio;
            _servicoProduto = servicoProduto;
        }

        public void Criar(Pedido pedido)
        {
            Validar(pedido, true);
            BaixarEstoque(pedido.Itens);
            _repositorio.Criar(pedido);
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            Validar(pedidoAlterado, false);
            _repositorio.Alterar(pedidoAtual, pedidoAlterado);
        }

        public void Remover(Pedido pedido)
        {
            if (pedido == null)
                throw new InvalidOperationException("Pedido inválido.");

            ReporEstoque(pedido.Itens);
            _repositorio.Remover(pedido);
        }

        public int GerarNumero()
        {
            var pedidos = _repositorio.BuscarTodos();
            if (pedidos == null || pedidos.Count == 0)
                return 1;
            return pedidos.Max(p => p.Numero) + 1;
        }

        public Pedido? BuscarPorNumero(int numero)
        {
            return _repositorio.BuscarPorNumero(numero);
        }

        public Pedido? BuscarPorNumero(int numero, Usuario cliente)
        {
            if (cliente == null)
                throw new InvalidOperationException("Usuário inválido.");

            var pedido = _repositorio.BuscarPorNumero(numero);

            if (pedido == null)
                return null;

            if (pedido.Cliente.Nome != cliente.Nome)
                throw new InvalidOperationException("O pedido pertence a outro cliente. Você não tem permissão para acessá-lo.");

            return pedido;
        }

        public List<Pedido> BuscarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public List<Pedido> BuscarTodos(Usuario cliente)
        {
            if (cliente == null)
                throw new InvalidOperationException("Usuário inválido.");

            var pedidos = _repositorio.BuscarTodos();
            return pedidos.Where(p => p.Cliente.Nome == cliente.Nome)
            .ToList();
        }

        public List<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial > dataFinal)
                throw new InvalidOperationException("Data inicial não pode ser maior que a data final.");

            return _repositorio.BuscarPorData(dataInicial, dataFinal);
        }

        public List<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal, Usuario cliente)
        {
            if (dataInicial > dataFinal)
                throw new InvalidOperationException("Data inicial não pode ser maior que a data final.");

            if (cliente == null)
                throw new InvalidOperationException("Usuário inválido.");

            var pedidos = _repositorio.BuscarTodos();
            return pedidos
                .Where(p => p.Cliente.Nome == cliente.Nome && p.DataCriacao >= dataInicial && p.DataCriacao <= dataFinal)
                .ToList();
        }

        private void Validar(Pedido pedido, bool novo)
        {
            if (pedido == null)
                throw new InvalidOperationException("Pedido inválido.");

            if (pedido.Cliente == null)
                throw new InvalidOperationException("Cliente inválido!.");

            if (pedido.Itens == null || pedido.Itens.Count == 0)
                throw new InvalidOperationException("Pedido deve conter pelo menos um item.");

            if (pedido.Transportadora == null)
                throw new InvalidOperationException("Transportadora inválida.");

            if (novo)
            {
                if (pedido.Situacao != null && pedido.Situacao != "novo")
                    throw new InvalidOperationException("Situação do pedido deve ser 'novo'.");

                if (BuscarPorNumero(pedido.Numero) != null)
                    throw new InvalidOperationException("Já existe um pedido com esse número.");
            }
            else
            {
                if (pedido.Situacao != "novo" && pedido.Situacao != "transporte" && pedido.Situacao != "entregue" && pedido.Situacao != "cancelado")
                    throw new InvalidOperationException("Situação inválida. Deve ser 'novo', 'transporte', 'entregue' ou 'cancelado'.");
                if (pedido.DataEntrega.HasValue && pedido.DataEntrega.Value < pedido.DataCriacao)
                    throw new InvalidOperationException("Data de entrega não pode ser anterior à data de criação do pedido.");
            }
        }

        public void BaixarEstoque(List<ItemPedido> itens)
        {
            _servicoProduto.BaixarEstoque(itens);
        }

        public void ReporEstoque(List<ItemPedido> itens)
        {
            _servicoProduto.ReporEstoque(itens);
        }
    }
}