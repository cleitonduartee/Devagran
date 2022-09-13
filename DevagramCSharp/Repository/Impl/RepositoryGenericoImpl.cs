using DevagramCSharp.Models;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DevagramCSharp.Repository.Impl
{
    public class RepositoryGenericoImpl<Entity> : IRepositoryGenerico<Entity>, IDisposable
        where Entity : class, new()
    {
        protected readonly DevagramContext _contexto;
        private readonly ILogger<RepositoryGenericoImpl<Entity>> _logger;
        public RepositoryGenericoImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Entity>> logger)
        {
            _contexto = devagramContext;
            _logger = logger;
        }
        public bool Atualizar(Entity entity)
        {
            _contexto.Set<Entity>().Update(entity);
            return SalvarAlteracoes();
        }

        public Entity BuscarPorID(int id)
        {
            try
            {
                return _contexto.Set<Entity>().Find(id);
            }
            catch(Exception e)
            {
                _logger.LogError("Ocorreu um erro no repositorio: " + e.Message);
                return null;
            }
        }

        public List<Entity> BuscarTodos()
        {
            return _contexto.Set<Entity>().ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _contexto == null) return;

            _contexto.Dispose();
        }

        public bool Excluir(Entity entity)
        {
            _contexto.Remove(entity);
            return SalvarAlteracoes();
        }

        public bool Salvar(Entity entity)
        {
            _contexto.Set<Entity>().Add(entity);
            return SalvarAlteracoes();
        }
        public Entity BuscarSomente(Expression<Func<Entity, bool>> expression)
        {
            return _contexto.Set<Entity>().Where(expression).FirstOrDefault();
        }
        public List<Entity> BuscarTodosPor(Expression<Func<Entity, bool>> expression)
        {
            return _contexto.Set<Entity>().Where(expression).AsNoTracking().ToList();
        }
        protected bool SalvarAlteracoes()
        {
            try
            {
                _contexto.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("Ocorreu um erro no repositorio: " + e.Message);
                return false;
            }
        }
    }
}
