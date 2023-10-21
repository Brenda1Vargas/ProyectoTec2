using System;
using System.Threading;

namespace Application
{
    public class LineaEmergencia
    {
        private int numeroEmergencia;

        public int NumeroEmergencia
        {
            get { return numeroEmergencia; }
            set { numeroEmergencia = value; }
        }

        public LineaEmergencia(int numeroEmergencia)
        {
            this.numeroEmergencia = numeroEmergencia;
        }

        public void RealizarLlamadaEmergencia()
        {
            Console.WriteLine($"Realizando llamada de emergencia al número {NumeroEmergencia}...");

            Thread.Sleep(3000);

            Console.WriteLine("Llamada de emergencia completada.");
        }
    }


}