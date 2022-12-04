using EquipoQ22.Servicios.implementacion;
using EquipoQ22.Servicios.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Servicios
{
    internal class serviceFactoryImpl : abstractFactoryService
    {
        public override IServicio crearServicio()
        {
            return new servicioEquipo(); //SERVICIO DE LA CARPETA IMPLEMENTACION
        }
    }
}
