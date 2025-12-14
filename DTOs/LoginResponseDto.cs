namespace WebFamilyHomeApi.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int? GrupoId { get; set; }
        public string? GrupoNome { get; set; }
    }
}
