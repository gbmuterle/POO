using System;
using Modelos;
using Repositorios;

namespace Servicos
{
    public class ServicoCarrinho
    {
        private readonly IRepositorioCarrinho _repositorio;
        private readonly ServicoPedido _servicoPedido;

        public ServicoCarrinho(IRepositorioCarrinho repositorio, ServicoPedido servicoPedido)
        {
            _repositorio = repositorio;
            _servicoPedido = servicoPedido;
        }

        public void AdicionarItem(Carrinho carrinho, ItemPedido item)
        {
            Validar(carrinho, item, true);
            _repositorio.Adicionar(carrinho, item);
        }

        public void AlterarItem(Carrinho carrinho, ItemPedido itemAtual, ItemPedido itemAlterado)
        {
            Validar(carrinho, itemAlterado, false);
            _repositorio.Alterar(carrinho, itemAtual, itemAlterado);
        }

        public void RemoverItem(Carrinho carrinho, ItemPedido item)
        {
            if (item == null)
                throw new InvalidOperationException("Item não pode ser nulo.");

            _repositorio.Remover(carrinho, item);
        }
        
        public void Finalizar(Carrinho carrinho)
        {
            if (carrinho == null)
                throw new InvalidOperationException("Carrinho inválido.");
            if (!carrinho.ItensInternos.Any())
                throw new InvalidOperationException("Carrinho vazio. Não é possível finalizar a compra.");

            int numero = _servicoPedido.GerarNumero();
            var pedido = new Pedido(
                numero,
                carrinho.Cliente,
                DateTime.Now,
                null,
                "novo",
                carrinho.ItensInternos,
                null
            );
            _servicoPedido.Criar(pedido);
            Limpar(carrinho);
        }

        public void Limpar(Carrinho carrinho)
        {
            _repositorio.Limpar(carrinho);
        }

        public Carrinho ObterCarrinho(Usuario cliente)
        {
            return _repositorio.ObterCarrinho(cliente);
        }

        public ItemPedido? BuscarItem(Carrinho carrinho, Produto produto)
        {
            return _repositorio.BuscarItem(carrinho, produto);
        }

        public List<ItemPedido> ListarTodos(Carrinho carrinho)
        {
            return _repositorio.ListarTodos(carrinho);
        }

        private void Validar(Carrinho carrinho, ItemPedido item, bool novo)
        {
            if (carrinho == null)
                throw new InvalidOperationException("Carrinho inválido.");

            if (item == null)
                throw new InvalidOperationException("Item inválido.");

            if (item.Produto == null)
                throw new InvalidOperationException("Produto do item não pode ser nulo.");

            if (item.Quantidade <= 0)
                throw new InvalidOperationException("Quantidade deve ser maior que zero.");

            if (item.Produto.Quantidade == 0)
                throw new InvalidOperationException("Produto não está disponível no estoque.");

            if (item.Quantidade > item.Produto.Quantidade)
                    throw new InvalidOperationException($"Quantidade solicitada maior que o estoque disponível.\nO máximo disponível é {item.Produto.Quantidade}.");

            if (novo)
            {
                if (BuscarItem(carrinho, item.Produto) != null)
                    throw new InvalidOperationException("Esse item já está no carrinho.");
            }
            else
            {
                if (BuscarItem(carrinho, item.Produto) == null)
                    throw new InvalidOperationException("Item não encontrado no carrinho.");
            }
        }
    }
}