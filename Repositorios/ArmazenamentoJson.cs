using System;
using System.Text.Json;

namespace Repositorios
{
    public class ArmazenamentoJson<T> : IArmazenamento<T>
    {
        public void Salvar(IEnumerable<T> items, string arquivo)
        {
            try
            {
                var diretorio = Path.GetDirectoryName(arquivo);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio!);

                var json = JsonSerializer.Serialize(items, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                File.WriteAllText(arquivo, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar arquivo {arquivo}: {ex.Message}");
            }
        }

        public List<T> Carregar(string arquivo)
        {
            try
            {
                if (!File.Exists(arquivo))
                    return new List<T>();

                var json = File.ReadAllText(arquivo);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar arquivo {arquivo}: {ex.Message}");
            }
        }
    }
}
