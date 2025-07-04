using System;
using System.IO;

namespace Sistema
{
    public class ConfiguracaoArquivos
    {
        public string TipoRepositorio { get; }
        public string DiretorioBase => @"C:\SistemaLoja\Dados";
        public string DiretorioTipo => Path.Combine(DiretorioBase, TipoRepositorio);

        public string ArquivoFornecedores => Path.Combine(DiretorioTipo, "fornecedores.json");
        public string ArquivoTransportadoras => Path.Combine(DiretorioTipo, "transportadoras.json");
        public string ArquivoProdutos => Path.Combine(DiretorioTipo, "produtos.json");
        public string ArquivoPedidos => Path.Combine(DiretorioTipo, "pedidos.json");
        public string ArquivoUsuarios => Path.Combine(DiretorioTipo, "usuarios.json");
        public string ArquivoCarrinhos => Path.Combine(DiretorioTipo, "carrinhos.json");

        public ConfiguracaoArquivos(string tipoRepositorio)
        {
            TipoRepositorio = tipoRepositorio == "1" ? "Vetor" : "Lista";
        }

        public void InicializarDiretorios()
        {
            if (!Directory.Exists(DiretorioTipo))
                Directory.CreateDirectory(DiretorioTipo);
        }
    }
}