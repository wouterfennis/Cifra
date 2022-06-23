﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Database.Schema
{
    /// <summary>
    /// The Class entity
    /// </summary>
    public class Class
    {
        [Key]
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The Name of the Class
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public virtual ICollection<Student> Students { get; set; }
    }
}