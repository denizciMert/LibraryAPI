namespace LibraryAPI.Entities.DTOs.AuthorDTO
{
    public class AuthorGet
    {
        public int? Id { get; set; }
        public string? AuthorName { get; set; } = string.Empty;
        public string? Biography { get; set; } = string.Empty;
        public short? DateOfBirth { get; set; }
        public short? DateOfDeath { get; set; }
        public List<string>? Books { get; set; } = [];
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
