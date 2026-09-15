namespace TPWinForm_equipo_6
{
    partial class dgvArticulos
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnVerDetalle = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(69, 12);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(672, 206);
            dataGridView1.TabIndex = 0;
            dataGridView1.ReadOnlyChanged += dataGridView1_ReadOnlyChanged;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(105, 262);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(257, 262);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnVerDetalle
            // 
            btnVerDetalle.Location = new Point(417, 262);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new Size(75, 23);
            btnVerDetalle.TabIndex = 3;
            btnVerDetalle.Text = "Ver detalle";
            btnVerDetalle.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(600, 262);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += button4_Click;
            // 
            // dgvArticulos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnVerDetalle);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dataGridView1);
            Name = "dgvArticulos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Catálogo de artículos";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnVerDetalle;
        private Button btnEliminar;
    }
}
