    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Recipe.Model.RequestModel
    {
        public class RecipeRequestModel
        {
        [StringLength(100)]
        public string? RecipeSID { get; set; }

            [Required]
            [StringLength(100)]
            public string Title { get; set; }

            [StringLength(100)]
            public string? Ingredients { get; set; }

            [StringLength(100)]
            public string? Instructions { get; set; }

            [StringLength(100)]
            public string? Category { get; set; }

            //[Column(TypeName = "datetime")]
            //public DateTime? CreatedAt { get; set; }

            //[Column(TypeName = "datetime")]
            //public DateTime? ModifiedAt { get; set; }

            //public int? Status { get; set; }
        }
    }
