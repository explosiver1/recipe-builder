using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;
using System.Linq;

namespace RecipeBuilder.ViewModels
{
    public class RecipeEditVM
    {
        public Recipe recipe { get; set; } = new Recipe(); // Initialize an empty recipe

        public string TagsInput { get; set; }
        public List<IngredientDetail> IngredientsInput { get; set; }
        public string ServingSizeInput { get; set; }
        public string EquipmentInput { get; set; }
        public List<string> InstructionsInput { get; set; }
        public string? msg { get; set; }

        // Constructor to initialize Inputs for easier editing
        public RecipeEditVM()
        {
            TagsInput = recipe.Tags != null ? string.Join(", ", recipe.Tags) : string.Empty;
            IngredientsInput = recipe.Ingredients;
            ServingSizeInput = recipe.servingSize != null
                ? string.Empty//string.Join(", ", recipe.servingSize.Select(kv => $"{kv.Key}, {kv.Value}"))
                : string.Empty;
            EquipmentInput = recipe.Equipment != null ? string.Join(", ", recipe.Equipment) : string.Empty;
            InstructionsInput = recipe.Instructions;
        }
    }
}
