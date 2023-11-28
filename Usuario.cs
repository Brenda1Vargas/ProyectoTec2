using System;
using System.Collections.Generic;

namespace Application
{
    public class Usuario
    {
        private string _id;
        private string firstName;
        private string lastName;
        private int age;
        private List<Ubicacion> _ubicacionActual;
        private List<Ubicacion> _lugaresFrecuentes;
        private List<ContactoEmergencia> _contactoEmergencia;
        private string _locationUrl;
        private string _imageUrl;


        public Usuario()
        {
        }


        public Usuario(string locationUrl, string imageUrl, List<Ubicacion> ubicacionActual, List<Ubicacion> lugaresFrecuentes, List<ContactoEmergencia> contactoEmergencia, string id, string firstName, string lastName, int age)
        {
            LocationUrl = locationUrl;
            ImageUrl = imageUrl;
            UbicacionActual = ubicacionActual;
            LugaresFrecuentes = lugaresFrecuentes;
            ContactoEmergencia = contactoEmergencia;
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string LocationUrl { get => _locationUrl; set => _locationUrl = value; }
        public string ImageUrl { get => _imageUrl; set => _imageUrl = value; }
        public List<Ubicacion> UbicacionActual { get => _ubicacionActual; set => _ubicacionActual = value; }
        public List<Ubicacion> LugaresFrecuentes { get => _lugaresFrecuentes; set => _lugaresFrecuentes = value; }
        public List<ContactoEmergencia> ContactoEmergencia { get => _contactoEmergencia; set => _contactoEmergencia = value; }
        public string Id { get => _id; set => _id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Age { get => age; set => age = value; }
    }

}
