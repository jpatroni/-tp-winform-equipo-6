using Dominio;
using System.Data;
using TPWinForm_equipo_6.Datos;

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
        public void Agregar(Articulo nuevo)

        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"
                 INSERT INTO ARTICULOS
                                  (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)
                                  VALUES
                                  (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio);
                 SELECT SCOPE_IDENTITY();
                                 ");

                datos.SetearParametro("@Codigo", SqlDbType.VarChar, nuevo.Codigo);
                datos.SetearParametro("@Nombre", SqlDbType.VarChar, nuevo.Nombre);
                datos.SetearParametro("@Descripcion", SqlDbType.VarChar, nuevo.Descripcion);
                datos.SetearParametro("@IdMarca", SqlDbType.Int, nuevo.Marca.Id);
                datos.SetearParametro("@IdCategoria", SqlDbType.Int, nuevo.Categoria.Id);
                datos.SetearParametro("@Precio", SqlDbType.Money, nuevo.Precio);

                //Obtiene el ID del artículo para relacionarlo con las imágenes. Sale del SCOPE IDENTITY
                int idArticulo = Convert.ToInt32(datos.EjecutarEscalar());

                if (nuevo.Imagenes != null && nuevo.Imagenes.Count > 0)
                {
                    foreach (Imagen imagen in nuevo.Imagenes)
                    {
                        AccesoDatos datosImagen = new AccesoDatos();
                        datosImagen.SetearConsulta(@"

                         INSERT INTO IMAGENES
                         (IdArticulo, ImagenUrl)
                         VALUES
                         (@IdArticulo, @ImagenUrl)");

                        datosImagen.SetearParametro("@IdArticulo", SqlDbType.Int, idArticulo);
                        datosImagen.SetearParametro("@ImagenUrl", SqlDbType.VarChar, imagen.IdImagen);
                        datosImagen.EjecutarAccion();
                    }
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"
            UPDATE ARTICULOS
            SET Codigo = @Codigo,
                Nombre = @Nombre,
                Descripcion = @Descripcion,
                IdMarca = @IdMarca,
                IdCategoria = @IdCategoria,
                Precio = @Precio
            WHERE Id = @Id");

                datos.SetearParametro("@Id", SqlDbType.Int, nuevo.Id);
                datos.SetearParametro("@Codigo", SqlDbType.VarChar, nuevo.Codigo);
                datos.SetearParametro("@Nombre", SqlDbType.VarChar, nuevo.Nombre);
                datos.SetearParametro("@Descripcion", SqlDbType.VarChar, nuevo.Descripcion);
                datos.SetearParametro("@IdMarca", SqlDbType.Int, nuevo.Marca.Id);
                datos.SetearParametro("@IdCategoria", SqlDbType.Int, nuevo.Categoria.Id);
                datos.SetearParametro("@Precio", SqlDbType.Decimal, nuevo.Precio);

                datos.EjecutarAccion();

                //Primero eliminamos las imágenes anteriores
                AccesoDatos datosImagen = new AccesoDatos();

                datosImagen.SetearConsulta(@"
            DELETE FROM IMAGENES
            WHERE IdArticulo = @IdArticulo");

                datosImagen.SetearParametro("@IdArticulo", SqlDbType.Int, nuevo.Id);

                datosImagen.EjecutarAccion();

                //Después agregamos las imágenes nuevas
                if (nuevo.Imagenes != null && nuevo.Imagenes.Count > 0)
                {
                    foreach (Imagen imagen in nuevo.Imagenes)
                    {
                        AccesoDatos datosNuevaImagen = new AccesoDatos();

                        datosNuevaImagen.SetearConsulta(@"
                    INSERT INTO IMAGENES
                    (IdArticulo, ImagenUrl)
                    VALUES
                    (@IdArticulo, @ImagenUrl)");

                        datosNuevaImagen.SetearParametro("@IdArticulo", SqlDbType.Int, nuevo.Id);
                        datosNuevaImagen.SetearParametro("@ImagenUrl", SqlDbType.VarChar, imagen.IdImagen);

                        datosNuevaImagen.EjecutarAccion();
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}


