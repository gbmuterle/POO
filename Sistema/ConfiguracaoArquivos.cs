
using System;
using System.IO;

namespace Sistema.Configuracoes
{
    public static class ConfiguracaoArquivos
    {
        public static string DiretorioBase => @"C:\temp\Dados";
        public static string ArquivoFornecedores => Path.Combine(DiretorioBase, "fornecedores.json");

        public static string ArquivoTransportadoras => Path.Combine(DiretorioBase, "transportadoras.json");

        public static string ArquivoProdutos => Path.Combine(DiretorioBase, "produtos.json");

        public static string ArquivoPedidos => Path.Combine(DiretorioBase, "pedidos.json");

        public static string ArquivoUsuarios => Path.Combine(DiretorioBase, "usuarios.json");

        public static void InicializarDiretorios()
        {

            if (!Directory.Exists(DiretorioBase))
                Directory.CreateDirectory(DiretorioBase);
        }
    }
}