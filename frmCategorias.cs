using TPWinForm_equipo_6.Negocio;
namespace TPWinForm_equipo_6
{
    public partial class frmCategorias : frmAdministracion
    {
        public frmCategorias()
        {
            InitializeComponent();
            var negocio = new CategoriaNegocio();
            Configurar("Categorías", () => negocio.Listar().Select(c => new FilaCatalogo { Id = c.Id, Descripcion = c.descripcion }).ToList(),
                negocio.Agregar, negocio.Modificar, negocio.EliminarLogico);
        }
    }
}
