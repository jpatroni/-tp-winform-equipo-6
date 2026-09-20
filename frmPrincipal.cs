using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();
                AgregarColumna(nameof(Articulo.Codigo), "Código");
                AgregarColumna(nameof(Articulo.Nombre), "Nombre");
                AgregarColumna(nameof(Articulo.Descripcion), "Descripción");
                AgregarColumna(nameof(Articulo.Marca), "Marca");
                AgregarColumna(nameof(Articulo.Categoria), "Categoría");
                AgregarColumna(nameof(Articulo.Precio), "Precio");
                dataGridView1.Columns[nameof(Articulo.Precio)]!.DefaultCellStyle.Format = "N2";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = new ArticuloNegocio().Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los artículos.\n" + ex.Message,
                    "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarColumna(string propiedad, string titulo)
        {
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = propiedad,
                DataPropertyName = propiedad,
                HeaderText = titulo
            });
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            using var formulario = new frmArticuloAlta();
            if ((formulario.ShowDialog(this)) == DialogResult.OK)
            {
                try
                {
                    dataGridView1.DataSource = new ArticuloNegocio().Listar();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("No se pudo actualizar el listado\n" + ex.Message);

                }


            }


        }

        private void btnVerDetalle_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show(this, "Seleccioná un artículo para ver su detalle.",
                    "Ver detalle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                using (frmDetalleArticulo formulario = new frmDetalleArticulo(seleccionado))
                {
                    formulario.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                MessageBox.Show(this, "No se pudo abrir el detalle del artículo.",
                    "Ver detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is not Articulo seleccionado)
            {
                MessageBox.Show("Seleccioná un artículo para modificar.");
                return;
            }
            try
            {
                using var modificar = new frmArticuloAlta(seleccionado);
                if (modificar.ShowDialog(this) == DialogResult.OK)
                    dataGridView1.DataSource = new ArticuloNegocio().Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo completar la modificación o actualizar el listado.\n" + ex.Message);
            }
        }
    }
}

