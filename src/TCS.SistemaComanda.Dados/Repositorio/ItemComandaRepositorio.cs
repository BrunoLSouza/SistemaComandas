using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCS.SistemaComanda.Dominio;
using TCS.SistemaComanda.Dominio.Interfaces.Repositorio;

namespace TCS.SistemaComanda.Dados.Repositorio
{
    public class ItemComandaRepositorio : RepositorioBase<ItemComanda>, IItemComandaRepositorio
    {    

        public void Remover(Guid id)
        {
            ItemComanda item = Buscar(i => i.IdItemComanda.Equals(id)).FirstOrDefault();
            DbSet.Remove(item);
            SaveChanges();
        }

    }
}
