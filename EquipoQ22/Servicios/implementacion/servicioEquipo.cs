using EquipoQ22.Datos.implementacion;
using EquipoQ22.Datos.interfaz;
using EquipoQ22.Domino;
using EquipoQ22.Servicios.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Servicios.implementacion
{
    internal class servicioEquipo : IServicio
    {
        private IEquipoDao dao;

        public servicioEquipo()
        {
            dao = new equipoDao();
        }
        public bool guardarAlta(Equipo equipos)
        {
            return dao.crear(equipos);
        }

        public List<Persona> obtenerPersona()
        {
            return dao.obtenerPersona();
        }
    }
}
