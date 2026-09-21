using System.Data;
using Microsoft.Data.SqlClient;

namespace TPWinForm_equipo_6.Negocio
{
    public sealed class AccesoDatos : IDisposable
    {
        private readonly SqlConnection conexion;
        private readonly SqlCommand comando;
        private SqlDataReader? lector;
        private SqlTransaction? transaccion;
        public SqlDataReader Lector => lector ?? throw new InvalidOperationException("Primero ejecutá una lectura.");

        public AccesoDatos()
        {
            conexion = new SqlConnection(Environment.GetEnvironmentVariable("CATALOGO_CONNECTION_STRING")
                ?? @"Server=.\SQLEXPRESS;Database=CATALOGO_P3_DB;Integrated Security=True;TrustServerCertificate=True;");
            comando = new SqlCommand { Connection = conexion };
        }
        private void Preparar()
        {
            lector?.Dispose();
            lector = null;
            if (conexion.State != ConnectionState.Open) conexion.Open();
        }
        public void IniciarTransaccion()
        {
            Preparar();
            transaccion = conexion.BeginTransaction();
            comando.Transaction = transaccion;
        }
        public void ConfirmarTransaccion()
        {
            lector?.Dispose();
            lector = null;
            transaccion!.Commit();
            transaccion.Dispose();
            transaccion = null;
            comando.Transaction = null;
        }
        public void SetearConsulta(string consulta)
        {
            lector?.Dispose();
            lector = null;
            comando.CommandText = consulta;
            comando.Parameters.Clear();
        }
        public void SetearParametro(string nombre, SqlDbType tipo, object? valor) =>
            comando.Parameters.Add(nombre, tipo).Value = valor ?? DBNull.Value;
        public void EjecutarLectura() { Preparar(); lector = comando.ExecuteReader(); }
        public int EjecutarAccion() { Preparar(); return comando.ExecuteNonQuery(); }
        public object EjecutarEscalar() { Preparar(); return comando.ExecuteScalar(); }
        public void CerrarConexion()
        {
            lector?.Dispose();
            lector = null;
            // Dispose revierte una transacción que no llegó a confirmarse.
            transaccion?.Dispose();
            transaccion = null;
            comando.Transaction = null;
            conexion.Close();
        }
        public void Dispose() { CerrarConexion(); comando.Dispose(); conexion.Dispose(); }
    }
}
