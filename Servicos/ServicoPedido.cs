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

        public void Cadastrar(Pedido pedido)
        {
            if (pedido == null)
                throw new InvalidOperationException("Pedido não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(pedido.Descricao))
                throw new InvalidOperationException("Descrição do pedido não pode ser vazia.");

            if (_repositorio.BuscarPorCodigo(pedido.Codigo) != null)
                throw new InvalidOperationException("Já existe um pedido com esse código.");

            _repositorio.Adicionar(pedido);
        }

        public void Alterar(Pedido pedidoAtual, Pedido pedidoAlterado)
        {
            if (pedidoAlterado == null)
                throw new InvalidOperationException("Pedido alterado não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(pedidoAlterado.Descricao))
                throw new InvalidOperationException("Descrição do pedido não pode ser vazia.");

            _repositorio.Alterar(pedidoAtual, pedidoAlterado);
        }

        public void Remover(Pedido pedido)
        {
            if (pedido == null)
                throw new InvalidOperationException("Pedido não pode ser nulo.");

            _repositorio.Remover(pedido);
        }

        public Pedido? BuscarPorCodigo(int codigo)
        {
            return _repositorio.BuscarPorCodigo(codigo);
        }

        public List<Pedido> ListarTodos()
        {
            return _repositorio.BuscarTodos();
        }
    }
}
