namespace Servicos
{
    using System;
    using System.Collections.Generic;
    using Modelos;
    using Repositorios;

    public class ServicoPedido
    {
        private readonly IRepositorioPedido _repositorio;

        public ServicoPedido(IRepositorioPedido repositorio)
        {
            _repositorio = repositorio;
        }

        public void Criar(Pedido pedido)
        {
            Validar(pedido, true);
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
                throw new InvalidOperationException("Pedido não pode ser nulo.");

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

        public List<Pedido> BuscarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public List<Pedido> BuscarPorCliente(Usuario cliente)
        {
            if (cliente == null)
                throw new InvalidOperationException("Cliente não pode ser nulo.");

            return _repositorio.BuscarPorCliente(cliente);
        }

        private void Validar(Pedido pedido, bool novo)
        {
            if (pedido == null)
                throw new InvalidOperationException("Pedido não pode ser nulo.");

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
                    throw new InvalidOperationException("Situação do pedido inválida.");
            }
        }
    }
}