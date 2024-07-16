using LibraryAPI.Entities.Enums;

namespace LibraryAPI.Entities.Models
{
    public abstract class AbstractEntity
    {
        public int Id { get; set; }

        public DateTime CreationDateLog { get; set; } = DateTime.Now;

        public DateTime? UpdateDateLog { get; set; }

        public DateTime? DeleteDateLog { get; set; }

        public State State { get; set; } = State.Eklendi;
    }
}
