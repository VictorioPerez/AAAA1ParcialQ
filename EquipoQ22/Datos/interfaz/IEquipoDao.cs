using EquipoQ22.Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Datos.interfaz
{
    internal interface IEquipoDao
    {
        List<Persona> obtenerPersona(); //PARA EL CBO
        bool crear(Equipo equipos); 
    }
}
