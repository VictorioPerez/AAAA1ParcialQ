using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Domino
{
    public class Persona
    {
        public Persona()
        {

        }

        public Persona(int idPersona, string nombreCompleto, int clase)
        {
            IdPersona = idPersona;
            NombreCompleto = nombreCompleto;
            Clase = clase;
        }

        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; }
        public int Clase { get; set; }

    }
}
