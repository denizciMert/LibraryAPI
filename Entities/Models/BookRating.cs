using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
	public class BookRating
	{
		public int RatedBookId { get; set; }

		[ForeignKey(nameof(RatedBookId))]
		public Book? Book { get; set; }

		public string RaterMemberId { get; set; }

        [ForeignKey(nameof(RaterMemberId))]
        public Member? Member { get; set; }

		public float Rate { get; set; }
	}
}

