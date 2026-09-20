using Dominio;
using Microsoft.Win32;
using System.Configuration;
using System.IO;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmArticuloAlta : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        public frmArticuloAlta()
        {
            InitializeComponent();
        }

        //Modificar
        public frmArticuloAlta(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = nudPrecio.Value;
                if (cboMarca.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una marca.");
                    return;
                }
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                if (cboCategoria.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una categoría.");
                    return;
                }
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                ArticuloNegocio negocio = new ArticuloNegocio();

                if (articulo.Id != 0)
                {
                    negocio.Modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                // Guardo imagen si la levantó localmente:
                if (archivo != null && !(txtURLImagen.Text.ToUpper().Contains("HTTP")))
                    File.Copy(
                        archivo.FileName,
                        ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName
                    );

                Close();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());


            }
        }





        private void frmArticuloAlta_Load(object sender, EventArgs e)
        {
            try
            {

                MarcaNegocio negocio = new MarcaNegocio();
                cboMarca.DataSource = negocio.Listar();
                cboMarca.DisplayMember = "descripcion";
                cboMarca.ValueMember = "id";

                //CategoriaNegocio negocio = new CategoriaNegocio();
                //cboCategoria.DataSource = negocio.Listar();
                //cboCategoria.DisplayMember = "descripcion";
                //cboCategoria.ValueMember = "id";

                if (articulo != null)
                {

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());


            }
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtURLImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnCancelar_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtURLImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                //guardo la imagen
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
            }
        }
    }
}