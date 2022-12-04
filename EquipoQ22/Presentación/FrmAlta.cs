using EquipoQ22.Domino;
using EquipoQ22.Servicios;
using EquipoQ22.Servicios.implementacion;
using EquipoQ22.Servicios.interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//PARA GUIARME: Receta--<>->DetalleReceta-->Ingrediente
//PARA GUIARME: Equipo--<>->Jugador-->Persona
//1 - Creo carpetas = (1)Datos,(2)Servicios,Dominio,Presentacion
//2 - Empiezo HelperDao en(1), creo carpetas(1.1)interfaz e(1.2)implementacion
//3 - InombreDao dentro de(1.1)
//4 - nombreDao dentro de(1.2)
//5 - En(2) creo la carpeta (2.1)implementacion e(2.2)interfaz
//(ISERVICIO PRIMERO DESPUES SERVICIONOMBRE)
//6 - ServicioNombre dentro de(2.1)
//7 - IServicio dentro de(2.2)
//8 - abstractFactoryService dentro(2)
//9 - serviceFactoryImpl dentro(2)

namespace EquipoQ22
{
    public partial class FrmAlta : Form
    {
        private Equipo equiposN;
        private Jugador jugadorN;
        private IServicio service; //IMPORTANTE ESTO SI NO NO CARGAR CBO

        public FrmAlta()
        {
            InitializeComponent();
            equiposN = new Equipo();
            service = new  serviceFactoryImpl().crearServicio();//IMPORTANTE ESTO SI NO NO CARGAR CBO
            jugadorN = new Jugador();
        }
        private void FrmAlta_Load(object sender, EventArgs e)
        {
            cargarCombo();
        }
        public void limpiar()
        {
            txtDT.Text = "";
            txtPais.Text = "";
            cboPersona.Text = "";
            nudCamiseta.Value = 1;
            cboPosicion.Text = "";
        }

        private void cargarCombo()
        {
            cboPersona.DataSource = service.obtenerPersona();
            //Atributos de la clase que corresponde al combo
            cboPersona.ValueMember = "IdPersona";
            cboPersona.DisplayMember = "NombreCompleto";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (nudCamiseta.Value < 1 || nudCamiseta.Value > 23)
            {
                MessageBox.Show("Debe elegir a un numero de camiseta entre 1 y 23", "Error", MessageBoxButtons.OK);
                return;
            }
            //PARA VALIDAR DE QUE NO SE PUEDA INGRESAR OTRA VEZ UN DATO INGRESADO
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                //Nombre de la columna del nombre 
                if (row.Cells["camiseta"].Value.ToString().Equals(nudCamiseta.Value))
                {
                    MessageBox.Show("No puede agregar un mismo numero de camiseta", "Error", MessageBoxButtons.OK);
                    return;
                }
            }
            if (validacion())
            {
                jugadorN.Persona = (Persona)cboPersona.SelectedItem;
                jugadorN.Posicion = (string)cboPosicion.Text;
                jugadorN.Camiseta = (int)nudCamiseta.Value;
                equiposN.AGREGAR(jugadorN);
                //Lo que quiero que se muestre en la grilla
                dgvDetalles.Rows.Add(new object[] { jugadorN.Persona.IdPersona, jugadorN.Persona.NombreCompleto, jugadorN.Camiseta, jugadorN.Posicion });

                calcularTotal();
                //SUBTOTAL();
            }
        }
        private void calcularTotal()
        {
            lblTotal.Text = "Total: " + dgvDetalles.Rows.Count.ToString();
        }
        private bool validacion()
        {
            if (txtPais.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un pais", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtDT.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el nombre del director tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboPosicion.SelectedIndex == 0)
            {
                MessageBox.Show("Debe elegir una posicion!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtDT.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe ingresar el nombre del DT", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDT.Focus();
                return;
            }
            if (txtPais.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe ingresar el nombre del pais", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPais.Focus();
                return;

            }
            if (dgvDetalles.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar al menos un jugador", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            equiposN.directorTecnico = txtDT.Text;
            equiposN.pais = txtPais.Text;
            jugadorN.Camiseta = (int)nudCamiseta.Value;

            if (service.guardarAlta(equiposN))
            {
                MessageBox.Show("Se registro con exito", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar();
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cancelar?","cancelar",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        //BOTON QUITAR QUE ESTA EN LA GRILLA
        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                equiposN.QUITAR(dgvDetalles.CurrentRow.Index);

                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                calcularTotal();
            }
        }
    }
}
//----------------------------------------------------------------------------------
//PARA CALCULAR SUBTOTAL
//public int Subtotal()
//{
//    int subtotal = 0;
//    foreach (DataGridViewRow dr in dgvDetalles.Rows)
//    {
//        subtotal = Convert.ToInt32(dr.Cells["precio"].Value) * Convert.ToInt32(dr.Cells["cantidad"].Value);
//    }
//    return subtotal;
//}
