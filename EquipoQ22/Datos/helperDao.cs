using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EquipoQ22.Domino;

namespace EquipoQ22.Datos
{
    //PARA GUIARME: Receta--<>->DetalleReceta-->Ingrediente
    //PARA GUIARME: Equipo--<>->Jugador-->Persona
    //1-Creo carpetas = (1)Datos,(2)Servicios,Dominio,Presentacion
    //2-Empiezo HelperDao en(1), creo carpetas(1.1)interfaz e(1.2)implementacion 
    //3-InombreDao dentro de(1.1)
    //4-nombreDao dentro de(1.2)
    //5-En(2) creo la carpeta(2.1)implementacion e(2.2)interfaz
    //(ISERVICIO PRIMERO DESPUES SERVICIONOMBRE)
    //6-ServicioNombre dentro de(2.1)
    //7-IServicio dentro de(2.2)
    //8-abstractFactoryService dentro(2)
    //9-serviceFactoryImpl dentro(2)
    internal class helperDao
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private static helperDao instancia;

        public helperDao()
        {
            conn = new SqlConnection(@"");
            cmd = new SqlCommand();
        }

        public static helperDao obtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new helperDao();
            }
            return instancia;
        }
                                                                         //tabla que esta a la izq de -<>->  
        public bool CrearMaestroDetalleReceta(string insertSP, string insertDetSP, Equipo equipos)
        {
            bool aux = false;
            SqlTransaction t = null;
            try
            {
                conn.Open();
                t = conn.BeginTransaction();

                SqlCommand comando = new SqlCommand(insertSP, conn, t);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.AddWithValue("nombreColumnaSQL", parametroDeDato primeraTabla)
                comando.Parameters.AddWithValue("@pais", equipos.pais);
                comando.Parameters.AddWithValue("@director_tecnico", equipos.directorTecnico);

                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                comando.Parameters.Add(pOut);

                comando.ExecuteNonQuery();
                int identificador = (int)pOut.Value;
                SqlCommand comandoD = null;
                        //Tabla que se encuentra en el medio(detalle)
                foreach (Jugador jugadorD in equipos.listJugadores)
                {
                    comandoD = new SqlCommand(insertDetSP, conn, t);
                    comandoD.CommandType = CommandType.StoredProcedure;
                    //INGRESO LAS COLUMNAS QUE ESTAN EN EL DETALLE
                    //comandoD.Parameters.AddWithValue("nombreColumnaSQLDetalles", parametroDato);
                    comandoD.Parameters.AddWithValue("id_equipo", identificador);
                    comandoD.Parameters.AddWithValue("id_persona", jugadorD.Persona.IdPersona);
                    comandoD.Parameters.AddWithValue("camiseta", jugadorD.Camiseta);
                    comandoD.Parameters.AddWithValue("posicion", jugadorD.Posicion);
                    comandoD.ExecuteNonQuery();
                }
                t.Commit();
                aux = true;
            }
            catch (Exception ex)
            {
                if (t != null)
                    t.Rollback();
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return aux;
        }
        public DataTable combo(string nombreSP)
        {
            DataTable dt = new DataTable();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            cmd.Parameters.Clear();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return dt;
        }
    }
}
