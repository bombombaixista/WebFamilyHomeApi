namespace WebFamilyHomeApi.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<Cliente>? Clientes { get; set; }
    }
}
