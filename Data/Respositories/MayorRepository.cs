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
    public class MayorRepository : IRepository<Mayor>
    {
        private const string COLLECTION_NAME = "mayor";
        private readonly Connection _connection;

        public MayorRepository(Connection dbConnetion)
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

        public List<Mayor> FindAll()
        {
            try
            {
                MessageLogger.LogInformationMessage($"FindAll...");

                Query query = _connection.FirestoreDb.Collection(COLLECTION_NAME);
                var querySnapshot = query.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var list = new List<Mayor>();

                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    if (!documentSnapshot.Exists) continue;
                    var data = documentSnapshot.ConvertTo<FirestoreModels.Mayor>();
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

        public Mayor FindById(string id)
        {
            try
            {
                if (id == null)
                {
                    // Manejar el caso en que id es nulo
                    return null;
                }

                MessageLogger.LogInformationMessage($"FindById... {id}");

                var docRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(id);
                var snapshot = docRef.GetSnapshotAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (snapshot.Exists)
                {
                    var mayorModel = snapshot.ConvertTo<FirestoreModels.Mayor>();
                    mayorModel.Id = snapshot.Id;

                    MessageLogger.LogInformationMessage($"Succes FindById... {id}");
                    return MapFirebaseModelToEntity(mayorModel);
                }
                else
                {
                    MessageLogger.LogWarningMessage("Colletion  doesn't exist");
                    return null;

                }
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        public void Insert(Mayor entity)
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

        public Mayor Update(Mayor entity)
        {
            try
            {
                MessageLogger.LogInformationMessage($"Update... {entity.Id}");

                var recordRef = _connection.FirestoreDb.Collection(COLLECTION_NAME).Document(entity.Id);
                var fbModel = MapEntityToFirestoremodel(entity);
                recordRef.SetAsync(fbModel, SetOptions.MergeAll).ConfigureAwait(false).GetAwaiter().GetResult();
                MessageLogger.LogInformationMessage($"Success Update... {entity.Id}");

                return entity;
            }
            catch (Exception ex)
            {
                MessageLogger.LogErrorMessage(ex);
                throw;
            }
        }

        private FirestoreModels.Mayor MapEntityToFirestoremodel(Mayor entity)
        {
            FirestoreModels.Alerta alarmaEmergencia = null;
            List<FirestoreModels.Ubicacion> lugaresFrecuentes = null;
            List<FirestoreModels.Ubicacion> _ubicacionActual = null;
            List<FirestoreModels.ContactoEmergencia> _contactoEmergencia = null;

            if (entity.ContactoEmergencia != null)
            {
                _contactoEmergencia = entity.ContactoEmergencia.Select(x =>
                    new FirestoreModels.ContactoEmergencia
                    {

                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        FullName = x.FullName,
                        Id = x.Id,
                        Age = x.Age,
                        Email = x.Email,
                        Parentezco = x.Parentezco,
                        TelefonoContacto = x.TelefonoContacto
                    }).ToList();
            }

            if (entity.AlarmaEmergencia != null)
            {
                alarmaEmergencia = new FirestoreModels.Alerta
                {

                    Fecha = entity.AlarmaEmergencia.Fecha,
                    Id = entity.AlarmaEmergencia.Id,
                    Mensaje = entity.AlarmaEmergencia.Mensaje,
                    Ubicacion = entity.AlarmaEmergencia.Ubicacion,
                    Hora = entity.AlarmaEmergencia.Hora,
                    Numero = entity.AlarmaEmergencia.Numero,
                    TelefonoContacto = entity.AlarmaEmergencia.TelefonoContacto,
                    ContactosEmergencia = entity.AlarmaEmergencia?.ContactosEmergencia?.Select(x => new FirestoreModels.ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, FullName = x.FullName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
                };


            }
            if (entity.LugaresFrecuentes != null)
            {
                lugaresFrecuentes = entity.LugaresFrecuentes.Select(x =>
                    new FirestoreModels.Ubicacion
                    {
                        Id = x.Id,
                        Longitud = x.Longitud,
                        Nombre = x.Nombre,
                        Latitud = x.Latitud
                    }).ToList();
            }

            if (entity.UbicacionActual != null)
            {
                _ubicacionActual = entity.UbicacionActual.Select(x =>
                    new FirestoreModels.Ubicacion
                    {
                        Id = x.Id,
                        Longitud = x.Longitud,
                        Nombre = x.Nombre,
                        Latitud = x.Latitud
                    }).ToList();
            }

            return new FirestoreModels.Mayor
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age,
                Id = entity.Id,
                LugaresFrecuentes = lugaresFrecuentes,
                UbicacionActual = _ubicacionActual,
                AlarmaEmergencia = alarmaEmergencia,
                ContactoEmergencia = entity.ContactoEmergencia?.Select(x => new FirestoreModels.ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, FullName = x.FullName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
                LatitudHogar = entity.LatitudHogar,
                LongitudHogar = entity.LongitudHogar,
                ImageUrl = entity.ImageUrl,
                LocationUrl = entity.LocationUrl
            };

        }
        private Mayor MapFirebaseModelToEntity(FirestoreModels.Mayor model)
        {
            Alerta alarmaEmergencia = null;
            List<Ubicacion> lugaresFrecuentes = null;
            List<Ubicacion> _ubicacionActual = null;
            List<ContactoEmergencia> _contactoEmergencia = null;

            if (model.AlarmaEmergencia != null)
            {
                alarmaEmergencia = new Alerta
                {

                    Fecha = model.AlarmaEmergencia.Fecha,
                    Id = model.AlarmaEmergencia.Id,
                    Mensaje = model.AlarmaEmergencia.Mensaje,
                    Ubicacion = model.AlarmaEmergencia.Ubicacion,
                    Hora = model.AlarmaEmergencia.Hora,
                    Numero = model.AlarmaEmergencia.Numero,
                    TelefonoContacto = model.AlarmaEmergencia.TelefonoContacto,
                    ContactosEmergencia = model.AlarmaEmergencia?.ContactosEmergencia?.Select(x => new ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, FullName = x.FullName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
                };

                if (model.LugaresFrecuentes != null)
                {
                    lugaresFrecuentes = model.LugaresFrecuentes.Select(x =>
                        new Ubicacion
                        {
                            Id = x.Id,
                            Longitud = x.Longitud,
                            Nombre = x.Nombre,
                            Latitud = x.Latitud
                        }).ToList();
                }
                if (model.UbicacionActual != null)
                {
                    _ubicacionActual = model.UbicacionActual.Select(x =>
                        new Ubicacion
                        {
                            Id = x.Id,
                            Longitud = x.Longitud,
                            Nombre = x.Nombre,
                            Latitud = x.Latitud
                        }).ToList();
                }

                if (model.ContactoEmergencia != null)
                {
                    _contactoEmergencia = model.ContactoEmergencia.Select(x =>
                          new ContactoEmergencia
                          {
                              Id = x.Id,
                              Age = x.Age,
                              Email = x.Email,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              FullName = x.FullName,
                              Parentezco = x.Parentezco,
                              TelefonoContacto = x.TelefonoContacto

                          }).ToList();
                }
            }

            return new Mayor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Id = model.Id,
                LugaresFrecuentes = lugaresFrecuentes,
                UbicacionActual= _ubicacionActual,
                AlarmaEmergencia = alarmaEmergencia,
                ContactoEmergencia = _contactoEmergencia,
                LatitudHogar = model.LatitudHogar,
                LongitudHogar = model.LongitudHogar,
                ImageUrl= model.ImageUrl,
                LocationUrl= model.LocationUrl

            };
        }
    }
}