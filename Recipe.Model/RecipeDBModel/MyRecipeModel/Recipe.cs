using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test_api.Recipe.Model.MyRecipeModel
{
    [Table("Recipe")]
    public partial class Recipe
    {
        [Key]
        [Column("RecipeID")]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        [Unicode(false)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "RecipeSID is required.")]
        [Column("RecipeSID")]
        [StringLength(50, ErrorMessage = "RecipeSID cannot be longer than 50 characters.")]
        [Unicode(false)]
        public string? RecipeSid { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Ingredients cannot be longer than 100 characters.")]
        [Unicode(false)]
        public string? Ingredients { get; set; }

        [StringLength(100, ErrorMessage = "Instructions cannot be longer than 100 characters.")]
        [Unicode(false)]
        public string? Instructions { get; set; }

        [Column(TypeName = "datetime")]
        [DataType(DataType.DateTime, ErrorMessage = "CookingTime must be a valid date/time.")]
        public DateTime? CookingTime { get; set; }

        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters.")]
        [Unicode(false)]
        public string? Category { get; set; }

        [Column(TypeName = "datetime")]
        [DataType(DataType.DateTime, ErrorMessage = "CreatedAt must be a valid date/time.")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "datetime")]
        [DataType(DataType.DateTime, ErrorMessage = "ModifiedAt must be a valid date/time.")]
        public DateTime? ModifiedAt { get; set; }

        [Range(1, 3, ErrorMessage = "Status must be either 3 (Inactive) or 1 (Active).")]
        public int? Status { get; set; }
    }
}
