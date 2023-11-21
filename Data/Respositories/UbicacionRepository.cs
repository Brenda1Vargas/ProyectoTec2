using Application.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Respositories
{
    public class UbicacionRepository : IRepository<Ubicacion>
    {
        private const string COLLECTION_NAME = "Ubicacion";
        private readonly Connection _connection;

        public UbicacionRepository(Connection dbConnetion)
        {
            _connection = dbConnetion;
        }
        public void Delete(string id)
        {
            try
            {
                MessageLogger.LogWarningMessage($"Deleting... {id}");

                var recordRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(id);
                recordRef.DeleteAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                MessageLogger.LogInformationMessage($"Succes delete... {id}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public List<Ubicacion> FindAll()
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindAll...");

                Query query = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var querySnapshot = query.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var list = new List<Ubicacion>();

                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    if (!documentSnapshot.Exists) continue;
                    var data = documentSnapshot.ConvertTo<FirestoreModels.Ubicacion>();
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

        public Ubicacion FindById(string id)
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindById... {id}");

                var docRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(id);
                var snapshot = docRef.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (snapshot.Exists)
                {
                    var ubicacionModel = snapshot.ConvertTo<FirestoreModels.Ubicacion>();
                    ubicacionModel.Id = snapshot.Id;
                    MessageLogger.LogInformationMessage($"Succes FindById... {id}");
                    return MapFirestoreModelToEntity(ubicacionModel);
                }
                else
                {
                    MessageLogger.LogWarningMessage("Colletion lineaEmergencia doesn't exist");
                    return null;

                }
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }
        public void Insert(Ubicacion entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var fbModel = MapEntityToFirestoremodel(entity);
                var colRef = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var doc = colRef.AddAsync(fbModel).ConfigureAwait(false).GetAwaiter().GetResult();

                MessageLogger.LogInformationMessage($"Succes Insert... {entity.Nombre}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }
        public Ubicacion Update(Ubicacion entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var recordRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(entity.Id);
                var fbModel = MapEntityToFirestoremodel(entity);
                recordRef.SetAsync(fbModel, SetOptions.MergeAll).ConfigureAwait(false).GetAwaiter().GetResult();
                MessageLogger.LogInformationMessage($"Success Insert... {entity.Nombre}");

                return entity;
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        private FirestoreModels.Ubicacion MapEntityToFirestoremodel(Ubicacion entity)
        {
            return new FirestoreModels.Ubicacion
            {
                Id = entity.Id,
                Longitud = entity.Longitud,
                Nombre = entity.Nombre,
                Latitud = entity.Latitud,
            };

        }
        private Ubicacion MapFirestoreModelToEntity(FirestoreModels.Ubicacion model)
        {
            return new Ubicacion(model.Id, model.Longitud, model.Nombre, model.Latitud);
        }
    }
}