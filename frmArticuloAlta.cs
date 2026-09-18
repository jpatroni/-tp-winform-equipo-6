using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmArticuloAlta : Form
    {
        public frmArticuloAlta()
        {
            InitializeComponent();
            MarcaNegocio negocio = new MarcaNegocio();
            cboMarca.DataSource = negocio.Listar();
            cboMarca.DisplayMember = "descripcion";
            cboMarca.ValueMember = "id";


        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new();
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



                new ArticuloNegocio().Agregar(articulo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        private void btnCancelar_Click(Object sender, EventArgs e)
        {
            this.Close();
        }
    }
}