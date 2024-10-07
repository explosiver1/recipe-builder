namespace RecipeBuilder.Models
{
    public class Calculation
    {
        public float ConvertUnit(float value, string fromUnit, string toUnit)
        {
            return value;
        }

        public float AddQuantities(float quantity1, float quantity2)
        {
            return quantity1 + quantity2;
        }

        public float SubtractQuantities(float quantity1, float quantity2)
        {
            return quantity1 - quantity2;
        }

        public float CalculateNutrition(List<Ingredient> ingredients)
        {
            return 0f;
        }

        public List<Ingredient> ScaleRecipe(List<Ingredient> ingredients, float scaleFactor)
        {
            return ingredients;
        }
    }
}