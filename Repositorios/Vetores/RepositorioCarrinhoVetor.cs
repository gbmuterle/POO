namespace Repositorios
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Modelos;

    public class RepositorioCarrinhoVetor : IRepositorioCarrinho
    {
        private Carrinho[] carrinhos = new Carrinho[0];
        private readonly IArmazenamento<Carrinho> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioCarrinhoVetor(IArmazenamento<Carrinho> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            carrinhos = _armazenamento.Carregar(_caminhoArquivo).ToArray();
        }

        public Carrinho ObterCarrinho(Usuario cliente)
        {
            var carrinho = carrinhos.FirstOrDefault(c => c.Cliente.Nome == cliente.Nome);

            if (carrinho == null)
            {
                carrinho = new Carrinho(cliente);

                var novos = new Carrinho[carrinhos.Length + 1];
                for (int i = 0; i < carrinhos.Length; i++)
                    novos[i] = carrinhos[i];
                novos[novos.Length - 1] = carrinho;
                carrinhos = novos;

                _armazenamento.Salvar(carrinhos, _caminhoArquivo);
            }
            return carrinho;
        }

        public void Adicionar(Carrinho carrinho, ItemPedido item)
        {
            carrinho.ItensInternos.Add(item);
            _armazenamento.Salvar(carrinhos, _caminhoArquivo);
        }

        public void Alterar(Carrinho carrinho, ItemPedido itemAtual, ItemPedido itemAlterado)
        {
            var index = carrinho.ItensInternos.IndexOf(itemAtual);
            carrinho.ItensInternos[index] = itemAlterado;
            _armazenamento.Salvar(carrinhos, _caminhoArquivo);
        }

        public void Remover(Carrinho carrinho, ItemPedido item)
        {
            carrinho.ItensInternos.Remove(item);
            _armazenamento.Salvar(carrinhos, _caminhoArquivo);
        }

        public void Limpar(Carrinho carrinho)
        {
            carrinho.ItensInternos.Clear();
            _armazenamento.Salvar(carrinhos, _caminhoArquivo);
        }

        public ItemPedido? BuscarItem(Carrinho carrinho, Produto produto)
        {
            return carrinho.ItensInternos.FirstOrDefault(i => i.Produto.Codigo == produto.Codigo);
        }

        public List<ItemPedido> BuscarTodos(Carrinho carrinho)
        {
            return new List<ItemPedido>(carrinho.ItensInternos);
        }
    }
}