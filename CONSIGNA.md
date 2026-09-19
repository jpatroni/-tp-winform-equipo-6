# TP: gestión de catálogo de artículos

Base de referencia proporcionada por el usuario el 19/09/2026.

## Alcance solicitado

Aplicación de escritorio genérica para administrar artículos de cualquier comercio.
La información se persiste en la base de datos existente. El consumo posterior
desde webs, e-commerce, aplicaciones móviles o revistas es contexto y queda fuera
del desarrollo.

Funcionalidades requeridas:
- Listar artículos.
- Buscar artículos por distintos criterios.
- Agregar, modificar y eliminar artículos.
- Ver el detalle de un artículo.
- Administrar marcas y categorías.
- Gestionar múltiples imágenes por artículo, sin un límite fijo de cantidad.

Datos mínimos del artículo: código, nombre, descripción, marca seleccionable de
una lista desplegable, categoría seleccionable de una lista desplegable, imagen
y precio.

## Material de clase

Archivos de referencia en la carpeta Downloads del usuario:
- I Conexión a la Base de Datos .docx (1).pdf
- II Acceso a Datos .docx (1).pdf
- III ABM contra la DB.docx (1).pdf

Los PDF son material didáctico; la consigna anterior define los requisitos del TP.
Sus ejemplos de Pokémon no agregan requisitos al catálogo.

Base técnica: Windows Forms, clases de dominio, capa de negocio y acceso a SQL
Server con ADO.NET. AccesoDatos centraliza conexión, comandos y lector. Los
formularios llaman a negocio; negocio usa AccesoDatos. Cerrar los recursos tras
su uso y utilizar parámetros SQL para los valores ingresados.

## Revisión inicial del código (19/09/2026)

Esta revisión es estática; no acredita funcionamiento probado contra la DB.

- Listado: implementado, con recuperación de imágenes relacionadas.
- Detalle: implementado, con navegación de múltiples imágenes.
- Alta: parcial. Carga marcas, pero falta cargar categorías y gestionar imágenes
  desde el formulario; también falta actualizar el listado tras guardar.
- Modificación: existe el método de negocio, pero el evento del botón está vacío.
- Eliminación y búsqueda por criterios: pendientes de implementación.
- Marcas: existe listado; falta su administración.
- Categorías: existe modelo; falta negocio e interfaz de administración.
- Guardado de artículos e imágenes: revisar cierre de recursos y agregar una
  transacción para evitar guardados parciales.
- Pendiente verificar el esquema real de la DB, validaciones y funcionamiento
  integral antes de considerar terminada cada funcionalidad.

Orden propuesto: completar alta y modificación con imágenes; implementar baja y
búsqueda; administrar marcas y categorías; verificar todos los flujos contra DB.
