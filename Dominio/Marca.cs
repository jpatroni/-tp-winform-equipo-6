namespace Dominio
{
    public class Marca
    {
        public override string ToString() => Descripcion;
        ///propiedades
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;


    }
}
