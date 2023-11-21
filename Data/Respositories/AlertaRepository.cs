using Application.Data.FirestoreModels;
using Application.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace Application.Data.Respositories
{
    public class AlertaRepository : IRepository<Alerta>
    {
        private const string COLLECTION_NAME = "alerta";
        private readonly Connection _connection;

        public AlertaRepository(Connection dbConnetion)
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

        public List<Alerta> FindAll()
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindAll...");

                Query query = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var querySnapshot = query.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var list = new List<Alerta>();

                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    if (!documentSnapshot.Exists) continue;
                    var data = documentSnapshot.ConvertTo<FirestoreModels.Alerta>();
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

        public Alerta FindById(string id)
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindById... {id}");

                var docRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(id);
                var snapshot = docRef.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (snapshot.Exists)
                {
                    var alertaModel = snapshot.ConvertTo<FirestoreModels.Alerta>();
                    alertaModel.Id = snapshot.Id;
                    MessageLogger.LogInformationMessage($"Succes FindById... {id}");
                    return MapFirestoreModelToEntity(alertaModel);
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

        public void Insert(Alerta entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Numero}");

                var fbModel = MapEntityToFirestoremodel(entity);
                var colRef = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var doc = colRef.AddAsync(fbModel).ConfigureAwait(false).GetAwaiter().GetResult();

                MessageLogger.LogInformationMessage($"Succes Insert... {entity.Numero}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public Alerta Update(Alerta entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Numero}");

                var recordRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(entity.Id);
                var fbModel = MapEntityToFirestoremodel(entity);
                recordRef.SetAsync(fbModel, SetOptions.MergeAll).ConfigureAwait(false).GetAwaiter().GetResult();
                MessageLogger.LogInformationMessage($"Success Insert... {entity.Numero}");

                return entity;
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }
        private FirestoreModels.Alerta MapEntityToFirestoremodel(Alerta entity)
        {
            return new FirestoreModels.Alerta
            {
                Id = entity.Id,
                Ubicacion = entity.Ubicacion,
                Fecha = new DateTime(entity.Fecha.Year, entity.Fecha.Month, entity.Fecha.Day, entity.Fecha.Hour, entity.Fecha.Minute, entity.Fecha.Second, DateTimeKind.Utc),
                Hora = entity.Hora,
                Numero = entity.Numero,
                TelefonoContacto = entity.TelefonoContacto,
                ContactosEmergencia = entity.ContactosEmergencia.Select(x => new FirestoreModels.ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),

            };

        }
        private Alerta MapFirestoreModelToEntity(FirestoreModels.Alerta model)
        {
            return new Alerta
            {
                Id = model.Id,
                Ubicacion = model.Ubicacion,
                Fecha = model.Fecha,
                Hora = model.Hora,
                Numero = model.Numero,
                TelefonoContacto = model.TelefonoContacto,
                ContactosEmergencia = model.ContactosEmergencia.Select(x => new ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
            };
        }
    }
}

