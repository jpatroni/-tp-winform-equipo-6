using System.ComponentModel;

namespace TPWinForm_equipo_6
{
    // Una misma pantalla para los dos catálogos evita duplicar los eventos del ABM.
    public class frmAdministracion : Form
    {
        private readonly DataGridView grilla = new() { Name = "dgvRegistros", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        private readonly TextBox descripcion = new() { Name = "txtDescripcion", Width = 280, MaxLength = 50 };
        private Func<List<FilaCatalogo>> listar = () => new();
        private Action<string> agregar = _ => { };
        private Action<int, string> modificar = (_, _) => { };
        private Action<int> eliminar = _ => { };
        public frmAdministracion()
        {
            ClientSize = new Size(760, 430);
            MinimumSize = new Size(680, 350);
            StartPosition = FormStartPosition.CenterParent;
            var inferior = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 92, Padding = new Padding(8), WrapContents = true };
            inferior.Controls.Add(new Label { Text = "Descripción", AutoSize = true, Margin = new Padding(3, 8, 3, 3) });
            inferior.Controls.Add(descripcion);
            var nuevo = new Button { Text = "Limpiar", AutoSize = true };
            nuevo.Click += (_, _) => { grilla.ClearSelection(); descripcion.Clear(); descripcion.Focus(); };
            inferior.Controls.Add(nuevo);
            inferior.SetFlowBreak(nuevo, true);
            Boton(inferior, "Agregar", () => { agregar(descripcion.Text); Recargar(); });
            Boton(inferior, "Modificar", () => { modificar(Seleccionado().Id, descripcion.Text); Recargar(); });
            Boton(inferior, "Dar de baja", () =>
            {
                var fila = Seleccionado();
                if (MessageBox.Show(this, $"¿Dar de baja '{fila.Descripcion}'?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
                eliminar(fila.Id);
                Recargar();
            });
            Boton(inferior, "Cerrar", Close);
            Controls.Add(grilla);
            Controls.Add(inferior);
            grilla.SelectionChanged += (_, _) =>
            {
                if (grilla.SelectedRows.Count > 0 && grilla.CurrentRow?.DataBoundItem is FilaCatalogo fila) descripcion.Text = fila.Descripcion;
            };
            Load += (_, _) => Intentar(Recargar);
        }
        protected void Configurar(string titulo, Func<List<FilaCatalogo>> lectura, Action<string> alta, Action<int,string> cambio, Action<int> baja)
        { Text = titulo; listar = lectura; agregar = alta; modificar = cambio; eliminar = baja; }
        private FilaCatalogo Seleccionado() => grilla.SelectedRows.Count > 0 && grilla.CurrentRow?.DataBoundItem is FilaCatalogo fila
            ? fila : throw new ArgumentException("Seleccioná un registro.");
        private void Recargar()
        {
            grilla.DataSource = listar();
            grilla.Columns[nameof(FilaCatalogo.Id)]!.Visible = false;
            grilla.ClearSelection();
            descripcion.Clear();
        }
        private void Boton(FlowLayoutPanel panel, string texto, Action accion)
        {
            var boton = new Button { Text = texto, AutoSize = true };
            boton.Click += (_, _) => Intentar(accion);
            panel.Controls.Add(boton);
        }
        private void Intentar(Action accion)
        {
            try { accion(); }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        public class FilaCatalogo
        {
            public int Id { get; set; }
            [DisplayName("Descripción")]
            public string Descripcion { get; set; } = "";
        }
    }
}
