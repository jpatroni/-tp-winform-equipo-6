using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Categoria
    {
        public override string ToString() => descripcion;
        public int Id { get; set; }
        public string descripcion { get; set; } = string.Empty;



    }
}

