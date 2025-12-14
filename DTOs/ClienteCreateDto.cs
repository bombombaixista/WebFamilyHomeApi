namespace WebFamilyHomeApi.DTOs
{
    public class ClienteCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int? GrupoId { get; set; }
    }
}
