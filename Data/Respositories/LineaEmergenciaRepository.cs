using Application.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Respositories
{
    public class LineaEmergenciaRepository : IRepository<LineaEmergencia>
    {

        private const string COLLECTION_NAME = "lineaEmergencia";
        private readonly Connection _connection;

        public LineaEmergenciaRepository(Connection dbConnetion)
        {
            _connection = dbConnetion;
        }

        public void Delete(string id)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Deleting... {id}");

                MessageLogger.LogInformationMessage($"Succes delete... {id}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public List<LineaEmergencia> FindAll()
        {
            throw new NotImplementedException();
        }

        public LineaEmergencia FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(LineaEmergencia entity)
        {
            throw new NotImplementedException();
        }

        public LineaEmergencia Update(LineaEmergencia entity)
        {
            throw new NotImplementedException();
        }

        private FirestoreModels.LineaEmergencia MapEntityToFirestoremodel(LineaEmergencia entity)
        {
            return new FirestoreModels.LineaEmergencia
            {
                id = entity.Id,
                numeroEmergencia = entity.NumeroEmergencia,
                ubicacionEmergencia = entity.UbicacionEmergencia,
        };

    }
    private LineaEmergencia MapFirebaseModelToEntity(FirestoreModels.LineaEmergencia model)
    {
        return new LineaEmergencia(model.id, model.numeroEmergencia, model.ubicacionEmergencia);
    }
}
}

