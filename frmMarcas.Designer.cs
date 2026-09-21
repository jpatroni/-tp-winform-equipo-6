namespace TPWinForm_equipo_6
{
    partial class frmMarcas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvMarca = new DataGridView();
            btnAgregarMarca = new Button();
            btnModificarMarca = new Button();
            btnEliminar = new Button();
            btnCerrarMarca = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMarca).BeginInit();
            SuspendLayout();
            // 
            // dgvMarca
            // 
            dgvMarca.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMarca.Location = new Point(128, 58);
            dgvMarca.Name = "dgvMarca";
            dgvMarca.RowHeadersWidth = 62;
            dgvMarca.Size = new Size(476, 232);
            dgvMarca.TabIndex = 0;
            // 
            // btnAgregarMarca
            // 
            btnAgregarMarca.Location = new Point(128, 348);
            btnAgregarMarca.Name = "btnAgregarMarca";
            btnAgregarMarca.Size = new Size(112, 34);
            btnAgregarMarca.TabIndex = 1;
            btnAgregarMarca.Text = "Agregar";
            btnAgregarMarca.UseVisualStyleBackColor = true;
            // 
            // btnModificarMarca
            // 
            btnModificarMarca.Location = new Point(256, 348);
            btnModificarMarca.Name = "btnModificarMarca";
            btnModificarMarca.Size = new Size(112, 34);
            btnModificarMarca.TabIndex = 2;
            btnModificarMarca.Text = "Modificar";
            btnModificarMarca.UseVisualStyleBackColor = true;
            btnModificarMarca.Click += button2_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(374, 348);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(112, 34);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnCerrarMarca
            // 
            btnCerrarMarca.Location = new Point(492, 348);
            btnCerrarMarca.Name = "btnCerrarMarca";
            btnCerrarMarca.Size = new Size(112, 34);
            btnCerrarMarca.TabIndex = 4;
            btnCerrarMarca.Text = "Cerrar";
            btnCerrarMarca.UseVisualStyleBackColor = true;
            // 
            // frmMarcas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCerrarMarca);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificarMarca);
            Controls.Add(btnAgregarMarca);
            Controls.Add(dgvMarca);
            Name = "frmMarcas";
            Text = "frmMarcas";
            ((System.ComponentModel.ISupportInitialize)dgvMarca).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvMarca;
        private Button btnAgregarMarca;
        private Button btnModificarMarca;
        private Button btnEliminar;
        private Button btnCerrarMarca;
    }
}