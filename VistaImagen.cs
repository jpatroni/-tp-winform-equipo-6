using System.Net.Http;

namespace TPWinForm_equipo_6
{
    // Cancela la descarga anterior y evita bloquear la interfaz por URLs lentas o rotas.
    internal sealed class VistaImagen : IDisposable
    {
        private static readonly HttpClient cliente = new() { Timeout = TimeSpan.FromSeconds(15) };
        private readonly PictureBox cuadro;
        private readonly Label estado;
        private CancellationTokenSource? carga;
        private bool cerrado;
        public VistaImagen(PictureBox cuadro, Label estado) { this.cuadro = cuadro; this.estado = estado; cuadro.SizeMode = PictureBoxSizeMode.Zoom; }
        public async Task Mostrar(string direccion)
        {
            carga?.Cancel();
            using var actual = new CancellationTokenSource();
            carga = actual;
            cuadro.Image?.Dispose();
            cuadro.Image = null;
            estado.Text = string.IsNullOrWhiteSpace(direccion) ? "Sin imagen" : "Cargando imagen…";
            if (string.IsNullOrWhiteSpace(direccion)) { carga = null; return; }
            try
            {
                byte[] bytes;
                if (Uri.TryCreate(direccion, UriKind.Absolute, out var uri) && (uri.Scheme == "http" || uri.Scheme == "https"))
                    bytes = await cliente.GetByteArrayAsync(uri, actual.Token);
                else bytes = await File.ReadAllBytesAsync(direccion, actual.Token);
                if (cerrado || actual.IsCancellationRequested) return;
                using var memoria = new MemoryStream(bytes);
                using var original = Image.FromStream(memoria);
                cuadro.Image = new Bitmap(original);
                estado.Text = "";
            }
            catch (Exception)
            {
                if (!cerrado && !actual.IsCancellationRequested)
                    estado.Text = "No se pudo cargar la imagen. Podés continuar.";
            }
            finally { if (ReferenceEquals(carga, actual)) carga = null; }
        }
        public void Dispose()
        {
            cerrado = true;
            carga?.Cancel();
            cuadro.Image?.Dispose();
            cuadro.Image = null;
        }
    }
}
