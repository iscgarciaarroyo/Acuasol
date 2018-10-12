namespace Acuasol.Backend.Models
{
    using Dominio;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Acuasol.Dominio.Usuario> Usuarios { get; set; }
    }
}