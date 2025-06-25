using System;

namespace Repositorios
{
    public interface IArmazenamento<T>
    {
        void Salvar(List<T> items, string arquivo);
        List<T> Carregar(string arquivo);
    }
}
