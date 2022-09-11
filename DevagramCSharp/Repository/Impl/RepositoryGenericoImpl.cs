using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class RepositoryGenericoImpl<Entity> : IRepositoryGenerico<Entity>, IDisposable
        where Entity : class, new()
    {
        private readonly DevagramContext _DevagramContext;
        private readonly ILogger<RepositoryGenericoImpl<Entity>> _logger;
        public RepositoryGenericoImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Entity>> logger)
        {
            _DevagramContext = devagramContext;
            _logger = logger;
        }
        public bool Atualizar(Entity entity)
        {
            _DevagramContext.Set<Entity>().Update(entity);
            return SalvarAlteracoes();
        }

        public Entity BuscarPorID(int id)
        {
            try
            {
                return _DevagramContext.Set<Entity>().Find(id);
            }
            catch(Exception e)
            {
                _logger.LogError("Ocorreu um erro no repositorio: " + e.Message);
                return null;
            }
        }

        public List<Entity> BuscarTodos()
        {
            return _DevagramContext.Set<Entity>().ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _DevagramContext == null) return;

            _DevagramContext.Dispose();
        }

        public bool Excluir(Entity entity)
        {
            _DevagramContext.Remove(entity);
            return SalvarAlteracoes();
        }

        public bool Salvar(Entity entity)
        {
            _DevagramContext.Set<Entity>().Add(entity);
            return SalvarAlteracoes();
        }

        protected bool SalvarAlteracoes()
        {
            try
            {
                _DevagramContext.SaveChanges();
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
