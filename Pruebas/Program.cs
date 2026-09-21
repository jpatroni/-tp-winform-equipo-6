using Dominio;
using Microsoft.Data.SqlClient;
using System.Reflection;
using TPWinForm_equipo_6;
using TPWinForm_equipo_6.Negocio;

internal static class Program
{
    static int verificaciones;
    static void Verificar(bool valor, string nombre)
    {
        if (!valor) throw new Exception("FALLÓ: " + nombre);
        Console.WriteLine("OK: " + nombre);
        verificaciones++;
    }
    static void Rechaza(Action accion, string nombre)
    {
        bool rechazo = false;
        try { accion(); } catch (Exception) { rechazo = true; }
        Verificar(rechazo, nombre);
    }
    static void Sql(string conexion, string sql)
    {
        using var c = new SqlConnection(conexion); c.Open();
        using var cmd = new SqlCommand(sql, c); cmd.ExecuteNonQuery();
    }
    static object Valor(string conexion, string sql)
    {
        using var c = new SqlConnection(conexion); c.Open();
        using var cmd = new SqlCommand(sql, c); return cmd.ExecuteScalar();
    }
    static void Evento(object obj, string metodo, params object[] args) =>
        obj.GetType().GetMethod(metodo, BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(obj, args);
    static T Campo<T>(object obj, string campo) => (T)obj.GetType().GetField(campo, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(obj)!;
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        string nombre = "CatalogoPruebas_" + Guid.NewGuid().ToString("N");
        string master = @"Server=.\SQLEXPRESS;Database=master;Integrated Security=True;TrustServerCertificate=True;";
        string conexion = master.Replace("Database=master", "Database=" + nombre);
        Sql(master, $"CREATE DATABASE [{nombre}]");
        try
        {
            Environment.SetEnvironmentVariable("CATALOGO_CONNECTION_STRING", conexion);
            Sql(conexion, """
                CREATE TABLE MARCAS (Id int IDENTITY PRIMARY KEY, Descripcion varchar(50), Activo bit NOT NULL DEFAULT 1);
                CREATE TABLE CATEGORIAS (Id int IDENTITY PRIMARY KEY, Descripcion varchar(50), Activo bit NOT NULL DEFAULT 1);
                CREATE TABLE ARTICULOS (Id int IDENTITY PRIMARY KEY, Codigo varchar(50), Nombre varchar(50), Descripcion varchar(150), IdMarca int REFERENCES MARCAS(Id), IdCategoria int REFERENCES CATEGORIAS(Id), Precio money, Activo bit NOT NULL DEFAULT 1);
                CREATE TABLE IMAGENES (Id int IDENTITY PRIMARY KEY, IdArticulo int REFERENCES ARTICULOS(Id), ImagenUrl varchar(1000));
                """);
            var marcas = new MarcaNegocio(); var categorias = new CategoriaNegocio(); var articulos = new ArticuloNegocio();
            marcas.Agregar("Marca prueba"); categorias.Agregar("Categoría prueba");
            Rechaza(() => marcas.Agregar("   "), "marca vacía rechazada");
            Rechaza(() => categorias.Agregar(new string('a', 51)), "categoría demasiado larga rechazada");
            Rechaza(() => marcas.Agregar(" Marca prueba "), "marca duplicada rechazada");
            Rechaza(() => categorias.Agregar("Categoría prueba"), "categoría duplicada rechazada");
            var marca = marcas.Listar().Single(); var categoria = categorias.Listar().Single();
            marcas.Modificar(marca.Id, "Marca editada"); categorias.Modificar(categoria.Id, "Categoría editada");
            Verificar(marcas.Listar().Single().Descripcion == "Marca editada" && categorias.Listar().Single().descripcion == "Categoría editada", "modificación de catálogos");
            var a = new Articulo { Codigo="COD-XYZ", Nombre="Producto prueba", Descripcion="Detalle", Precio=12.34m, Marca=marca, Categoria=categoria,
                Imagenes=Enumerable.Range(1,12).Select(i => new Imagen { IdImagen="https://example.com/"+i+".png" }).ToList() };
            articulos.Agregar(a);
            Verificar(a.Id > 0 && articulos.Listar().Single().Imagenes.Count == 12, "alta con doce imágenes sin límite fijo");
            Rechaza(() => marcas.EliminarLogico(marca.Id), "marca usada protegida");
            Rechaza(() => categorias.EliminarLogico(categoria.Id), "categoría usada protegida");
            a.Nombre="Producto editado"; a.Imagenes.RemoveAt(0); articulos.Modificar(a);
            Verificar(articulos.Listar().Single().Nombre == a.Nombre && articulos.Listar().Single().Imagenes.Count==11, "modificar artículo y quitar imagen");
            Sql(conexion, "ALTER TABLE IMAGENES ADD CONSTRAINT CK_Prueba CHECK (ImagenUrl <> 'https://fallo.test/imagen.png')");
            a.Nombre="No debe persistir"; a.Imagenes.Add(new Imagen { IdImagen="https://fallo.test/imagen.png" });
            Rechaza(() => articulos.Modificar(a), "fallo de imagen revierte operación");
            var guardado=articulos.Listar().Single();
            Verificar(guardado.Nombre=="Producto editado" && guardado.Imagenes.Count==11, "rollback conserva artículo e imágenes anteriores");
            a.Id=0;
            Rechaza(() => articulos.Agregar(a), "alta fallida se revierte");
            Verificar(articulos.Listar().Count==1, "sin artículo parcial tras fallo");
            // Comprobaciones de formularios sobre la base desechable.
            using (var principal=new frmPrincipal())
            {
                Evento(principal, "Form1_Load", principal, EventArgs.Empty);
                var buscar=Campo<TextBox>(principal,"txtBuscar"); var grilla=Campo<DataGridView>(principal,"dataGridView1");
                foreach (var texto in new[]{"cod-xyz","EDITADO","Marca editada","Categoría editada"})
                { buscar.Text=texto; Verificar(((List<Articulo>)grilla.DataSource!).Count==1,"búsqueda: "+texto); }
                buscar.Text="no coincide"; Verificar(((List<Articulo>)grilla.DataSource!).Count==0,"búsqueda sin resultados");
                buscar.Clear(); Verificar(((List<Articulo>)grilla.DataSource!).Count==1,"limpiar búsqueda");
                Captura(principal,"principal");
            }
            using (var alta=new frmArticuloAlta(guardado))
            {
                Evento(alta,"frmArticuloAlta_Load",alta,EventArgs.Empty);
                Verificar(Campo<TextBox>(alta,"txtNombre").Text==guardado.Nombre,"modificación precargada");
                Verificar(Campo<ListBox>(alta,"lstImagenes").Items.Count==11,"imágenes cargadas en editor");
                Evento(alta,"AgregarDireccion","https://example.com/nueva.png");
                Verificar(Campo<ListBox>(alta,"lstImagenes").Items.Count==12,"añadir otra imagen");
                Verificar(guardado.Imagenes.Count==11,"editar no altera el original al cancelar");
                Captura(alta,"articulo");
            }
            using (var f=new frmCategorias()) { f.Show(); Application.DoEvents(); Captura(f,"categorias"); f.Close(); }
            using (var f=new frmMarcas()) { f.Show(); Application.DoEvents(); Captura(f,"marcas"); f.Close(); }
            articulos.EliminarLogico(guardado.Id);
            Verificar(articulos.Listar().Count==0,"baja oculta artículo");
            Verificar(Convert.ToInt32(Valor(conexion,"SELECT COUNT(*) FROM ARTICULOS WHERE Activo=0"))==1 && Convert.ToInt32(Valor(conexion,"SELECT COUNT(*) FROM IMAGENES"))==11,"baja conserva registro e imágenes");
            marcas.EliminarLogico(marca.Id); categorias.EliminarLogico(categoria.Id);
            Verificar(marcas.Listar().Count==0 && categorias.Listar().Count==0,"baja lógica de catálogos");
            Rechaza(() => articulos.Agregar(new Articulo { Codigo="x",Nombre="x",Precio=1,Marca=marca,Categoria=categoria }),"no admite catálogos inactivos");
            Console.WriteLine($"PRUEBAS COMPLETADAS: {verificaciones}");
        }
        finally
        {
            SqlConnection.ClearAllPools();
            Sql(master,$"ALTER DATABASE [{nombre}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{nombre}]");
            Environment.SetEnvironmentVariable("CATALOGO_CONNECTION_STRING",null);
        }
    }
    static void Captura(Form form, string nombre)
    {
        if (!form.Visible) form.Show();
        Application.DoEvents();
        form.CreateControl();
        using var imagen=new Bitmap(form.Width,form.Height);
        form.DrawToBitmap(imagen,new Rectangle(0,0,form.Width,form.Height));
        string carpeta=Path.Combine(Path.GetTempPath(),"CatalogoQA"); Directory.CreateDirectory(carpeta);
        string archivo=Path.Combine(carpeta,nombre+".png"); imagen.Save(archivo); Console.WriteLine("CAPTURA: "+archivo);
    }
}
