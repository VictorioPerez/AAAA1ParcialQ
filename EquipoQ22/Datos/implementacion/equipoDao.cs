using EquipoQ22.Datos.interfaz;
using EquipoQ22.Domino;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Datos.implementacion
{
    internal class equipoDao : IEquipoDao //IMPORTANTE
    {
        public bool crear(Equipo equipos)
        {

            return helperDao.obtenerInstancia().CrearMaestroDetalleReceta("SP_INSERTAR_EQUIPO", "SP_INSERTAR_DETALLES_EQUIPO", equipos);
        }
        //METODO PARA OBTENER LOS DATOS PARA EL COMBOBOX
        public List<Persona> obtenerPersona()
        {
            List<Persona> personaList = new List<Persona>();
            DataTable tabla = helperDao.obtenerInstancia().combo("SP_CONSULTAR_PERSONAS");
            foreach (DataRow dr in tabla.Rows)
            {
                Persona persona = new Persona();
                persona.IdPersona = Convert.ToInt32(dr["id_persona"]);
                persona.NombreCompleto = (string)dr["nombre_completo"];
                persona.Clase = (int)dr["clase"];
                personaList.Add(persona);
            }
            return personaList;
        }
    }
}
