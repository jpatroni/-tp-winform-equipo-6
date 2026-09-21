using Dominio;

namespace TPWinForm_equipo_6.Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            var marcas = new List<Marca>();

            using var datos = new AccesoDatos();

            datos.SetearConsulta("""
                SELECT Id, Descripcion
                FROM MARCAS
                ORDER BY Id
                """);

            datos.EjecutarLectura();

            var lector = datos.Lector;

            while (lector.Read())
            {
                var marca = new Marca
                {
                    Id = (int)lector["Id"],
                    Descripcion = lector["Descripcion"] as string ?? string.Empty
                };

                marcas.Add(marca);
            }

            return marcas;
        }

            public void Agregar(string descripcion)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta("""
        INSERT INTO MARCAS (Descripcion)
        VALUES (@descripcion)
        """);

            datos.SetearParametro(
                "@descripcion",
                System.Data.SqlDbType.VarChar,
                descripcion);

            datos.EjecutarAccion();
        }
        public void Modificar(int id, string descripcion)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta("""
        UPDATE MARCAS
        SET Descripcion = @descripcion
        WHERE Id = @id
        """);

            datos.SetearParametro(
                "@descripcion",
                System.Data.SqlDbType.VarChar,
                descripcion);

            datos.SetearParametro(
                "@id",
                System.Data.SqlDbType.Int,
                id);

            datos.EjecutarAccion();
        }

        public bool EstaEnUso(int id)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta("""
        SELECT TOP 1 Id
        FROM ARTICULOS
        WHERE IdMarca = @id
        """);

            datos.SetearParametro(
                "@id",
                System.Data.SqlDbType.Int,
                id);

            datos.EjecutarLectura();

            return datos.Lector.Read();
        }
        public void Eliminar(int id)
        {
            if (EstaEnUso(id))
            {
                throw new Exception("No se puede eliminar la marca porque está siendo utilizada por un artículo.");
            }

            using var datos = new AccesoDatos();

            datos.SetearConsulta("""
        DELETE FROM MARCAS
        WHERE Id = @id
        """);

            datos.SetearParametro(
                "@id",
                System.Data.SqlDbType.Int,
                id);

            datos.EjecutarAccion();
        }


    } 
    }

