using Dominio;
using TPWinForm_equipo_6.Datos;

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
    }
}