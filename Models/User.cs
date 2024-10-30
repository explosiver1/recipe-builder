using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization.Infrastructure;

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
        public List<Recipe> recipes { get; set; } = new List<Recipe>();
        public List<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>(); //If we change the nature of MealPlanner, we won't need this as a list
        public ShoppingList ShoppingList { get; set; } = new ShoppingList();
        public Pantry pantry { get; set; } = new Pantry();

        // Blank constructor initializing default values
        public User()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;

            // These aren't needed in database for User node but may be used in functions idk
            Cookbooks = new List<Cookbook>();
            MealPlanners = new List<MealPlanner>();
            ShoppingList = new ShoppingList();
        }//end Blank Constructor

        // Authtoken is used to pass username value
        public bool CreateUser(AuthToken at, string name, string email, string phone, string password)
        {
            bool userCreated = DBQueryModel.CreateUserNode(at.username, name, email, phone, password).Result;
            return userCreated;
        }

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

            if (ShoppingList.Items == null)
                ShoppingList.Items = new List<Ingredient>(); // Initialize if null

            ShoppingList.AddItem(ingredient);
            Console.WriteLine($"{ingredient.Name} has been added to the shopping list.");
        } // end AddToShoppingList

        // Add a cookbook with validation
        public void AddCookbook(Cookbook cookbook)
        {
            if (cookbook == null)
            {
                Console.WriteLine("Cookbook cannot be empty.");
                return;
            }

            Cookbooks.Add(cookbook);
            Console.WriteLine($"Cookbook '{cookbook.Title}' added to user.");
        }//end AddCookbook

        // Display the user's cookbooks
        public void DisplayCookbooks()
        {
            if (Cookbooks == null || Cookbooks.Count == 0)
            {
                Console.WriteLine("No cookbooks available.");
                return;
            }

            Console.WriteLine($"Cookbooks for {Username}:");
            foreach (var cookbook in Cookbooks)
            {
                Console.WriteLine($"- {cookbook.Title}");
            }
        } // end DisplayCookbooks

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
                Console.WriteLine($"- Scheduled Meals: {mealPlanner.ScheduledMeals.Count}");
            }
        }//end DisplayMealPlanners

        // Display the user's shopping list
        public void DisplayShoppingList()
        {
            if (ShoppingList.Items == null || ShoppingList.Items.Count == 0)
            {
                Console.WriteLine("Shopping list is empty.");
                return;
            }

            Console.WriteLine($"Shopping list for {Username}:");
            foreach (var item in ShoppingList.Items)
            {
                Console.WriteLine($"- {item.Name}");
            }
        } // end DisplayShoppingList
    }//end User
}//end namespace RecipeBuilder.Models
