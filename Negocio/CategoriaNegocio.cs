using Dominio;
using System;
using System.Collections.Generic;
using System.Data;

namespace TPWinForm_equipo_6.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using var datos = new AccesoDatos();

            datos.SetearConsulta(
                "SELECT Id, Descripcion FROM CATEGORIAS ORDER BY Descripcion");

            datos.EjecutarLectura();

            while (datos.Lector.Read())
            {
                Categoria categoria = new Categoria();

                categoria.Id = (int)datos.Lector["Id"];
                categoria.descripcion =
                    datos.Lector["Descripcion"].ToString();

                lista.Add(categoria);
            }

            return lista;
        }

        public void Agregar(string descripcion)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta(
                "INSERT INTO CATEGORIAS (Descripcion) VALUES (@descripcion)");

            datos.SetearParametro(
                "@descripcion",
                SqlDbType.VarChar,
                descripcion);

            datos.EjecutarAccion();
        }

        public void Modificar(int id, string descripcion)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta(
                "UPDATE CATEGORIAS SET Descripcion = @descripcion WHERE Id = @id");

            datos.SetearParametro(
                "@descripcion",
                SqlDbType.VarChar,
                descripcion);

            datos.SetearParametro(
                "@id",
                SqlDbType.Int,
                id);

            datos.EjecutarAccion();
        }

        public void Eliminar(int id)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta(
                "DELETE FROM CATEGORIAS WHERE Id = @id");

            datos.SetearParametro(
                "@id",
                SqlDbType.Int,
                id);

            datos.EjecutarAccion();
        }

        public bool EstaEnUso(int id)
        {
            using var datos = new AccesoDatos();

            datos.SetearConsulta(
                "SELECT TOP 1 Id FROM ARTICULOS WHERE IdCategoria = @id");

            datos.SetearParametro(
                "@id",
                SqlDbType.Int,
                id);

            datos.EjecutarLectura();

            return datos.Lector.Read();
        }
    }
}


