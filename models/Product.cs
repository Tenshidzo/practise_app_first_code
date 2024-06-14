using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practise_app_first_code.models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя продукта обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Имя продукта должно содержать не более 100 символов")]
        public string Name { get; set; }

        [Column(TypeName = "decimal")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }
    }
}
