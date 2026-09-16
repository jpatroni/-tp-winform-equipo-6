namespace TPWinForm_equipo_6
{
    partial class frmArticulo
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            SuspendLayout();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblCodigo.Text = "Código";
            lblCodigo.Location = new Point(24, 24);
            lblCodigo.AutoSize = true;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Location = new Point(130, 24);
            txtCodigo.Size = new Size(310, 23);
            txtCodigo.TabIndex = 0;
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigo);
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblNombre.Text = "Nombre";
            lblNombre.Location = new Point(24, 68);
            lblNombre.AutoSize = true;
            txtNombre.Name = "txtNombre";
            txtNombre.Location = new Point(130, 68);
            txtNombre.Size = new Size(310, 23);
            txtNombre.TabIndex = 1;
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblDescripcion.Text = "Descripción";
            lblDescripcion.Location = new Point(24, 112);
            lblDescripcion.AutoSize = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Location = new Point(130, 112);
            txtDescripcion.Size = new Size(310, 23);
            txtDescripcion.TabIndex = 2;
            txtDescripcion.Multiline = true;
            txtDescripcion.Height = 70;
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            lblPrecio = new Label();
            nudPrecio = new NumericUpDown();
            lblPrecio.Text = "Precio";
            lblPrecio.Location = new Point(24, 204);
            lblPrecio.AutoSize = true;
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Location = new Point(130, 204);
            nudPrecio.Size = new Size(310, 23);
            nudPrecio.TabIndex = 3;
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Maximum = 100000000;
            nudPrecio.ThousandsSeparator = true;
            Controls.Add(lblPrecio);
            Controls.Add(nudPrecio);
            lblMarca = new Label();
            cboMarca = new ComboBox();
            lblMarca.Text = "Marca";
            lblMarca.Location = new Point(24, 248);
            lblMarca.AutoSize = true;
            cboMarca.Name = "cboMarca";
            cboMarca.Location = new Point(130, 248);
            cboMarca.Size = new Size(310, 23);
            cboMarca.TabIndex = 4;
            cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(lblMarca);
            Controls.Add(cboMarca);
            lblCategoria = new Label();
            cboCategoria = new ComboBox();
            lblCategoria.Text = "Categoría";
            lblCategoria.Location = new Point(24, 292);
            lblCategoria.AutoSize = true;
            cboCategoria.Name = "cboCategoria";
            cboCategoria.Location = new Point(130, 292);
            cboCategoria.Size = new Size(310, 23);
            cboCategoria.TabIndex = 5;
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(lblCategoria);
            Controls.Add(cboCategoria);

            btnGuardar = new Button();
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(240, 350);
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 6;
            btnGuardar.Enabled = false;
            btnCancelar = new Button();
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(345, 350);
            btnCancelar.Size = new Size(95, 30);
            btnCancelar.TabIndex = 7;
            btnCancelar.DialogResult = DialogResult.Cancel;
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            CancelButton = btnCancelar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 405);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "frmArticulo";
            Text = "Agregar artículo";
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblPrecio;
        private NumericUpDown nudPrecio;
        private Label lblMarca;
        private ComboBox cboMarca;
        private Label lblCategoria;
        private ComboBox cboCategoria;

        private Button btnGuardar;
        private Button btnCancelar;
    }
}

