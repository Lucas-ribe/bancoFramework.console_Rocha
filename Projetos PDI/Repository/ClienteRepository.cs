using Dapper;
using Domain.Model;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private IDbConnection _cliente;
        public ClienteRepository()
        {
            _cliente = new SqlConnection(@"Server=PE07ZKZB\SQLEXPRESS;Database=BancoFramework;Trusted_Connection=True;");
        }


        public Cliente Get(int id)
        {
            return _cliente.QuerySingleOrDefault<Cliente>("SELECT * FROM Cliente WHERE Id = @Id", new {Id = id});
        }

        public void Insert(Cliente cliente)
        {
            string sql = "INSERT INTO Cliente(Id, Nome, Cpf, Saldo) VALUES (@Id, @Nome, @Cpf, @Saldo)";
            _cliente.Execute(sql, cliente);
        }

        public void Update(Cliente cliente)
        {
            string sql = "UPDATE Cliente SET Id = @Id, Nome = @Nome, Cpf = @CPF, Saldo = @Saldo WHERE Id = @Id";
            _cliente.Execute(sql, cliente);
        }
    }
}
