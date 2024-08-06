using LibraryAPI.Entities.Enums;

namespace LibraryAPI.Entities.Models
{
    // Base entity class that provides common properties for all entities.
    public abstract class AbstractEntity
    {
        // Unique identifier for the entity.
        public int Id { get; set; }

        // The date and time when the entity was created. Defaults to the current date and time.
        public DateTime CreationDateLog { get; set; } = DateTime.Now;

        // The date and time when the entity was last updated. Nullable, as it might not be updated yet.
        public DateTime? UpdateDateLog { get; set; }

        // The date and time when the entity was deleted. Nullable, as it might not be deleted.
        public DateTime? DeleteDateLog { get; set; }

        // The state of the entity, indicating its status (e.g., created, updated, deleted). Defaults to "Eklendi" (Added).
        public State State { get; set; } = State.Eklendi;
    }
}