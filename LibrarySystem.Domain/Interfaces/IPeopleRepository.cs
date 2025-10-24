
namespace LibrarySystem.Domain.Interfaces
{
    public interface IPeopleRepository
    {
        void Add(People person);
        People GetById(int id);
        void Update(People people);
        IEnumerable<People> GetAll();
        
    }
}
