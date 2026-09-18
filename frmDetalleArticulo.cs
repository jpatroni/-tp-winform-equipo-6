using Dominio;
using System.ComponentModel;

namespace TPWinForm_equipo_6
{
    public partial class frmDetalleArticulo : Form
    {
        private Articulo? articulo;
        private int indiceImagen = 0;
        private bool cargandoImagen = false;
        private bool cerrando = false;
        private System.Windows.Forms.Timer tiempoCarga;

        // El constructor sin parámetros permite abrir el diseñador de Visual Studio.
        public frmDetalleArticulo()
        {
            InitializeComponent();
            tiempoCarga = new System.Windows.Forms.Timer(components!);
            tiempoCarga.Interval = 15000;
            tiempoCarga.Tick += tiempoCarga_Tick;
        }

        public frmDetalleArticulo(Articulo articulo) : this()
        {
            this.articulo = articulo;
        }

        private void frmDetalleArticulo_Load(object? sender, EventArgs e)
        {
            if (articulo == null)
            {
                MessageBox.Show(this, "No se recibió un artículo para mostrar.");
                Close();
                return;
            }

            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString("N2");
            if (articulo.Marca != null)
                txtMarca.Text = articulo.Marca.Descripcion;
            if (articulo.Categoria != null)
                txtCategoria.Text = articulo.Categoria.descripcion;

            MostrarImagen();
        }

        private void btnAnterior_Click(object? sender, EventArgs e)
        {
            if (cargandoImagen || indiceImagen == 0) return;
            indiceImagen--;
            MostrarImagen();
        }

        private void btnSiguiente_Click(object? sender, EventArgs e)
        {
            if (articulo == null || cargandoImagen) return;
            if (indiceImagen >= articulo.Imagenes.Count - 1) return;
            indiceImagen++;
            MostrarImagen();
        }

        private void ActualizarBotones()
        {
            btnAnterior.Enabled = !cargandoImagen && indiceImagen > 0;
            btnSiguiente.Enabled = !cargandoImagen && articulo != null
                && indiceImagen < articulo.Imagenes.Count - 1;
        }

        private void MostrarImagen()
        {
            LimpiarImagen();
            ActualizarBotones();
            if (articulo == null || articulo.Imagenes.Count == 0)
            {
                lblContador.Text = "Sin imágenes";
                lblEstadoImagen.Text = "Este artículo no tiene imágenes.";
                return;
            }

            lblContador.Text = "Imagen " + (indiceImagen + 1) + " de " + articulo.Imagenes.Count;
            // En el modelo actual, IdImagen contiene la dirección web, no un número.
            string direccion = articulo.Imagenes[indiceImagen].IdImagen;
            if (!Uri.TryCreate(direccion, UriKind.Absolute, out Uri? uri)
                || (uri.Scheme != "http" && uri.Scheme != "https"))
            {
                lblEstadoImagen.Text = "La dirección de esta imagen no es válida.";
                return;
            }

            try
            {
                cargandoImagen = true;
                ActualizarBotones();
                lblEstadoImagen.Text = "Cargando imagen…";
                tiempoCarga.Start();
                // PictureBox descarga sin bloquear la ventana y avisa en LoadCompleted.
                pictureBoxArticulo.LoadAsync(direccion);
            }
            catch (Exception ex)
            {
                tiempoCarga.Stop();
                cargandoImagen = false;
                System.Diagnostics.Trace.TraceError(ex.ToString());
                lblEstadoImagen.Text = "No se pudo iniciar la carga de la imagen.";
                ActualizarBotones();
            }
        }

        private void pictureBoxArticulo_LoadCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (cerrando) return;
            tiempoCarga.Stop();
            cargandoImagen = false;

            // Los errores de una descarga asincrónica llegan aquí, no al catch anterior.
            if (e.Cancelled)
                lblEstadoImagen.Text = "La carga superó el tiempo de espera. Podés seguir navegando.";
            else if (e.Error != null)
            {
                System.Diagnostics.Trace.TraceError(e.Error.ToString());
                lblEstadoImagen.Text = "No se pudo cargar la imagen. Podés seguir navegando.";
                LimpiarImagen();
            }
            else
                lblEstadoImagen.Text = string.Empty;

            ActualizarBotones();
        }

        private void tiempoCarga_Tick(object? sender, EventArgs e)
        {
            tiempoCarga.Stop();
            pictureBoxArticulo.CancelAsync();
        }

        private void LimpiarImagen()
        {
            Image? anterior = pictureBoxArticulo.Image;
            pictureBoxArticulo.Image = null;
            if (anterior != null) anterior.Dispose();
        }

        private void LiberarImagen()
        {
            cerrando = true;
            if (tiempoCarga != null) tiempoCarga.Stop();
            pictureBoxArticulo.LoadCompleted -= pictureBoxArticulo_LoadCompleted;
            pictureBoxArticulo.CancelAsync();
            LimpiarImagen();
        }
    }
}
