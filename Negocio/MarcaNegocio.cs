using Dominio;
using System.Data;

namespace TPWinForm_equipo_6.Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            var lista = new List<Marca>();
            using var datos = new AccesoDatos();
            datos.SetearConsulta("SELECT Id, Descripcion FROM MARCAS WHERE Activo = 1 ORDER BY Descripcion");
            datos.EjecutarLectura();
            while (datos.Lector.Read())
                lista.Add(new Marca { Id = (int)datos.Lector["Id"], Descripcion = datos.Lector["Descripcion"] as string ?? string.Empty });
            return lista;
        }

        public void Agregar(string descripcion) => Guardar(0, descripcion);
        public void Modificar(int id, string descripcion)
        {
            if (id <= 0) throw new ArgumentException("Seleccioná un registro válido.");
            Guardar(id, descripcion);
        }

        private void Guardar(int id, string descripcion)
        {
            descripcion = descripcion?.Trim() ?? string.Empty;
            if (descripcion.Length == 0 || descripcion.Length > 50)
                throw new ArgumentException("La descripción debe tener entre 1 y 50 caracteres.");
            using var datos = new AccesoDatos();
            // La consulta y el guardado comparten una transacción para evitar duplicados concurrentes.
            datos.IniciarTransaccion();
            datos.SetearConsulta("SELECT COUNT(*) FROM MARCAS WITH (UPDLOCK, HOLDLOCK) WHERE Activo = 1 AND LTRIM(RTRIM(Descripcion)) = @Descripcion AND Id <> @Id");
            datos.SetearParametro("@Descripcion", SqlDbType.VarChar, descripcion);
            datos.SetearParametro("@Id", SqlDbType.Int, id);
            if (Convert.ToInt32(datos.EjecutarEscalar()) > 0)
                throw new ArgumentException("Ya existe un registro activo con esa descripción.");
            datos.SetearConsulta(id == 0
                ? "INSERT INTO MARCAS (Descripcion) VALUES (@Descripcion)"
                : "UPDATE MARCAS SET Descripcion = @Descripcion WHERE Id = @Id AND Activo = 1");
            datos.SetearParametro("@Descripcion", SqlDbType.VarChar, descripcion);
            if (id != 0) datos.SetearParametro("@Id", SqlDbType.Int, id);
            if (datos.EjecutarAccion() == 0) throw new InvalidOperationException("El registro ya no está disponible.");
            datos.ConfirmarTransaccion();
        }

        public bool EstaEnUso(int id)
        {
            using var datos = new AccesoDatos();
            datos.SetearConsulta("SELECT TOP 1 Id FROM ARTICULOS WHERE IdMarca = @Id AND Activo = 1");
            datos.SetearParametro("@Id", SqlDbType.Int, id);
            datos.EjecutarLectura();
            return datos.Lector.Read();
        }

        public void EliminarLogico(int id)
        {
            using var datos = new AccesoDatos();
            datos.IniciarTransaccion();
            datos.SetearConsulta("SELECT COUNT(*) FROM ARTICULOS WITH (UPDLOCK, HOLDLOCK) WHERE IdMarca = @Id AND Activo = 1");
            datos.SetearParametro("@Id", SqlDbType.Int, id);
            if (Convert.ToInt32(datos.EjecutarEscalar()) > 0)
                throw new InvalidOperationException("Hay artículos activos que usan este registro. Cambiá su asignación o dalos de baja primero.");
            datos.SetearConsulta("UPDATE MARCAS SET Activo = 0 WHERE Id = @Id AND Activo = 1");
            datos.SetearParametro("@Id", SqlDbType.Int, id);
            if (datos.EjecutarAccion() != 1) throw new InvalidOperationException("El registro ya no está disponible.");
            datos.ConfirmarTransaccion();
        }
    }
}
