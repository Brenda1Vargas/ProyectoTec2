﻿using Application.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace Application.Data.Respositories
{
    public class MenorRepository : IRepository<Menor>
    {
        private const string COLLECTION_NAME = "menor";
        private readonly Connection _connection;

        public MenorRepository(Connection dbConnection)
        {
            _connection = dbConnection;
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
            FirestoreModels.Alerta alarmaEmergencia = null;
            List<FirestoreModels.Ubicacion> lugaresFrecuentes = null;
            List<FirestoreModels.Ubicacion> _ubicacionActual = null;
            List<FirestoreModels.ContactoEmergencia> _contactoEmergencia = null;



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


            if (entity.ContactoEmergencia != null)
            {
                _contactoEmergencia = entity.ContactoEmergencia.Select(x =>
                    new FirestoreModels.ContactoEmergencia
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

            return new FirestoreModels.Menor
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age,
                Id = entity.Id,
                LugaresFrecuentes = lugaresFrecuentes,
                AlarmaEmergencia = alarmaEmergencia,
                ContactoEmergencia = _contactoEmergencia,
                LatitudHogar = entity.LatitudHogar,
                LongitudHogar = entity.LongitudHogar,
                ImageUrl = entity.ImageUrl,
                LocationUrl = entity.LocationUrl
            };
        }
        private Menor MapFirebaseModelToEntity(FirestoreModels.Menor model)
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
                    ContactosEmergencia = model.AlarmaEmergencia.ContactosEmergencia.Select(x => new ContactoEmergencia { Id = x.Id, Age = x.Age, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, FullName = x.FullName, Parentezco = x.Parentezco, TelefonoContacto = x.TelefonoContacto }).ToList(),
                };
            }
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
            return new Menor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Id = model.Id,
                LugaresFrecuentes = lugaresFrecuentes,
                AlarmaEmergencia = alarmaEmergencia,
                UbicacionActual = _ubicacionActual,
                ContactoEmergencia = _contactoEmergencia,
                LatitudHogar = model.LatitudHogar,
                LongitudHogar = model.LongitudHogar,
                ImageUrl = model.ImageUrl,
                LocationUrl = model.LocationUrl
            };
        }
    }
}
