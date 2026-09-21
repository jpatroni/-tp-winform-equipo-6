# Organización del proyecto

Abrir `TPWinForm_equipo-6.slnx` en Visual Studio. La solución contiene tres proyectos:

| Proyecto | Responsabilidad |
| --- | --- |
| Dominio | Representa los datos: Articulo, Marca, Categoria e Imagen. |
| Negocio | Consultas y operaciones del catálogo. Incluye AccesoDatos para centralizar la conexión y ejecución de SQL. |
| TPWinForm_equipo-6 | Interfaz WinForms: muestra datos, recibe las acciones del usuario y llama a Negocio. Es el proyecto de inicio. |

Referencias: WinForms utiliza Negocio y Dominio; Negocio utiliza Dominio. Dominio no depende de los otros proyectos.

El paquete Microsoft.Data.SqlClient pertenece a Negocio. Los formularios no deben ejecutar consultas SQL directamente.

Ejemplo de recorrido: Form1 llama a ArticuloNegocio.Listar(); este usa AccesoDatos para consultar SQL Server y devuelve objetos Articulo para mostrarlos en la grilla.

Se conserva el namespace TPWinForm_equipo_6.Negocio para no cambiar los imports de los formularios. AccesoDatos ahora pertenece a ese mismo namespace.

## Trabajo en equipo

- Agregar clases de negocio en el proyecto Negocio, y modelos en Dominio.
- Agregar formularios al proyecto WinForms.
- AccesoDatos está ahora en Negocio/AccesoDatos.cs; no crear otra copia en Datos.
- Mantener validaciones, manejo de errores y cierre de recursos en las operaciones.

Para verificar la solución: `dotnet build TPWinForm_equipo-6.slnx`.
