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
                var itens = new List<ItemPedido>();
                carrinho = new Carrinho(cliente, itens);

                var novos = new Carrinho[carrinhos.Length + 1];
                for (int i = 0; i < carrinhos.Length; i++)
                    novos[i] = carrinhos[i];
                novos[novos.Length - 1] = carrinho;
                carrinhos = novos;

                _armazenamento.Salvar(carrinhos, _caminhoArquivo);
            }
            return carrinho;
        }

        public List<Carrinho> BuscarTodos()
        {
            return new List<Carrinho>(carrinhos);
        }

        public void Salvar()
        {
            _armazenamento.Salvar(carrinhos, _caminhoArquivo);
        }
    }
}