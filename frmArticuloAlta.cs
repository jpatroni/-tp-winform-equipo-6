using Dominio;
using System.ComponentModel;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmArticuloAlta : Form
    {
        private Articulo articulo = new();
        private readonly BindingList<string> imagenes = new();
        private readonly ListBox lstImagenes = new() { Name = "lstImagenes", Location = new Point(24, 370), Size = new Size(780, 110), HorizontalScrollbar = true };
        private readonly Label estadoImagen = new() { Location = new Point(453, 290), Size = new Size(350, 20) };
        private readonly VistaImagen vistaImagen;

        public frmArticuloAlta()
        {
            InitializeComponent();
            ClientSize = new Size(831, 550);
            btnGuardar.Location = new Point(610, 505);
            btnCancelar.Location = new Point(710, 505);
            btnAgregarImagen.Text = "Elegir archivos…";
            lblURLImagen.Text = "URL / archivo";
            txtCodigo.MaxLength = 50;
            txtNombre.MaxLength = 50;
            txtDescripcion.MaxLength = 150;
            txtURLImagen.MaxLength = 1000;
            lstImagenes.DataSource = imagenes;
            Controls.Add(lstImagenes);
            Controls.Add(estadoImagen);
            vistaImagen = new VistaImagen(pbxArticulo, estadoImagen);
            Disposed += (_, _) => vistaImagen.Dispose();
            var agregarUrl = new Button { Name = "btnAgregarUrl", Text = "Añadir dirección", Location = new Point(610, 310), Size = new Size(145, 25) };
            agregarUrl.Click += (_, _) => Intentar(() => AgregarDireccion(txtURLImagen.Text));
            var quitar = new Button { Name = "btnQuitarImagen", Text = "Quitar seleccionada", Location = new Point(24, 490), AutoSize = true };
            quitar.Click += (_, _) => { if (lstImagenes.SelectedIndex >= 0) imagenes.RemoveAt(lstImagenes.SelectedIndex); };
            Controls.Add(agregarUrl);
            Controls.Add(quitar);
            Controls.Add(new Label { Text = "Imágenes del artículo (añadí direcciones o seleccioná varios archivos)", Location = new Point(24, 346), AutoSize = true });
            lstImagenes.SelectedIndexChanged += (_, _) => cargarImagen(lstImagenes.SelectedItem as string ?? "");
            AcceptButton = btnGuardar;
        }

        public frmArticuloAlta(Articulo original) : this()
        {
            // El formulario edita una copia; cancelar nunca modifica la grilla.
            articulo = new Articulo
            {
                Id = original.Id, Codigo = original.Codigo, Nombre = original.Nombre,
                Descripcion = original.Descripcion, Precio = original.Precio,
                Marca = original.Marca, Categoria = original.Categoria
            };
            foreach (var imagen in original.Imagenes) imagenes.Add(imagen.IdImagen);
            Text = "Modificar artículo";
        }

        private void frmArticuloAlta_Load(object sender, EventArgs e)
        {
            try
            {
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";
                cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
                cboMarca.DataSource = new MarcaNegocio().Listar();
                cboCategoria.DisplayMember = "descripcion";
                cboCategoria.ValueMember = "Id";
                cboCategoria.DataSource = new CategoriaNegocio().Listar();
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                nudPrecio.Maximum = Math.Max(nudPrecio.Maximum, articulo.Precio);
                nudPrecio.Minimum = Math.Min(nudPrecio.Minimum, articulo.Precio);
                nudPrecio.Value = articulo.Precio;
                if (articulo.Id != 0)
                {
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                }
                cargarImagen(lstImagenes.SelectedItem as string ?? "");
                if (cboMarca.Items.Count == 0 || cboCategoria.Items.Count == 0)
                    estadoImagen.Text = "Creá marcas y categorías antes de guardar.";
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = false;
                MostrarError(ex);
            }
        }

        private static bool EsWeb(string valor) => Uri.TryCreate(valor, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

        private void AgregarDireccion(string valor)
        {
            string direccion = valor.Trim();
            if (direccion.Length == 0 || direccion.Length > 1000 || (!EsWeb(direccion) && !File.Exists(direccion)))
                throw new ArgumentException("Ingresá una dirección http/https o un archivo existente (hasta 1000 caracteres).");
            if (!imagenes.Contains(direccion)) imagenes.Add(direccion);
            lstImagenes.SelectedItem = direccion;
            txtURLImagen.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var copias = new List<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(txtURLImagen.Text)) AgregarDireccion(txtURLImagen.Text);
                articulo.Codigo = txtCodigo.Text.Trim();
                articulo.Nombre = txtNombre.Text.Trim();
                articulo.Descripcion = txtDescripcion.Text.Trim();
                articulo.Precio = nudPrecio.Value;
                articulo.Marca = cboMarca.SelectedItem as Marca ?? throw new ArgumentException("Seleccioná una marca.");
                articulo.Categoria = cboCategoria.SelectedItem as Categoria ?? throw new ArgumentException("Seleccioná una categoría.");
                articulo.Imagenes = imagenes.Select(i => new Imagen { IdImagen = i }).ToList();
                ArticuloNegocio.Validar(articulo);
                // Copiamos archivos locales a una carpeta del usuario, no a la raíz de C:.
                string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CatalogoEquipo6", "Imagenes");
                foreach (var imagen in articulo.Imagenes)
                {
                    if (EsWeb(imagen.IdImagen)) continue;
                    if (!File.Exists(imagen.IdImagen)) throw new IOException("No se encuentra la imagen: " + imagen.IdImagen);
                    string origen = Path.GetFullPath(imagen.IdImagen);
                    if (origen.StartsWith(Path.GetFullPath(carpeta) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) continue;
                    Directory.CreateDirectory(carpeta);
                    string destino = Path.Combine(carpeta, Guid.NewGuid().ToString("N") + Path.GetExtension(origen));
                    File.Copy(origen, destino);
                    copias.Add(destino);
                    imagen.IdImagen = destino;
                }
                btnGuardar.Enabled = false;
                var negocio = new ArticuloNegocio();
                if (articulo.Id == 0) negocio.Agregar(articulo);
                else negocio.Modificar(articulo);
                // Desde aquí las copias pertenecen al registro confirmado.
                copias.Clear();
                MessageBox.Show(this, "Artículo guardado correctamente.", "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                foreach (string copia in copias)
                    try { File.Delete(copia); } catch (IOException) { }
                MostrarError(ex);
            }
            finally { btnGuardar.Enabled = true; }
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtURLImagen.Text)) cargarImagen(txtURLImagen.Text.Trim());
        }
        private async void cargarImagen(string direccion) => await vistaImagen.Mostrar(direccion);
        private void btnCancelar_Click(object sender, EventArgs e) => Close();
        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            using var archivo = new OpenFileDialog { Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.gif;*.bmp", Multiselect = true };
            if (archivo.ShowDialog(this) == DialogResult.OK)
                Intentar(() => { foreach (string nombre in archivo.FileNames) AgregarDireccion(nombre); });
        }
        private void Intentar(Action accion) { try { accion(); } catch (Exception ex) { MostrarError(ex); } }
        private void MostrarError(Exception ex) => MessageBox.Show(this, ex.Message, "No se pudo completar la operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
