using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace App_de_Escritorio
{
    public partial class frmBiblioteca : Form
    {
        private List<Libro> listaLibros;
        public frmBiblioteca()
        {
            InitializeComponent();
        }

        private void frmBiblioteca_Load(object sender, EventArgs e)
        {
            cargar();
            ocultarColumnas();
            cboCampo.Items.Add("Título");
            cboCampo.Items.Add("Autor");        
        }
        public void cargar()
        {
            LibroNegocio negocio = new LibroNegocio();
            listaLibros = negocio.listar();
            dgvLibros.DataSource = listaLibros;
            ocultarColumnas();
        }
        public void ocultarColumnas()
        {
            dgvLibros.Columns["UrlTapaLibro"].Visible = false;
            dgvLibros.Columns["IdLibros"].Visible = false;
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxLibros.Load(imagen);
            }
            catch (Exception)
            {

                pbxLibros.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder-1024x683.png");

            }
        }

        private void dgvLibros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLibros.CurrentRow != null)
            {
                Libro seleccionado = (Libro)dgvLibros.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlTapaLibro);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaLibro altaLibro = new frmAltaLibro();
            altaLibro.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Libro objetoSeleccionado;
            objetoSeleccionado = (Libro)dgvLibros.CurrentRow.DataBoundItem;
            frmAltaLibro modificadoLibro = new frmAltaLibro(objetoSeleccionado);
            modificadoLibro.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();
            Libro objetoSeleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar el libro seleccionado?", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    
                    objetoSeleccionado = (Libro)dgvLibros.CurrentRow.DataBoundItem;
                    negocio.eliminar(objetoSeleccionado.IdLibros);
                    MessageBox.Show("Registro eliminado.");
                    cargar();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();
            try
            {
                if (validarFiltro())
                    return;
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvLibros.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Libro> listaFiltrada;

            string filtro = txtFiltroRapido.Text;



            if (filtro.Length >= 3)
            {
                listaFiltrada = listaLibros.FindAll(x => x.Titulo.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Categoria.ToUpper().Contains(filtro.ToUpper()) || x.Autor.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                listaFiltrada = listaLibros;
            }

            dgvLibros.DataSource = null;
            dgvLibros.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Título")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Empieza con: ");
                cboCriterio.Items.Add("Termina con: ");
                cboCriterio.Items.Add("Contiene: ");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Empieza con: ");
                cboCriterio.Items.Add("Termina con: ");
                cboCriterio.Items.Add("Contiene: ");
            }
        }
        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el campo para filtrar por favor.");
                return true;
            }
            if (cboCriterio.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el criterio para filtrar por favor.");
                return true;
            }
            return false;
        }

        private void dgvLibros_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Libro objetoSeleccionado;
            objetoSeleccionado = (Libro)dgvLibros.CurrentRow.DataBoundItem;
            frmVistaPreliminar libroVisualizado = new frmVistaPreliminar(objetoSeleccionado);
            libroVisualizado.ShowDialog();
            cargar();
            
            
        }
    }
}
