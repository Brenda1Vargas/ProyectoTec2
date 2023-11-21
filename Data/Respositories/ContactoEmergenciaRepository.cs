using Application.Data.FirestoreModels;
using Application.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Respositories
{
    public class ContactoEmergenciaRepository : IRepository<ContactoEmergencia>
    {
        private const string COLLECTION_NAME = "contactoEmergencia";
        private readonly Connection _connection;

        public ContactoEmergenciaRepository(Connection dbConnetion)
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
        public List<ContactoEmergencia> FindAll()
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindAll...");

                Query query = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var querySnapshot = query.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var list = new List<ContactoEmergencia>();

                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    if (!documentSnapshot.Exists) continue;
                    var data = documentSnapshot.ConvertTo<FirestoreModels.ContactoEmergencia>();
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
        public ContactoEmergencia FindById(string id)
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindById... {id}");

                var docRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(id);
                var snapshot = docRef.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (snapshot.Exists)
                {
                    var contactoEmergenciaModel = snapshot.ConvertTo<FirestoreModels.ContactoEmergencia>();
                    contactoEmergenciaModel.Id = snapshot.Id;
                    MessageLogger.LogInformationMessage($"Succes FindById... {id}");
                    return MapFirestoreModelToEntity(contactoEmergenciaModel);
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

        public void Insert(ContactoEmergencia entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var fbModel = MapEntityToFirestoremodel(entity);
                var colRef = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var doc = colRef.AddAsync(fbModel).ConfigureAwait(false).GetAwaiter().GetResult();

                MessageLogger.LogInformationMessage($"Succes Insert... {entity.FirstName}");
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public ContactoEmergencia Update(ContactoEmergencia entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Insert... {entity.Id}");

                var recordRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(entity.Id);
                var fbModel = MapEntityToFirestoremodel(entity);
                recordRef.SetAsync(fbModel, SetOptions.MergeAll).ConfigureAwait(false).GetAwaiter().GetResult();
                MessageLogger.LogInformationMessage($"Success Insert... {entity.FirstName}");

                return entity;
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        private FirestoreModels.ContactoEmergencia MapEntityToFirestoremodel(ContactoEmergencia entity)
        {
            return new FirestoreModels.ContactoEmergencia
            {
                Id = entity.Id,
                Age = entity.Age,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Parentezco = entity.Parentezco,
                TelefonoContacto = entity.TelefonoContacto,
            };

        }
        private ContactoEmergencia MapFirestoreModelToEntity(FirestoreModels.ContactoEmergencia model)
        {
            return new ContactoEmergencia(model.Id, model.Age, model.FirstName, model.LastName, model.Email, model.Parentezco, model.TelefonoContacto);
        } 
    }
}
