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
        private Button btnCerrar = null!;
        private Label lblCodigo = null!;
        private Label lblNombre = null!;
        private Label lblDescripcion = null!;
        private Label lblMarca = null!;
        private Label lblCategoria = null!;
        private Label lblPrecio = null!;

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
            btnCerrar = new Button();
            lblCodigo = new Label();
            lblNombre = new Label();
            lblDescripcion = new Label();
            lblMarca = new Label();
            lblCategoria = new Label();
            lblPrecio = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxArticulo).BeginInit();
            SuspendLayout();
            lblCodigo.Name = "lblCodigo";
            txtCodigo.Name = "txtCodigo";
            lblCodigo.Text = "Código";
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(24, 20);
            txtCodigo.Location = new Point(24, 44);
            txtCodigo.Size = new Size(350, 23);
            txtCodigo.ReadOnly = true;
            txtCodigo.TabIndex = 0;
            lblNombre.Name = "lblNombre";
            txtNombre.Name = "txtNombre";
            lblNombre.Text = "Nombre";
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(24, 85);
            txtNombre.Location = new Point(24, 109);
            txtNombre.Size = new Size(350, 23);
            txtNombre.ReadOnly = true;
            txtNombre.TabIndex = 1;
            lblDescripcion.Name = "lblDescripcion";
            txtDescripcion.Name = "txtDescripcion";
            lblDescripcion.Text = "Descripción";
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(24, 150);
            txtDescripcion.Location = new Point(24, 174);
            txtDescripcion.Size = new Size(350, 112);
            txtDescripcion.ReadOnly = true;
            txtDescripcion.TabIndex = 2;
            lblMarca.Name = "lblMarca";
            txtMarca.Name = "txtMarca";
            lblMarca.Text = "Marca";
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(24, 300);
            txtMarca.Location = new Point(24, 324);
            txtMarca.Size = new Size(350, 23);
            txtMarca.ReadOnly = true;
            txtMarca.TabIndex = 3;
            lblCategoria.Name = "lblCategoria";
            txtCategoria.Name = "txtCategoria";
            lblCategoria.Text = "Categoría";
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(24, 365);
            txtCategoria.Location = new Point(24, 389);
            txtCategoria.Size = new Size(350, 23);
            txtCategoria.ReadOnly = true;
            txtCategoria.TabIndex = 4;
            lblPrecio.Name = "lblPrecio";
            txtPrecio.Name = "txtPrecio";
            lblPrecio.Text = "Precio";
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(24, 430);
            txtPrecio.Location = new Point(24, 454);
            txtPrecio.Size = new Size(350, 23);
            txtPrecio.ReadOnly = true;
            txtPrecio.TabIndex = 5;
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            pictureBoxArticulo.Name = "pictureBoxArticulo";
            pictureBoxArticulo.Location = new Point(404, 24);
            pictureBoxArticulo.Size = new Size(410, 340);
            pictureBoxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxArticulo.BackColor = Color.WhiteSmoke;
            pictureBoxArticulo.InitialImage = null;
            pictureBoxArticulo.ErrorImage = null;
            pictureBoxArticulo.TabStop = false;
            pictureBoxArticulo.LoadCompleted += pictureBoxArticulo_LoadCompleted;
            lblEstadoImagen.Location = new Point(404, 374);
            lblEstadoImagen.Size = new Size(410, 55);
            lblEstadoImagen.TextAlign = ContentAlignment.MiddleCenter;
            lblContador.Location = new Point(404, 433);
            lblContador.Size = new Size(410, 25);
            lblContador.TextAlign = ContentAlignment.MiddleCenter;
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Text = "Anterior";
            btnAnterior.Location = new Point(470, 469);
            btnAnterior.Size = new Size(100, 30);
            btnAnterior.TabIndex = 6;
            btnAnterior.Click += btnAnterior_Click;
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.Location = new Point(620, 469);
            btnSiguiente.Size = new Size(100, 30);
            btnSiguiente.TabIndex = 7;
            btnSiguiente.Click += btnSiguiente_Click;
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(714, 517);
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.TabIndex = 8;
            btnCerrar.DialogResult = DialogResult.Cancel;
            CancelButton = btnCerrar;
            Controls.Add(txtCodigo);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(txtMarca);
            Controls.Add(txtCategoria);
            Controls.Add(txtPrecio);
            Controls.Add(pictureBoxArticulo);
            Controls.Add(lblContador);
            Controls.Add(lblEstadoImagen);
            Controls.Add(btnAnterior);
            Controls.Add(btnSiguiente);
            Controls.Add(btnCerrar);
            Controls.Add(lblCodigo);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(lblMarca);
            Controls.Add(lblCategoria);
            Controls.Add(lblPrecio);
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 565);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "frmDetalleArticulo";
            Text = "Detalle del artículo";
            Load += frmDetalleArticulo_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxArticulo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
