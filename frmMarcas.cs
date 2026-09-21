using TPWinForm_equipo_6.Negocio;
namespace TPWinForm_equipo_6
{
    public class frmMarcas : frmAdministracion
    {
        public frmMarcas()
        {
            var negocio = new MarcaNegocio();
            Configurar("Marcas", () => negocio.Listar().Select(m => new FilaCatalogo { Id = m.Id, Descripcion = m.Descripcion }).ToList(),
                negocio.Agregar, negocio.Modificar, negocio.EliminarLogico);
        }
    }
}
