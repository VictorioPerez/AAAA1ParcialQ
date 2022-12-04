using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Domino
{
    internal class Equipo
    {
        public string pais { get; set; }
        public string directorTecnico { get; set; }



        //listaJugador (LISTA DE LA TABLA QUE ESTA EN EL MEDIO)
        public List<Jugador> listJugadores { get; set; }
        public Equipo()
        {
            listJugadores = new List<Jugador>();
        }
        public void AGREGAR(Jugador jugadores)
        {
            listJugadores.Add(jugadores);
        }
        public void QUITAR(int posicion)
        {
            listJugadores.RemoveAt(posicion);
        }
    }
}
