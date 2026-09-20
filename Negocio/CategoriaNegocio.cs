using System;
using System.Collections.Generic;
using System.Text;
using Dominio;

namespace TPWinForm_equipo_6.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using var datos = new AccesoDatos();
            datos.SetearConsulta("SELECT Id, descripcion FROM CATEGORIAS ORDER BY Descripcion");
            datos.EjecutarLectura();

            while (datos.Lector.Read())
            {
                Categoria categoria = new Categoria();
                categoria.Id = (int)datos.Lector["Id"];
                categoria.descripcion =
                    datos.Lector["descripcion"] as string ?? string.Empty;

                lista.Add(categoria);

            }
            return lista;
        }


    }
}
