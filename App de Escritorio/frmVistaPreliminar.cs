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
    public partial class frmVistaPreliminar : Form
    {
        private Libro libro = null;
        public frmVistaPreliminar()
        {
            InitializeComponent();
        }
        public frmVistaPreliminar(Libro libroPrevisualizado)
        {
            InitializeComponent();
            this.libro = libroPrevisualizado;
            
        }
        private void frmVistaPreliminar_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "IdCategoria";
                cboCategoria.DisplayMember = "Categoria";

                if (libro != null)
                {
                    txtCodigo.Text = libro.Codigo;
                    txtTitulo.Text = libro.Titulo;
                    txtAutor.Text = libro.Autor;
                    cboCategoria.SelectedValue = libro.Categoria.IdCategoria;
                    txtDescripcion.Text = libro.Descripcion;
                    txtUrlTapaLibro.Text = libro.UrlTapaLibro;
                    cargarImagen(libro.UrlTapaLibro);
                    dtpFechaPublicacion.Value = libro.FechaPublicacion;
                    txtStock.Text = libro.Stock.ToString();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxAltaTapa.Load(imagen);
            }
            catch (Exception)
            {

                pbxAltaTapa.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder-1024x683.png");

            }
        }
        private void txtUrlTapaLibro_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlTapaLibro.Text);
        }
        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                e.Handled = true;
        }
        private bool validarTextos()
        {
            if (string.IsNullOrEmpty(txtAutor.Text))
            {
                MessageBox.Show("El campo Autor no puede estar vacio.\n" +
                     "Ingrese el autor del libro por favor.", "Error campo vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MessageBox.Show("El campo Titulo no puede estar vacio.\n" +
                     "Ingrese el titulo del libro por favor.", "Error campo vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            if (string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox.Show("El campo Stock no puede estar vacio.\n" +
                     "Ingrese la cantidad de libros que hay en la biblioteca por favor.", "Error campo vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }

        private void btnVolverAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
