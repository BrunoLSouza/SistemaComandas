using System;

namespace TCS.SistemaComanda.Dominio.Interfaces.Repositorio
{
    public interface IItemComandaRepositorio : IRepositorioBase<ItemComanda>
    {
        ItemComanda ObterPorId(Guid id);
    }
}
