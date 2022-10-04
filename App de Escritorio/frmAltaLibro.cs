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
    public partial class frmAltaLibro : Form
    {
        private Libro libro = null;
        public frmAltaLibro()
        {
            InitializeComponent();
        }

        public frmAltaLibro(Libro libroModificado)
        {
            InitializeComponent();
            this.libro = libroModificado;
            Text = "Modificar Libro";
        }

        

        private void frmAltaLibro_Load(object sender, EventArgs e)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();

            try
            {
                if (validarTextos())
                    return;
                if (libro == null)
                    libro = new Libro();

                libro.Codigo = txtCodigo.Text;
                libro.Categoria = (Categorias)cboCategoria.SelectedItem;
                libro.Titulo = txtTitulo.Text;
                libro.Autor = txtAutor.Text;
                libro.Descripcion = txtDescripcion.Text;
                libro.UrlTapaLibro = txtUrlTapaLibro.Text;
                libro.FechaPublicacion = dtpFechaPublicacion.Value;
                libro.Stock = int.Parse(txtStock.Text);
                
                if (libro.IdLibros != 0)
                {
                    negocio.modificar(libro);
                    MessageBox.Show("Modificado exitosamente.", "Modificar Libro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    negocio.agregar(libro);
                    MessageBox.Show("Agregado exitosamente.", "Alta Libro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlTapaLibro_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlTapaLibro.Text);
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
    }
}
