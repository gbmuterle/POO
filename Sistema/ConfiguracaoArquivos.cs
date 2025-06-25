
using System;
using System.IO;

namespace Sistema.Configuracoes
{
    public static class ConfiguracaoArquivos
    {
        public static string DiretorioBase => @"C:\temp\Dados";
        public static string ArquivoFornecedores => Path.Combine(DiretorioBase, "fornecedores.json");
        public static string ArquivoProdutos => Path.Combine(DiretorioBase, "produtos.json");

        public static void InicializarDiretorios()
        {

            if (!Directory.Exists(DiretorioBase))
                Directory.CreateDirectory(DiretorioBase);
        }
    }
}