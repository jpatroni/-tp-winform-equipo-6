namespace TPWinForm_equipo_6
{
    partial class frmArticuloAlta
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblPrecio = new Label();
            nudPrecio = new NumericUpDown();
            lblMarca = new Label();
            cboMarca = new ComboBox();
            lblCategoria = new Label();
            cboCategoria = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            lblURLImagen = new Label();
            txtURLImagen = new TextBox();
            btnAgregarImagen = new Button();
            pbxArticulo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxArticulo).BeginInit();
            SuspendLayout();
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(24, 24);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(46, 15);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(130, 24);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(310, 23);
            txtCodigo.TabIndex = 0;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(24, 68);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(130, 68);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(310, 23);
            txtNombre.TabIndex = 1;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(24, 112);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(69, 15);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(130, 112);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(310, 50);
            txtDescripcion.TabIndex = 2;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(24, 177);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(40, 15);
            lblPrecio.TabIndex = 3;
            lblPrecio.Text = "Precio";
            // 
            // nudPrecio
            // 
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Location = new Point(130, 177);
            nudPrecio.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(310, 23);
            nudPrecio.TabIndex = 3;
            nudPrecio.ThousandsSeparator = true;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(24, 221);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(40, 15);
            lblMarca.TabIndex = 4;
            lblMarca.Text = "Marca";
            // 
            // cboMarca
            // 
            cboMarca.Location = new Point(130, 221);
            cboMarca.Name = "cboMarca";
            cboMarca.Size = new Size(310, 23);
            cboMarca.TabIndex = 4;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(24, 265);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(58, 15);
            lblCategoria.TabIndex = 5;
            lblCategoria.Text = "Categoría";
            // 
            // cboCategoria
            // 
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoria.Location = new Point(130, 265);
            cboCategoria.Name = "cboCategoria";
            cboCategoria.Size = new Size(310, 23);
            cboCategoria.TabIndex = 5;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(240, 350);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(345, 350);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(95, 30);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblURLImagen
            // 
            lblURLImagen.AutoSize = true;
            lblURLImagen.Location = new Point(28, 310);
            lblURLImagen.Name = "lblURLImagen";
            lblURLImagen.Size = new Size(71, 15);
            lblURLImagen.TabIndex = 8;
            lblURLImagen.Text = "URL Imagen";
            // 
            // txtURLImagen
            // 
            txtURLImagen.Location = new Point(130, 310);
            txtURLImagen.Name = "txtURLImagen";
            txtURLImagen.Size = new Size(310, 23);
            txtURLImagen.TabIndex = 9;
            txtURLImagen.Leave += txtUrlImagen_Leave;
            // 
            // btnAgregarImagen
            // 
            btnAgregarImagen.Location = new Point(456, 312);
            btnAgregarImagen.Name = "btnAgregarImagen";
            btnAgregarImagen.Size = new Size(145, 23);
            btnAgregarImagen.TabIndex = 10;
            btnAgregarImagen.Text = "Agregar imagen";
            btnAgregarImagen.UseVisualStyleBackColor = true;
            btnAgregarImagen.Click += btnAgregarImagen_Click;
            // 
            // pbxArticulo
            // 
            pbxArticulo.Location = new Point(453, 21);
            pbxArticulo.Name = "pbxArticulo";
            pbxArticulo.Size = new Size(242, 267);
            pbxArticulo.TabIndex = 11;
            pbxArticulo.TabStop = false;
            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            // 
            // frmArticuloAlta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(831, 405);
            Controls.Add(pbxArticulo);
            Controls.Add(btnAgregarImagen);
            Controls.Add(txtURLImagen);
            Controls.Add(lblURLImagen);
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(lblPrecio);
            Controls.Add(nudPrecio);
            Controls.Add(lblMarca);
            Controls.Add(cboMarca);
            Controls.Add(lblCategoria);
            Controls.Add(cboCategoria);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmArticuloAlta";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar artículo";
            Load += frmArticuloAlta_Load;
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxArticulo).EndInit();
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
        private Label lblURLImagen;
        private TextBox txtURLImagen;
        private Button btnAgregarImagen;
        private PictureBox pbxArticulo;
    }
}

