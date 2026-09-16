using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class Form1 : Form
    {
        public Form1()
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
            using var formulario = new frmArticulo();
            formulario.ShowDialog(this);
        }
    }
}

