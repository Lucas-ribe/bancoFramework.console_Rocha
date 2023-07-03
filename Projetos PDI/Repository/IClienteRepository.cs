using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IClienteRepository
    {
        public Cliente Get(int id);
        public void Insert(Cliente cliente);
        public void Update(Cliente cliente);
        
    }
}
