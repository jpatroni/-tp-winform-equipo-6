using Dominio;

namespace TPWinForm_equipo_6.Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            var articulos = new Dictionary<int, Articulo>();
            using var datos = new AccesoDatos();
            datos.SetearConsulta("""
                SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio,
                       M.Id AS MarcaId, M.Descripcion AS MarcaDescripcion,
                       C.Id AS CategoriaId, C.Descripcion AS CategoriaDescripcion,
                       I.Id AS ImagenId, I.ImagenUrl
                FROM ARTICULOS A
                LEFT JOIN MARCAS M ON M.Id = A.IdMarca
                LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria
                LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id
                ORDER BY A.Id, I.Id
                """);
            datos.EjecutarLectura();
            var lector = datos.Lector;
            while (lector.Read())
            {
                int id = (int)lector["Id"];
                if (!articulos.TryGetValue(id, out var articulo))
                {
                    articulo = new Articulo
                    {
                        Id = id,
                        Codigo = lector["Codigo"] as string ?? string.Empty,
                        Nombre = lector["Nombre"] as string ?? string.Empty,
                        Descripcion = lector["Descripcion"] as string ?? string.Empty,
                        Precio = lector["Precio"] == DBNull.Value ? 0 : (decimal)lector["Precio"],
                        Marca = new Marca
                        {
                            Id = lector["MarcaId"] == DBNull.Value ? 0 : (int)lector["MarcaId"],
                            Descripcion = lector["MarcaDescripcion"] as string ?? string.Empty
                        },
                        Categoria = new Categoria
                        {
                            Id = lector["CategoriaId"] == DBNull.Value ? 0 : (int)lector["CategoriaId"],
                            descripcion = lector["CategoriaDescripcion"] as string ?? string.Empty
                        }
                    };
                    articulos.Add(id, articulo);
                }

                if (lector["ImagenId"] != DBNull.Value)
                {
                    articulo.Imagenes.Add(new Imagen
                    {
                        Id = (int)lector["ImagenId"],
                        IdArticulo = id,
                        IdImagen = lector["ImagenUrl"] as string ?? string.Empty
                    });
                }
            }
            return articulos.Values.ToList();
        }
    }
}
