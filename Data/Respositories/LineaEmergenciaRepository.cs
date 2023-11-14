using Application.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

              try
              {
                  MessageLogger.LogInformationMessage($"FindAll...");

                  Query query = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                  var querySnapshot = query.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                  var list = new List<LineaEmergencia>();

                  foreach (var documentSnapshot in querySnapshot.Documents)
                  {
                    if (!documentSnapshot.Exists) continue;
                    var data = documentSnapshot.ConvertTo<FirestoreModels.LineaEmergencia>();
                    if (data == null) continue;
                    data.Id = documentSnapshot.Id;
                    list.Add(MapFirestoreModelToEntity(data));
                  }
                MessageLogger.LogInformationMessage($"Succes FindAll... ");
                return list;
              }
              catch (Exception ex)
              {
                  MessageLogger.LogErrorMessage(ex);
                  throw;
              }

        }

        public LineaEmergencia FindById(string id)
        {
            throw new Exception();
             try
             {
                 MessageLogger.LogInformationMessage($"FindById... {id}");

                 MessageLogger.LogInformationMessage($"Succes FindById... {id}");
             }
             catch (Exception ex)
             {
                 MessageLogger.LogErrorMessage(ex);
                 throw;
             }
        }

        public void Insert(LineaEmergencia entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var fbModel = MapEntityToFirestoremodel(entity);
                var colRef = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var doc = colRef.AddAsync(fbModel).ConfigureAwait(false).GetAwaiter().GetResult();

                MessageLogger.LogInformationMessage($"Succes Insert... {entity.NumeroEmergencia}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public LineaEmergencia Update(LineaEmergencia entity)
        {
            throw new Exception();
          /*  try
            {
                MessageLogger.LogInformationMessage($"Update... {entity.NumeroEmergencia}");

                MessageLogger.LogInformationMessage($"Succes Update... {entity.NumeroEmergencia}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }*/
        }

        private FirestoreModels.LineaEmergencia MapEntityToFirestoremodel(LineaEmergencia entity)
        {
            return new FirestoreModels.LineaEmergencia
            {
                Id = entity.Id,
                numeroEmergencia = entity.NumeroEmergencia,
                ubicacionEmergencia = entity.UbicacionEmergencia,
        };

    }
        private LineaEmergencia MapFirestoreModelToEntity(FirestoreModels.LineaEmergencia model)
    {
            return new LineaEmergencia(model.Id, model.numeroEmergencia, model.ubicacionEmergencia);
        }
    }
}

