using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmMarca : Form
    {
        private List<Marca> listaMarcas = new List<Marca>();

        public frmMarca()
        {
            InitializeComponent();
            CargarMarcas();
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            listaMarcas = negocio.Listar();

            dgvMarca.DataSource = null;
            dgvMarca.DataSource = listaMarcas;
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcionMarca.Text))
                {
                    MessageBox.Show("Ingrese una descripción.");
                    return;
                }

                MarcaNegocio negocio = new MarcaNegocio();

                negocio.Agregar(txtDescripcionMarca.Text);

                MessageBox.Show("Marca agregada correctamente.");

                txtDescripcionMarca.Clear();
                CargarMarcas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMarca.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una marca.");
                    return;
                }

                Marca marca = (Marca)dgvMarca.CurrentRow.DataBoundItem;

                if (string.IsNullOrWhiteSpace(txtDescripcionMarca.Text))
                {
                    MessageBox.Show("Ingrese una descripción.");
                    return;
                }

                MarcaNegocio negocio = new MarcaNegocio();

                negocio.Modificar(marca.Id, txtDescripcionMarca.Text);

                MessageBox.Show("Marca modificada correctamente.");

                txtDescripcionMarca.Clear();
                CargarMarcas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMarca.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una marca.");
                    return;
                }

                Marca marca = (Marca)dgvMarca.CurrentRow.DataBoundItem;

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de eliminar la marca?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                    return;

                MarcaNegocio negocio = new MarcaNegocio();

                negocio.Eliminar(marca.Id);

                MessageBox.Show("Marca eliminada correctamente.");

                txtDescripcionMarca.Clear();
                CargarMarcas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvMarca_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarca.CurrentRow != null &&
                dgvMarca.CurrentRow.DataBoundItem is Marca marca)
            {
                txtDescripcionMarca.Text = marca.Descripcion;
            }
        }

        private void btnCerrarMarca_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}