using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmMarcas : Form
    {
        private List<Marca> listaMarcas = new List<Marca>();

        public frmMarcas()
        {
            InitializeComponent();
            Text = "Marcas";
            StartPosition = FormStartPosition.CenterParent;
            dgvMarca.ReadOnly = true;
            dgvMarca.AllowUserToAddRows = false;
            dgvMarca.AllowUserToDeleteRows = false;
            dgvMarca.MultiSelect = false;
            dgvMarca.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMarca.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtDescripcionMarca.MaxLength = 50;
            Load += (_, _) =>
            {
                try { CargarMarcas(); }
                catch (Exception ex) { MessageBox.Show("No se pudieron cargar las marcas.\n" + ex.Message); }
            };
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            listaMarcas = negocio.Listar();

            dgvMarca.DataSource = null;
            dgvMarca.DataSource = listaMarcas;
            dgvMarca.Columns["Id"]!.Visible = false;
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
                if (dgvMarca.CurrentRow?.DataBoundItem is not Marca marca)
                {
                    MessageBox.Show("Seleccione una marca.");
                    return;
                }


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
                if (dgvMarca.CurrentRow?.DataBoundItem is not Marca marca)
                {
                    MessageBox.Show("Seleccione una marca.");
                    return;
                }


                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de dar de baja la marca?",
                    "Confirmar baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (respuesta != DialogResult.Yes)
                    return;

                MarcaNegocio negocio = new MarcaNegocio();

                negocio.EliminarLogico(marca.Id);

                MessageBox.Show("Marca dada de baja correctamente.");

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