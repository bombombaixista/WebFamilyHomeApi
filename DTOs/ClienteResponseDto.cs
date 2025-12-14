namespace WebFamilyHomeApi.DTOs
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int? GrupoId { get; set; }
        public string? GrupoNome { get; set; }
    }
}
