using Dominio;
using System.Net.Http;

namespace TPWinForm_equipo_6
{
    public partial class frmDetalleArticulo : Form
    {
        private static readonly HttpClient cliente = new() { Timeout = TimeSpan.FromSeconds(15) };
        private readonly Articulo articulo;
        private readonly List<string> direcciones;
        private int indiceImagen;
        private CancellationTokenSource? cargaImagen;

        public frmDetalleArticulo(Articulo articulo)
        {
            ArgumentNullException.ThrowIfNull(articulo);
            InitializeComponent();
            this.articulo = articulo;
            // Copia de las direcciones: el detalle no modifica el artículo original.
            direcciones = articulo.Imagenes.Select(imagen => imagen.IdImagen).ToList();
        }

        private async void frmDetalleArticulo_Load(object? sender, EventArgs e)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtMarca.Text = articulo.Marca?.Descripcion ?? string.Empty;
            txtCategoria.Text = articulo.Categoria?.descripcion ?? string.Empty;
            txtPrecio.Text = articulo.Precio.ToString("N2");
            await MostrarImagenActualAsync();
        }

        private async void btnAnterior_Click(object? sender, EventArgs e)
        {
            if (indiceImagen <= 0) return;
            indiceImagen--;
            await MostrarImagenActualAsync();
        }

        private async void btnSiguiente_Click(object? sender, EventArgs e)
        {
            if (indiceImagen >= direcciones.Count - 1) return;
            indiceImagen++;
            await MostrarImagenActualAsync();
        }

        private async Task MostrarImagenActualAsync()
        {
            cargaImagen?.Cancel();
            ReemplazarImagen(null);
            btnAnterior.Enabled = indiceImagen > 0;
            btnSiguiente.Enabled = indiceImagen < direcciones.Count - 1;
            lblContador.Text = direcciones.Count == 0
                ? "Sin imágenes" : $"Imagen {indiceImagen + 1} de {direcciones.Count}";

            if (direcciones.Count == 0)
            {
                lblEstadoImagen.Text = "Este artículo no tiene imágenes.";
                return;
            }

            if (!Uri.TryCreate(direcciones[indiceImagen], UriKind.Absolute, out var uri)
                || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                lblEstadoImagen.Text = "La dirección de esta imagen no es válida.";
                return;
            }

            using var cancelacion = new CancellationTokenSource();
            cargaImagen = cancelacion;
            lblEstadoImagen.Text = "Cargando imagen…";
            try
            {
                // Descarga asincrónica: navegar y cerrar siguen disponibles durante la carga.
                byte[] contenido = await cliente.GetByteArrayAsync(uri, cancelacion.Token);
                if (cancelacion.IsCancellationRequested || IsDisposed) return;

                using var stream = new MemoryStream(contenido);
                using var original = Image.FromStream(stream);
                ReemplazarImagen(new Bitmap(original));
                lblEstadoImagen.Text = string.Empty;
            }
            catch (OperationCanceledException) when (cancelacion.IsCancellationRequested)
            {
                // Otra imagen o el cierre del formulario cancelaron esta carga.
            }
            catch (Exception ex)
            {
                if (!cancelacion.IsCancellationRequested && !IsDisposed)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                    lblEstadoImagen.Text = "No se pudo cargar la imagen. Podés seguir recorriendo las demás.";
                }
            }
            finally
            {
                if (ReferenceEquals(cargaImagen, cancelacion)) cargaImagen = null;
            }
        }

        private void ReemplazarImagen(Image? nueva)
        {
            Image? anterior = pictureBoxArticulo.Image;
            pictureBoxArticulo.Image = nueva;
            anterior?.Dispose();
        }

        private void LiberarImagen()
        {
            cargaImagen?.Cancel();
            ReemplazarImagen(null);
        }
    }
}
