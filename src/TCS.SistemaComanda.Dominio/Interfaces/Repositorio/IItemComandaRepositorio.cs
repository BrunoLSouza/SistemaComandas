using System;

namespace TCS.SistemaComanda.Dominio.Interfaces.Repositorio
{
    public interface IItemComandaRepositorio : IRepositorioBase<ItemComanda>
    {
        void Remover(Guid id);
    }
}
