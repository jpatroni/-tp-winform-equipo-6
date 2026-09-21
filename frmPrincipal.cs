using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos = new List<Articulo>();


        public frmPrincipal()
        {
            InitializeComponent();
            MinimumSize = new Size(820, 480);
            txtBuscar.Location = new Point(160, 40);
            txtBuscar.Width = 290;
            txtBuscar.PlaceholderText = "Nombre, código, marca o categoría";
            Controls.Add(new Label { Text = "Buscar artículos", AutoSize = true, Location = new Point(42, 44) });
            var marcas = new Button { Text = "Marcas", Location = new Point(460, 350), AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            var categorias = new Button { Text = "Categorías", Location = new Point(560, 350), AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            marcas.Click += (_, _) => AbrirAdministracion(new frmMarcas());
            categorias.Click += (_, _) => AbrirAdministracion(new frmCategorias());
            Controls.Add(marcas);
            Controls.Add(categorias);
            btnEliminar.Text = "Dar de baja";
            btnEliminar.Width = 95;
        }

        private void AbrirAdministracion(Form formulario)
        {
            using (formulario) formulario.ShowDialog(this);
            try
            {
                listaArticulos = new ArticuloNegocio().Listar();
                txtBuscar_TextChanged(txtBuscar, EventArgs.Empty);
            }
            catch (Exception ex) { MessageBox.Show("No se pudo actualizar el listado.\n" + ex.Message); }
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
                listaArticulos = new ArticuloNegocio().Listar();
                dataGridView1.DataSource = listaArticulos;  // para mostrar los datos en el DataGridView



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
                    listaArticulos = new ArticuloNegocio().Listar();
                    txtBuscar_TextChanged(txtBuscar, EventArgs.Empty);// para actualizar la grilla con el nuevo artículo agregado

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
                {

                    listaArticulos = new ArticuloNegocio().Listar();
                    txtBuscar_TextChanged(txtBuscar, EventArgs.Empty); // para actualizar la grilla con el artículo modificado

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo completar la modificación o actualizar el listado.\n" + ex.Message);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string textoBuscado = txtBuscar.Text.Trim();
            List<Articulo> Resultados = new List<Articulo>();
            foreach (Articulo articulo in listaArticulos)
            {
                if (articulo.Nombre.Contains(textoBuscado, StringComparison.OrdinalIgnoreCase)
                    || articulo.Codigo.Contains(textoBuscado, StringComparison.OrdinalIgnoreCase)
                    || articulo.Marca.Descripcion.Contains(textoBuscado, StringComparison.OrdinalIgnoreCase)
                    || articulo.Categoria.descripcion.Contains(textoBuscado, StringComparison.OrdinalIgnoreCase))
                {
                    Resultados.Add(articulo);
                }
            }

            dataGridView1.DataSource = Resultados;

            /// al hacer esto resultados: una lista vacía donde guardaremos las coincidencias.
            ///foreach: recorre listaArticulos, un artículo por vez.
            ///Contains: pregunta si el nombre contiene el texto buscado.
            ///OrdinalIgnoreCase: permite buscar sin distinguir mayúsculas y minúsculas.
            ///resultados.Add(articulo): agrega el artículo que coincide.
            ///DataSource = resultados: muestra las coincidencias en la grilla.


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is not Articulo seleccionado)
            {
                MessageBox.Show("selecciona un Articulo para eliminar");
                return;


            }

            DialogResult respuesta = MessageBox.Show(
                $"¿Querés dar de baja el artículo \"{seleccionado.Nombre}\"?",
                "Confirmar baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if(respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.EliminarLogico(seleccionado.Id);

                listaArticulos = negocio.Listar();
                txtBuscar_TextChanged(txtBuscar, EventArgs.Empty); // para actualizar la grilla con el artículo eliminado


            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo eliminar el artículo.\n"

                + ex.Message);

            }

        }
    }
}

