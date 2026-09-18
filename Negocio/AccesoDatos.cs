using System.Data;
using Microsoft.Data.SqlClient;

namespace TPWinForm_equipo_6.Negocio
{
    public sealed class AccesoDatos : IDisposable
    {
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        private SqlDataReader? lector;

        public SqlDataReader Lector => lector
            ?? throw new InvalidOperationException("Primero ejecutá una lectura.");

        public AccesoDatos()
        {
            conexion = new SqlConnection(
                @"Server=.\SQLEXPRESS;Database=CATALOGO_P3_DB;Integrated Security=True;TrustServerCertificate=True;");
            comando = new SqlCommand { Connection = conexion };
        }

        public void SetearConsulta(string consulta)
        {
            CerrarConexion();
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
            comando.Parameters.Clear();
        }

        public void SetearParametro(string nombre, SqlDbType tipo, object? valor)
        {
            comando.Parameters.Add(nombre, tipo).Value = valor ?? DBNull.Value;
        }

        // La conexión queda abierta para poder recorrer las filas con Lector.Read().
        // Usar AccesoDatos dentro de un bloque using garantiza su cierre.
        public void EjecutarLectura()
        {
            CerrarConexion();
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch
            {
                CerrarConexion();
                throw;
            }
        }

        public int EjecutarAccion()
        {
            CerrarConexion();
            try
            {
                conexion.Open();
                return comando.ExecuteNonQuery();
            }
            finally
            {
                CerrarConexion();
            }
        }

        public object EjecutarEscalar()
        {
            CerrarConexion();
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            finally 
            {
                CerrarConexion();
            }
        }

        public void CerrarConexion()
        {
            lector?.Dispose();
            lector = null;
            conexion.Close();
        }

        public void Dispose()
        {
            CerrarConexion();
            comando.Dispose();
            conexion.Dispose();
        }


    }
}
