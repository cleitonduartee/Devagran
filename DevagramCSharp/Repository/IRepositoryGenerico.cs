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
    }
}
