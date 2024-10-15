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
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            UserId = userId;
            Username = username;
            Email = email;
            Password = password;
        }//end User

        // Placeholder: Method for future database integration when creating a user
        public void CreateUser()
        {
            bool useDatabase = false;  // Change this when Neo4j is ready

            if (useDatabase)
            {
                // Future Neo4j code will go here when the database team finishes
                Console.WriteLine("Creating user in the Neo4j database...");
            }//end if
            else
            {
                // For now, simulate in-memory logic or any other logic
                Console.WriteLine($"User {Username} created in-memory.");
            }//end else
        }//end CreateUser

        // Methods
        // Create new cookbook and add to user's list of cookbooks
        public void CreateCookbook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Cookbook title cannot be empty.");
                return;
            }//end if

            var newCookbook = new Cookbook(Cookbooks.Count + 1, title);
            {
                Cookbooks.Add(newCookbook);
                Console.WriteLine($"Cookbook '{title}' created and added to user.");
            };//end newCookbook
            Cookbooks.Add(newCookbook);
        }//end CreateCookbook

        // Login process
        public bool Login(string inputUsername, string inputPassword)
        {
            if (string.IsNullOrWhiteSpace(inputUsername) || string.IsNullOrWhiteSpace(inputPassword))
            {
                Console.WriteLine("Username or password cannot be empty.");
                return false;
            }//end if

            if (Username == inputUsername && Password == inputPassword)
            {
                Console.WriteLine("Login successful!");
                return true;
            }//end if
            else
            {
                Console.WriteLine("Login failed. Incorrect username or password.");
                return false;
            }//end else
        }//end Login

        // Logout process
        public void Logout()
        {
            Console.WriteLine($"{Username} logged out.");
        }

        // Join a group
        public void JoinGroup(Group group)
        {
            if (group == null)
            {
                Console.WriteLine("Cannot join an empty group.");
                return;
            }//end if

            group.AddMember(this); // 'this' current user object
            Console.WriteLine($"{Username} joined the group {group.Name}.");
        }//end JoinGroup

        // Add recipe to meal planner
        public void AddToMealPlanner(MealPlanner mealPlanner, Recipe recipe)
        {
            if (mealPlanner == null || recipe == null)
            {
                Console.WriteLine("Meal planner or recipe cannot be null.");
                return;
            }//end if

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
            if (ingredient == null)
            {
                Console.WriteLine("Ingredient cannot be empty.");
                return;
            }//end if

            ShoppingList.AddItem(ingredient);
            Console.WriteLine($"{ingredient.Name} has been added to the shopping list.");
        }//end AddToShoppingList

        // Display cookbooks for this user
        public void DisplayCookbooks()
        {
            if (Cookbooks.Count == 0)
            {
                Console.WriteLine("No cookbooks available.");
                return;
            }//end if

            Console.WriteLine($"Cookbooks for {Username}:");
            foreach (var cookbook in Cookbooks)
            {
                Console.WriteLine($"- {cookbook.Title}");
            }//end foreach
        }//end DisplayCookbooks

        // Display meal planners for this user
        public void DisplayMealPlanners()
        {
            if (MealPlanners.Count == 0)
            {
                Console.WriteLine("No meal planners available.");
                return;
            }//end if

            Console.WriteLine($"Meal planners for {Username}:");
            foreach (var mealPlanner in MealPlanners)
            {
                Console.WriteLine($"- Meal Planner ID: {mealPlanner.MealPlannerId}, Scheduled Meals: {mealPlanner.ScheduledMeals.Count}");
            }//end foreach
        }//end DisplayMealPlanners

        // Display the shopping list
        public void DisplayShoppingList()
        {
            if (ShoppingList.Items.Count == 0)
            {
                Console.WriteLine("Shopping list is empty.");
                return;
            }//end if

            Console.WriteLine($"Shopping list for {Username}:");
            foreach (var item in ShoppingList.Items)
            {
                Console.WriteLine($"- {item.Name}");
            }//end foreach
        }//end DisplayShoppingList
    }//end User

}//end namespace RecipeBuilder.Models