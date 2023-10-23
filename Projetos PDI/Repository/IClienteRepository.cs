using Domain.Model;

namespace Repository
{
    public interface IClienteRepository
    {
        public Cliente Get(int id);
        public void Insert(Cliente cliente);
        public void Update(Cliente cliente);
        
    }
}
