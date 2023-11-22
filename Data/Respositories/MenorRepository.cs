using Application.Utils;
using Google.Cloud.Firestore;
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

        public List<Menor> FindAll()
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindAll...");

                Query query = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var querySnapshot = query.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var list = new List<Menor>();

                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    if (!documentSnapshot.Exists) continue;
                    var data = documentSnapshot.ConvertTo<FirestoreModels.Menor>();
                    if (data == null) continue;
                    data.Id = documentSnapshot.Id;
                    list.Add(MapFirebaseModelToEntity(data));
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

        public Menor FindById(string id)
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindById... {id}");

                var docRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(id);
                var snapshot = docRef.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (snapshot.Exists)
                {
                    var menorModel = snapshot.ConvertTo<FirestoreModels.Menor>();
                    menorModel.Id = snapshot.Id;
                    MessageLogger.LogInformationMessage($"Succes FindById... {id}");
                    return MapFirebaseModelToEntity(menorModel);
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

        public void Insert(Menor entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var fbModel = MapEntityToFirestoremodel(entity);
                var colRef = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var doc = colRef.AddAsync(fbModel).ConfigureAwait(false).GetAwaiter().GetResult();

                MessageLogger.LogInformationMessage($"Succes Insert... {entity.Id}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public Menor Update(Menor entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var recordRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(entity.Id);
                var fbModel = MapEntityToFirestoremodel(entity);
                recordRef.SetAsync(fbModel, SetOptions.MergeAll).ConfigureAwait(false).GetAwaiter().GetResult();
                MessageLogger.LogInformationMessage($"Success Insert... {entity.Id}");

                return entity;
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        private FirestoreModels.Menor MapEntityToFirestoremodel(Menor entity)
        {
            var alertaEmergencia = new FirestoreModels.Alerta
            {
                Fecha = entity.AlarmaEmergencia.Fecha,
                Id = entity.AlarmaEmergencia.Id,
                Mensaje = entity.AlarmaEmergencia.Mensaje,
                Ubicacion = entity.AlarmaEmergencia.Ubicacion,
                Hora = entity.AlarmaEmergencia.Hora,
                Numero = entity.AlarmaEmergencia.Numero,
                TelefonoContacto = entity.AlarmaEmergencia.TelefonoContacto,
                ContactosEmergencia = entity.AlarmaEmergencia.ContactosEmergencia.Select(x => new FirestoreModels.ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, FullName = x.FullName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
            };

            return new FirestoreModels.Menor
            {
                AlarmaEmergencia = alertaEmergencia,
                LatitudHogar = entity.LatitudHogar,
                LongitudHogar = entity.LongitudHogar
            };

        }
        private Menor MapFirebaseModelToEntity(FirestoreModels.Menor model)
        {
            var alarmaEmergencia = new Alerta
            {
                Fecha = model.AlarmaEmergencia.Fecha,
                Id = model.AlarmaEmergencia.Id,
                Mensaje = model.AlarmaEmergencia.Mensaje,
                Ubicacion = model.AlarmaEmergencia.Ubicacion,
                Hora = model.AlarmaEmergencia.Hora,
                Numero = model.AlarmaEmergencia.Numero,
                TelefonoContacto = model.AlarmaEmergencia.TelefonoContacto,
                ContactosEmergencia = model.AlarmaEmergencia.ContactosEmergencia?.Select(x => new FirestoreModels.ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, FullName = x.FullName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
            };

            return new Menor
            {
                AlarmaEmergencia = alarmaEmergencia,
                LongitudHogar = model.LongitudHogar,
                LatitudHogar = model.LatitudHogar,
                Id = model.Id,
            };
        }
    }
}
