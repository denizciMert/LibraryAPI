namespace LibraryAPI.Entities.DTOs.PenaltyTypeDTO
{
    public class PenaltyTypeGet
    {
        public int? Id { get; set; }
        public string? PenaltyNae { get; set; } = string.Empty;
        public float? AmountToPay { get; set; }
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
