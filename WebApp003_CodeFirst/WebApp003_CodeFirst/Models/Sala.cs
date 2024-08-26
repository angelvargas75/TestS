using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp003_CodeFirst.Models
{
    public class Sala
    {
        public int SalaId { get; set; }
        [Required]
        [StringLength(maximumLength:20)]
        public string Nombre { get; set; }
        [Required]
        public int Capacidad { get; set; }
        public string Recursos { get; set; }
        [StringLength(maximumLength:50)]
        public string Comentarios { get; set; }
    }
}