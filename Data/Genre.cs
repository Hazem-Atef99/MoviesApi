﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Data
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Byte Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
