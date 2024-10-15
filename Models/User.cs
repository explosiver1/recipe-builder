using System;
using System.Collections.Generic;

namespace RecipeBuilder.Models
{
    public class User
    {
        // Attributes
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // User can have multiple of each of these
        public List<Cookbook> Cookbooks { get; set; } = new List<Cookbook>();
        public List<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>();
        public ShoppingList ShoppingList { get; set; } = new ShoppingList();

        // Blank constructor initializing default values
        public User()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Cookbooks = new List<Cookbook>();
            MealPlanners = new List<MealPlanner>();
            ShoppingList = new ShoppingList();
        }//end Blank Constructor

        // Placeholder: Method for future database integration when creating a user
        public void CreateUser()
        {
            bool useDatabase = false;  // Change this when Neo4j is ready

            if (useDatabase)
            {
                Console.WriteLine("Creating user in the Neo4j database...");
            }
            else
            {
                Console.WriteLine($"User {Username} created in-memory.");
            }
        }//end CreateUser

        // Create a new cookbook and add it to the user's list
        public void CreateCookbook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Cookbook title cannot be empty.");
                return;
            }

            var newCookbook = new Cookbook { Title = title };
            Cookbooks.Add(newCookbook);
            Console.WriteLine($"Cookbook '{title}' created and added to user.");
        }//end CreateCookbook

        // Login process
        public bool Login(string inputUsername, string inputPassword)
        {
            if (string.IsNullOrWhiteSpace(inputUsername) || string.IsNullOrWhiteSpace(inputPassword))
            {
                Console.WriteLine("Username or password cannot be empty.");
                return false;
            }

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
        }//end Logout

        // Join a group
        public void JoinGroup(Group group)
        {
            if (group == null)
            {
                Console.WriteLine("Cannot join an empty group.");
                return;
            }

            group.AddMember(this);
            Console.WriteLine($"{Username} joined the group {group.Name}.");
        }//end JoinGroup

        // Add a recipe to a meal planner
        public void AddToMealPlanner(MealPlanner mealPlanner, Recipe recipe)
        {
            if (mealPlanner == null || recipe == null)
            {
                Console.WriteLine("Meal planner or recipe cannot be null.");
                return;
            }

            mealPlanner.ScheduleMeal(new MealSet
            {
                Name = recipe.Name,
                Recipes = new List<Recipe> { recipe }
            });

            Console.WriteLine($"{recipe.Name} has been added to the meal planner.");
        }//end AddToMealPlanner

        // Add an ingredient to the user's shopping list
        public void AddToShoppingList(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                Console.WriteLine("Ingredient cannot be empty.");
                return;
            }

            ShoppingList.AddItem(ingredient);
            Console.WriteLine($"{ingredient.Name} has been added to the shopping list.");
        }//end AddToShoppingList

        // Display the user's cookbooks
        public void DisplayCookbooks()
        {
            if (Cookbooks.Count == 0)
            {
                Console.WriteLine("No cookbooks available.");
                return;
            }

            Console.WriteLine($"Cookbooks for {Username}:");
            foreach (var cookbook in Cookbooks)
            {
                Console.WriteLine($"- {cookbook.Title}");
            }
        }//end DisplayCookbooks

        // Display the user's meal planners
        public void DisplayMealPlanners()
        {
            if (MealPlanners.Count == 0)
            {
                Console.WriteLine("No meal planners available.");
                return;
            }

            Console.WriteLine($"Meal planners for {Username}:");
            foreach (var mealPlanner in MealPlanners)
            {
                Console.WriteLine($"- Meal Planner ID: {mealPlanner.MealPlannerId}, Scheduled Meals: {mealPlanner.ScheduledMeals.Count}");
            }
        }//end DisplayMealPlanners

        // Display the user's shopping list
        public void DisplayShoppingList()
        {
            if (ShoppingList.Items.Count == 0)
            {
                Console.WriteLine("Shopping list is empty.");
                return;
            }

            Console.WriteLine($"Shopping list for {Username}:");
            foreach (var item in ShoppingList.Items)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }//end DisplayShoppingList
    }//end User
}//end namespace RecipeBuilder.Models