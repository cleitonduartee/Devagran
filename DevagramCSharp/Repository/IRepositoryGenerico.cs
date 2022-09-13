using DevagramCSharp.Models;
using System.Linq.Expressions;

namespace DevagramCSharp.Repository
{
    public interface IRepositoryGenerico <Entity> where Entity : class
    {
        public bool Salvar(Entity entity);
        public bool Atualizar(Entity entity);
        public bool Excluir(Entity entity);
        public  Entity BuscarPorID(int id);
        public List<Entity> BuscarTodos();
        public Entity BuscarSomente(Expression<Func<Entity, bool>> expression);     
        public List<Entity> BuscarTodosPor(Expression<Func<Entity, bool>> expression);     
    }
}
