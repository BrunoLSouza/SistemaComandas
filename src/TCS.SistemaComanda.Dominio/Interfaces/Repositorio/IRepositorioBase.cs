using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Dominio.Interfaces
{
    public interface IRepositorioBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity Salvar(TEntity obj);
        TEntity ObterPorId(int id);
        IEnumerable<TEntity> ObterTodos();
        TEntity Alterar(TEntity obj);
        void Remover(int id);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
