using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace Repositorios
{
    public class RepositorioCarrinhoLista : IRepositorioCarrinho
    {
        private List<Carrinho> carrinhos;
        private readonly IArmazenamento<Carrinho> _armazenamento;
        private readonly string _caminhoArquivo;

        public RepositorioCarrinhoLista(IArmazenamento<Carrinho> armazenamento, string caminhoArquivo)
        {
            _armazenamento = armazenamento;
            _caminhoArquivo = caminhoArquivo;
            carrinhos = _armazenamento.Carregar(_caminhoArquivo);
        }

        public Carrinho ObterCarrinho(Usuario cliente)
        {
            var carrinho = carrinhos.FirstOrDefault(c => c.Cliente.Nome == cliente.Nome);
            if (carrinho == null)
            {
                carrinho = new Carrinho(cliente);
                carrinhos.Add(carrinho);
                Salvar();
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