# Catálogo de artículos - equipo 6

Aplicación Windows Forms con proyectos separados de interfaz, Dominio y Negocio.
Requiere Windows, .NET SDK 10 y SQL Server con la base original `CATALOGO_P3_DB`.

## Preparación

1. Actualizar la rama `auxiliar` conservando previamente cualquier trabajo local.
2. Abrir `AgregarActivo.sql` en SQL Server Management Studio y ejecutarlo contra
   la instancia que contiene la base. Agrega `Activo` en ARTICULOS, MARCAS y
   CATEGORIAS si falta; no borra datos ni reactiva registros dados de baja.
3. Abrir `TPWinForm_equipo-6.slnx` en Visual Studio y ejecutar el proyecto WinForms.

La conexión predeterminada usa `.\SQLEXPRESS`, autenticación integrada y
`CATALOGO_P3_DB`. Para otra instancia se puede definir la variable de entorno
`CATALOGO_CONNECTION_STRING` antes de abrir la aplicación. No guardar contraseñas
en el repositorio.

```powershell
dotnet build TPWinForm_equipo-6.slnx
dotnet run --project TPWinForm_equipo-6.csproj
```

## Funciones

- Listado de artículos activos y búsqueda por nombre, código, marca o categoría,
  sin distinguir mayúsculas. Vaciar la búsqueda muestra todos los activos.
- Alta y modificación con validación de campos, precio y longitudes de la base.
- Baja lógica con confirmación; conserva el artículo y sus imágenes.
- Detalle con navegación de imágenes, archivos locales y URLs.
- Administración de marcas y categorías: alta, modificación y baja lógica.
  No permite desactivar registros utilizados por artículos activos. Los artículos
  inactivos conservan sus referencias y no impiden desactivar el catálogo.
- Nombres de marcas/categorías vacíos o duplicados activos se rechazan. No se
  eliminan automáticamente duplicados históricos que ya existían en la base.
- Varias imágenes por artículo, sin una cantidad máxima fijada por la aplicación:
  escribir URL y pulsar "Añadir dirección", o elegir varios archivos. Seleccionar
  una fila para previsualizarla; "Quitar seleccionada" la excluye del guardado.
  Una dirección escrita pendiente se incorpora al presionar Guardar.
- Artículo e imágenes se persisten en una única transacción. Si falla una parte,
  se revierte todo el cambio de base de datos.

Los archivos locales se copian a `%LOCALAPPDATA%\CatalogoEquipo6\Imagenes`.
Esas rutas pertenecen a esa computadora: para compartir imágenes entre equipos,
usar URLs accesibles desde ambos. Las descargas del editor se cancelan al cambiar
de imagen o cerrar, tienen tiempo límite y no bloquean la interfaz.

## Pruebas automatizadas

```powershell
dotnet run --project Pruebas/Pruebas.csproj
```

El ejecutable crea una base temporal `CatalogoPruebas_<identificador>` en
`.\SQLEXPRESS` y la elimina al finalizar. Requiere permisos para crear bases.
No usa ni modifica los artículos de `CATALOGO_P3_DB`. Muestra brevemente formularios
para comprobar su carga y guarda capturas en `%TEMP%\CatalogoQA`.

Comprueba 27 condiciones, incluyendo validaciones, duplicados, bajas lógicas,
protección de catálogos en uso, filtros, edición sin alterar el original,
doce imágenes y rollback ante fallos de alta/modificación.

Para la entrega, comprobar además el recorrido manual: agregar un artículo con
URL y archivo local, ver su detalle, editarlo, cancelar otra edición, buscarlo,
darle de baja y verificar `Activo = 0`. Probar los botones de marcas y categorías.
Los resultados automatizados no garantizan la disponibilidad de URLs externas.
