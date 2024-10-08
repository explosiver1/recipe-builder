namespace RecipeBuilder.Models
{
    public class User
    {
        // Attributes
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        // User can have multiple of each of these
        public List<Cookbook> Cookbooks { get; set; } = new List<Cookbook>();
        public List<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>();
        public ShoppingList ShoppingList { get; set; } = new ShoppingList();

        // Constructor
        public User(int userId, string username, string email, string password)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Password = password;
        }//end User

        // Methods
        // Create new cookbook and add to user's list of cookbooks
        public void CreateCookbook(string title)
        {
            var newCookbook = new Cookbook
            {
                CookbookId = Cookbooks.Count + 1, // Simple ID incrementer
                Title = title
            };
            Cookbooks.Add(newCookbook);
        }//end CreateCookbook

        // Login process
        public bool Login(string inputUsername, string inputPassword)
        {
            if (Username == inputUsername && Password == inputPassword)
            {
                Console.WriteLine("Login successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed. Incorrect username or password.");
                return false;
            }
        }//end Login

        // Logout process
        public void Logout()
        {
            Console.WriteLine($"{Username} logged out.");
        }

        // Join a group
        public void JoinGroup(Group group)
        {
            group.AddMember(this); // 'this' current user object
            Console.WriteLine($"{Username} joined the group {group.Name}.");
        }

        // Add recipe to meal planner
        public void AddToMealPlanner(MealPlanner mealPlanner, Recipe recipe)
        {
            mealPlanner.ScheduleMeal(new MealSet
            {
                Name = recipe.Name,
                Recipes = new List<Recipe> { recipe }
            });//end ScheduleMeal

            Console.WriteLine($"{recipe.Name} has been added to the meal planner.");
        }//end AddToMealPlanner

        // Add an ingredient to the user's shopping list
        public void AddToShoppingList(Ingredient ingredient)
        {
            ShoppingList.AddItem(ingredient);
            Console.WriteLine($"{ingredient.Name} has been added to the shopping list.");
        }//end AddToShoppingList

        // Display cookbooks for this user
        public void DisplayCookbooks()
        {
            Console.WriteLine($"Cookbooks for {Username}:");
            foreach (var cookbook in Cookbooks)
            {
                Console.WriteLine($"- {cookbook.Title}");
            }//end foreach
        }//end DisplayCookbooks

        // Display meal planners for this user
        public void DisplayMealPlanners()
        {
            Console.WriteLine($"Meal planners for {Username}:");
            foreach (var mealPlanner in MealPlanners)
            {
                Console.WriteLine($"- Meal Planner ID: {mealPlanner.MealPlannerId}, Scheduled Meals: {mealPlanner.ScheduledMeals.Count}");
            }//end foreach
        }//end DisplayMealPlanners

        // Display the shopping list
        public void DisplayShoppingList()
        {
            Console.WriteLine($"Shopping list for {Username}:");
            foreach (var item in ShoppingList.Items)
            {
                Console.WriteLine($"- {item.Name}");
            }//end foreach
        }//end DisplayShoppingList
    }//end User

}//end namespace RecipeBuilder.Models