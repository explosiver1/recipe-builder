using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeAddVM
    {
        // Uncomment if you need to track the user who is adding the recipe
        // public required string userName { get; set; }

        public string cookbookName { get; set; } = string.Empty;
        public Recipe recipe { get; set; } = new Recipe();
        public string TagsInput { get; set; } = string.Empty; // Comma-separated tags
        public List<IngredientDetail> IngredientsInput { get; set; } = new List<IngredientDetail>(); // One ingredient per line
        public string ServingSizeInput { get; set; } = string.Empty; // e.g., "cup, 2"
        public string EquipmentInput { get; set; } = string.Empty; // Comma-separated tools/equipment
        public List<string> InstructionsInput { get; set; } = new List<string>(); // One instruction per line


        public string? msg { get; set; }
    }
}
