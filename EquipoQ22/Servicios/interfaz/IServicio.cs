using EquipoQ22.Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Servicios.interfaz
{
    internal interface IServicio
    {
        List<Persona> obtenerPersona();

        //Guardar el alta del ejercicio
        bool guardarAlta(Equipo equipos);
    }
}
