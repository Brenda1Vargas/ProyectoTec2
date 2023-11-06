﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public List<Mayor> FindAll()
        {
            throw new NotImplementedException();
        }

        public Mayor FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Mayor entity)
        {
            throw new NotImplementedException();
        }

        public Mayor Update(Mayor entity)
        {
            throw new NotImplementedException();
        }

        private FirestoreModels.Mayor MapEntityToFirestoremodel(Mayor entity)
        {
            return new FirestoreModels.Mayor
            {
                UbicacionActual = entity.UbicacionActual,
                AlarmaEmergencia = entity.AlarmaEmergencia,
                LatitudHogar = entity.LatitudHogar,
                LongitudHogar = entity.LongitudHogar
            };

        }
        private Mayor MapFirebaseModelToEntity(FirestoreModels.Mayor model)
        {
            return new Mayor(model.UbicacionActual, model.AlarmaEmergencia, model.LatitudHogar, model.LongitudHogar);
        }

    
    }
}

