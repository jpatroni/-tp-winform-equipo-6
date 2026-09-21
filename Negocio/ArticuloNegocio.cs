using Dominio;
using System.Data;

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
                WHERE A.Activo = 1
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
        public void EliminarLogico(int id)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta("UPDATE ARTICULOS SET Activo = 0 WHERE Id = @Id");

            datos.SetearParametro("@Id", SqlDbType.Int, id);

            datos.EjecutarAccion();

        }
        public void Agregar(Articulo nuevo) => Guardar(nuevo, false);
        public void Modificar(Articulo nuevo) => Guardar(nuevo, true);

        private void Guardar(Articulo articulo, bool modificar)
        {
            Validar(articulo);
            using var datos = new AccesoDatos();
            datos.IniciarTransaccion();
            datos.SetearConsulta("SELECT COUNT(*) FROM MARCAS WITH (UPDLOCK, HOLDLOCK) WHERE Id = @Marca AND Activo = 1");
            datos.SetearParametro("@Marca", SqlDbType.Int, articulo.Marca.Id);
            if (Convert.ToInt32(datos.EjecutarEscalar()) != 1) throw new ArgumentException("Seleccioná una marca activa.");
            datos.SetearConsulta("SELECT COUNT(*) FROM CATEGORIAS WITH (UPDLOCK, HOLDLOCK) WHERE Id = @Categoria AND Activo = 1");
            datos.SetearParametro("@Categoria", SqlDbType.Int, articulo.Categoria.Id);
            if (Convert.ToInt32(datos.EjecutarEscalar()) != 1) throw new ArgumentException("Seleccioná una categoría activa.");
            datos.SetearConsulta(modificar
                ? "UPDATE ARTICULOS SET Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Descripcion, IdMarca=@Marca, IdCategoria=@Categoria, Precio=@Precio WHERE Id=@Id AND Activo=1"
                : "INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio) OUTPUT INSERTED.Id VALUES (@Codigo,@Nombre,@Descripcion,@Marca,@Categoria,@Precio)");
            datos.SetearParametro("@Codigo", SqlDbType.VarChar, articulo.Codigo.Trim());
            datos.SetearParametro("@Nombre", SqlDbType.VarChar, articulo.Nombre.Trim());
            datos.SetearParametro("@Descripcion", SqlDbType.VarChar, articulo.Descripcion.Trim());
            datos.SetearParametro("@Marca", SqlDbType.Int, articulo.Marca.Id);
            datos.SetearParametro("@Categoria", SqlDbType.Int, articulo.Categoria.Id);
            datos.SetearParametro("@Precio", SqlDbType.Money, articulo.Precio);
            int id = articulo.Id;
            if (modificar)
            {
                datos.SetearParametro("@Id", SqlDbType.Int, id);
                if (datos.EjecutarAccion() != 1) throw new InvalidOperationException("El artículo ya no está activo.");
                datos.SetearConsulta("DELETE FROM IMAGENES WHERE IdArticulo=@Id");
                datos.SetearParametro("@Id", SqlDbType.Int, id);
                datos.EjecutarAccion();
            }
            else id = Convert.ToInt32(datos.EjecutarEscalar());
            foreach (Imagen imagen in articulo.Imagenes)
            {
                datos.SetearConsulta("INSERT INTO IMAGENES (IdArticulo,ImagenUrl) VALUES (@Id,@Url)");
                datos.SetearParametro("@Id", SqlDbType.Int, id);
                datos.SetearParametro("@Url", SqlDbType.VarChar, imagen.IdImagen.Trim());
                datos.EjecutarAccion();
            }
            datos.ConfirmarTransaccion();
            articulo.Id = id;
        }

        public static void Validar(Articulo articulo)
        {
            if (string.IsNullOrWhiteSpace(articulo.Codigo) || articulo.Codigo.Trim().Length > 50)
                throw new ArgumentException("El código debe tener entre 1 y 50 caracteres.");
            if (string.IsNullOrWhiteSpace(articulo.Nombre) || articulo.Nombre.Trim().Length > 50)
                throw new ArgumentException("El nombre debe tener entre 1 y 50 caracteres.");
            if (articulo.Descripcion.Length > 150) throw new ArgumentException("La descripción admite hasta 150 caracteres.");
            if (articulo.Precio <= 0 || articulo.Precio > 922337203685477.5807m) throw new ArgumentException("Ingresá un precio válido mayor que cero.");
            if (articulo.Marca.Id <= 0 || articulo.Categoria.Id <= 0) throw new ArgumentException("Seleccioná marca y categoría.");
            if (articulo.Imagenes.Any(i => string.IsNullOrWhiteSpace(i.IdImagen) || i.IdImagen.Length > 1000))
                throw new ArgumentException("Cada dirección de imagen debe tener entre 1 y 1000 caracteres.");
        }
    }
}
