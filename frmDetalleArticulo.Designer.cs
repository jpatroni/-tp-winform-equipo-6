#nullable enable

namespace TPWinForm_equipo_6
{
    partial class frmDetalleArticulo
    {
        private System.ComponentModel.IContainer? components;
        private TextBox txtCodigo = null!;
        private TextBox txtNombre = null!;
        private TextBox txtDescripcion = null!;
        private TextBox txtMarca = null!;
        private TextBox txtCategoria = null!;
        private TextBox txtPrecio = null!;
        private PictureBox pictureBoxArticulo = null!;
        private Label lblContador = null!;
        private Label lblEstadoImagen = null!;
        private Button btnAnterior = null!;
        private Button btnSiguiente = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LiberarImagen();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtCodigo = new TextBox();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            txtMarca = new TextBox();
            txtCategoria = new TextBox();
            txtPrecio = new TextBox();
            pictureBoxArticulo = new PictureBox();
            lblContador = new Label();
            lblEstadoImagen = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            SuspendLayout();

            var contenido = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 1
            };
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var datos = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 12, Padding = new Padding(0, 0, 15, 0) };
            string[] titulos = { "Código", "Nombre", "Descripción", "Marca", "Categoría", "Precio" };
            TextBox[] campos = { txtCodigo, txtNombre, txtDescripcion, txtMarca, txtCategoria, txtPrecio };
            for (int i = 0; i < campos.Length; i++)
            {
                datos.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
                datos.RowStyles.Add(i == 2
                    ? new RowStyle(SizeType.Percent, 100)
                    : new RowStyle(SizeType.Absolute, 36));
                datos.Controls.Add(new Label { Text = titulos[i], AutoSize = true, Anchor = AnchorStyles.Left }, 0, i * 2);
                campos[i].ReadOnly = true;
                campos[i].Dock = DockStyle.Fill;
                datos.Controls.Add(campos[i], 0, i * 2 + 1);
            }
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;

            var imagenes = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
            imagenes.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            imagenes.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            imagenes.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            imagenes.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            pictureBoxArticulo.Dock = DockStyle.Fill;
            pictureBoxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxArticulo.BackColor = Color.WhiteSmoke;
            lblEstadoImagen.Dock = DockStyle.Fill;
            lblEstadoImagen.TextAlign = ContentAlignment.MiddleCenter;
            lblContador.Dock = DockStyle.Fill;
            lblContador.TextAlign = ContentAlignment.MiddleCenter;
            var navegacion = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            btnAnterior.Text = "Anterior";
            btnAnterior.AutoSize = true;
            btnAnterior.Click += btnAnterior_Click;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.AutoSize = true;
            btnSiguiente.Click += btnSiguiente_Click;
            navegacion.Controls.AddRange(new Control[] { btnAnterior, btnSiguiente });
            imagenes.Controls.Add(pictureBoxArticulo, 0, 0);
            imagenes.Controls.Add(lblEstadoImagen, 0, 1);
            imagenes.Controls.Add(lblContador, 0, 2);
            imagenes.Controls.Add(navegacion, 0, 3);
            contenido.Controls.Add(datos, 0, 0);
            contenido.Controls.Add(imagenes, 1, 0);

            var pie = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 52, Padding = new Padding(10), FlowDirection = FlowDirection.RightToLeft };
            var cerrar = new Button { Text = "Cerrar", AutoSize = true, DialogResult = DialogResult.Cancel };
            pie.Controls.Add(cerrar);
            CancelButton = cerrar;
            Controls.Add(contenido);
            Controls.Add(pie);
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 560);
            MinimumSize = new Size(760, 540);
            StartPosition = FormStartPosition.CenterParent;
            Name = "frmDetalleArticulo";
            Text = "Detalle del artículo";
            MinimizeBox = false;
            Load += frmDetalleArticulo_Load;
            ResumeLayout(false);
        }
    }
}
