using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace Repositorios
{
    public class RepositorioCarrinhoLista : IRepositorioCarrinho
    {
        private List<Carrinho> _carrinhos = new List<Carrinho>();
        private readonly IArmazenamento<Carrinho> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioCarrinhoLista(IArmazenamento<Carrinho> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            _carrinhos = _armazenamento.Carregar(_caminhoArquivo);
        }

        public void Adicionar(Carrinho carrinho, ItemPedido item)
        {
            carrinho.ItensInternos.Add(item);
            _armazenamento.Salvar(_carrinhos, _caminhoArquivo);
        }

        public void Alterar(Carrinho carrinho, ItemPedido itemAtual, ItemPedido itemAlterado)
        {
            itemAtual.Quantidade = itemAlterado.Quantidade;
            _armazenamento.Salvar(_carrinhos, _caminhoArquivo);
        }

        public void Remover(Carrinho carrinho, ItemPedido item)
        {
            carrinho.ItensInternos.Remove(item);
            _armazenamento.Salvar(_carrinhos, _caminhoArquivo);
        }

        public void Limpar(Carrinho carrinho)
        {
            carrinho.ItensInternos.Clear();
            _armazenamento.Salvar(_carrinhos, _caminhoArquivo);
        }

        public Carrinho ObterCarrinho(Usuario cliente)
        {
            var carrinho = _carrinhos.FirstOrDefault(c => c.Cliente.Nome == cliente.Nome);
            if (carrinho == null)
            {
                carrinho = new Carrinho(cliente);
                _carrinhos.Add(carrinho);
                _armazenamento.Salvar(_carrinhos, _caminhoArquivo);
            }
            return carrinho;
        }

        public ItemPedido? BuscarItem(Carrinho carrinho, Produto produto)
        {
            return carrinho.ItensInternos.FirstOrDefault(i => i.Produto.Codigo == produto.Codigo);
        }

        public List<ItemPedido> BuscarTodos(Carrinho carrinho)
        {
            return carrinho.ItensInternos.ToList();
        }
    }
}