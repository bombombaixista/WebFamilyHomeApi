namespace WebFamilyHomeApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Aqui vai o HASH
        public string Senha { get; set; } = string.Empty;

        public int? GrupoId { get; set; }
        public Grupo? Grupo { get; set; }
    }
}
