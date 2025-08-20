using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recipe.Model.ResponseModel
{
    public class RecipeResponseModel
    {
        [StringLength(100)]
        [JsonProperty("recipesid")]
        public string? RecipeSID { get; set; }

        [Required]
        [StringLength(100)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [StringLength(100)]
        [JsonProperty("ingredients")]
        public string? Ingredients { get; set; }

        [StringLength(100)]
        [JsonProperty("instructions")]
        public string? Instructions { get; set; }

        [StringLength(100)]
        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }

        [Column(TypeName = "datetime")]
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "datetime")]
        [JsonProperty("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }


    }
}
