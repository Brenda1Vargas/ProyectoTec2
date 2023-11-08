using System;
using System.Collections.Generic;


namespace Application
{
    public class LineaEmergencia
    {
        private string id;
        private int numeroEmergencia;
        private string ubicacionEmergencia;

        public int NumeroEmergencia
        {
            get { return numeroEmergencia; }
            set { numeroEmergencia = value; }
        }



        public string UbicacionEmergencia
        {
            get { return ubicacionEmergencia; }
            set { ubicacionEmergencia = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public LineaEmergencia()
        {
                
        }

        public LineaEmergencia(int numeroEmergencia)
        {
            this.numeroEmergencia = numeroEmergencia;
            this.ubicacionEmergencia = ""; // Inicializamos la ubicación vacía
        }

        public LineaEmergencia(string id, int numeroEmergencia, string ubicacionEmergencia)
        {
            this.id = id;
            this.numeroEmergencia = numeroEmergencia;
            this.ubicacionEmergencia = ubicacionEmergencia;
        }

        public void RealizarLlamadaEmergencia()
        {
            if (!string.IsNullOrEmpty(ubicacionEmergencia))
            {
                Console.WriteLine($"Realizando llamada de emergencia al número {NumeroEmergencia}...");
                Console.WriteLine($"Ubicación: {UbicacionEmergencia}");
                // Simulación de llamada (sleep)
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Llamada de emergencia completada.");
            }
            else
            {
                Console.WriteLine("Ubicación de emergencia no definida. No se puede realizar la llamada.");
            }
        }
    }
}
