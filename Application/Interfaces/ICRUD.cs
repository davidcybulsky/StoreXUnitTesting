using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICRUD<T> where T : BaseEntity
    {
        void Create(T entity);
        T Read(Guid id);
        void Update(Guid id, T entity);
        void Delete(Guid id);
    }
}
