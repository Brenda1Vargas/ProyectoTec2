using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Respositories
{
    public class MenorRepository : IRepository<Menor>
    {
        private const string COLLECTION_NAME = "menor";
        private readonly Connection _connection;

        public MenorRepository(Connection dbConnetion)
        {
            _connection = dbConnetion;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Menor> FindAll()
        {
            throw new NotImplementedException();
        }

        public Menor FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Menor entity)
        {
            throw new NotImplementedException();
        }

        public Menor Update(Menor entity)
        {
            throw new NotImplementedException();
        }
    }
}
