namespace TPWinForm_equipo_6
{
    partial class Form1
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
            SuspendLayout();
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Location = new Point(24, 24);
            dataGridView1.Size = new Size(752, 300);
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.MultiSelect = false;
            btnAgregar.Text = "Agregar";
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Location = new Point(24, 350);
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Text = "Modificar";
            btnModificar.Location = new Point(130, 350);
            btnVerDetalle.Text = "Ver detalle";
            btnVerDetalle.Location = new Point(236, 350);
            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new Point(342, 350);
            btnAgregar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnModificar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVerDetalle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(dataGridView1);
            Controls.Add(btnAgregar);
            Controls.Add(btnModificar);
            Controls.Add(btnVerDetalle);
            Controls.Add(btnEliminar);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Catálogo de artículos";
            Load += Form1_Load;
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
