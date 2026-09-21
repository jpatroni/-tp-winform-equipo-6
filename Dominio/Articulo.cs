using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public Marca Marca { get; set; } = new Marca();
        public Categoria Categoria { get; set; } = new Categoria();
        public decimal Precio { get; set; }
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();


    }
}

