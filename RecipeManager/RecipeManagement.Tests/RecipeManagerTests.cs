using Xunit;

using System.Collections.Generic;
// using System.Collections.IEnumerable;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    [Fact] // test01
    public void AddRecipe_ValidRecipe() {
        var initialRecipes = new List<Recipe>();
        var manager = new RecipeManager(initialRecipes);
        var newRecipe = new Recipe {Id = 1, Title = "Chicken"};

        bool result = manager.AddRecipe(newRecipe);

        Assert.True(result);
    }

    [Fact]
    public void AddRecipe_InvalidRecipe() {
        var initialRecipes = new List<Recipe>{new Recipe {Id = 1, Title = "Chicken"}};
        var manager = new RecipeManager(initialRecipes);
        var duplicateRecipe = new Recipe{Id = 1, Title = "chicken"};

        Assert.Throws<ArgumentException>(() => manager.AddRecipe(duplicateRecipe));
    }

    [Fact] // test02
    public void FindRecipe_TrueValue(){
        var initialRecipes = new List<Recipe>{new Recipe {Id = 1, Title = "Chicken"}};
        var manager = new RecipeManager(initialRecipes);

        var result = manager.FindRecipe(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Chicken", result.Title);
    }

    [Fact]

    public void FindRecipe_NullValue(){
        var initialRecipes = new List<Recipe>{new Recipe {Id = 1, Title = "Chicken"}};
        var manager = new RecipeManager(initialRecipes);

        var result = manager.FindRecipe(2);

        Assert.Null(result);
    }

    // test 03
    [Fact]

    public void RemoveRecipe_Exception() {
        var initialRecipes = new List<Recipe>{new Recipe
            {Id = 1, Title = "Chicken"}
        };
        var manager = new RecipeManager(initialRecipes);

        Assert.Throws<NotImplementedException>(() => manager.RemoveRecipe(2));
    }

    [Fact]
    public void RemoveRecipe_TrueValue(){
        var initialRecipes = new List<Recipe>{new Recipe {Id = 1, Title = "Chicken"}};
        var manager = new RecipeManager(initialRecipes);

        var result = manager.RemoveRecipe(1);
        Assert.True(result);
    }

    // test 04
    [Fact]
    public void AddIngredientsSL_TrueVal() {
        var recipe = new Recipe {Id = 1, Title = "Pasta", Ingredients = new List<string> {"Pasta", "Tomato", "Chicken", "Mayonnaise"}};

        var manager = new RecipeManager(new List<Recipe> {recipe});

        int recipeID = manager.AddIngredientsToShoppingList(1);

        Assert.Equal(4, recipeID);
    }

    [Fact]
    public void AddIngredientSL_FalseVal(){
        var manager = new RecipeManager(new List<Recipe>());

        int recipeID = manager.AddIngredientsToShoppingList(11);

        Assert.Equal(0, recipeID);
    }

    // test 05
    [Fact]
    public void ShoppingList_ReadOnly(){
        var recipe = new Recipe {
            Id = 1,
            Title = "Chopstick Noodles",
            Ingredients = {"Noodles", "Ramen", "Eggs"}
        };

        var manager = new RecipeManager(new List<Recipe> {recipe});
        
        Assert.Empty(manager.GetShoppingList());

        // inserting items in the shopping list
        manager.AddIngredientsToShoppingList(1);
        var shoppingList = manager.GetShoppingList();

        Assert.Equal(3, shoppingList.Count);
        Assert.NotNull(manager.GetShoppingList());
    }

    [Fact] // test 06
    public void ShoppingListClear_outputEmpty() {
        var recipe = new Recipe {
            
            Id = 1, Title = "Chicken", Ingredients = new List<string>{"Soup", "Salsa", "Corn"}
            
            };
        
        var manager = new RecipeManager(new List<Recipe>{recipe});
        manager.AddIngredientsToShoppingList(1);
        manager.ClearShoppingList();
        var shoppingList = manager.GetShoppingList();

        Assert.Empty(shoppingList);
    }

    // test 07 
    [Fact]

    public void AddRecipeCookingPlan_TrueVal(){
        var recipe = new List<Recipe>{
            new Recipe {
                Id = 1,
                Title = "MasalaSoup",
                Ingredients = new List<string>{"Soup", "Chicken", "MaggieMasala"}
            }, 
            new Recipe {
                Id = 2,
                Title = "Soyachunk",
                Ingredients = new List<string>{"MaggieMasala", "Wings", "Soyabeans"}
            }
        };

        var manager = new RecipeManager(recipe);

        Assert.True(manager.AddRecipeToCookingPlan(1));
        Assert.True(manager.AddRecipeToCookingPlan(2));

        Assert.Equal(2, manager.CookingPlanCount);
    }

    [Fact]
    public void AddRecipeToCookingPlan_FalseVal(){
        var recipe = new List<Recipe>{
            new Recipe {
                Id = 1,
                Title = "MasalaSoup",
                Ingredients = new List<string>{"Soup", "Chicken", "MaggieMasala"}
            }, 
            new Recipe {
                Id = 2,
                Title = "Soyachunk",
                Ingredients = new List<string>{"MaggieMasala", "Wings", "Soyabeans"}
            }
        };

        var manager = new RecipeManager(recipe);

        Assert.True(manager.AddRecipeToCookingPlan(1));
        Assert.False(manager.AddRecipeToCookingPlan(1));

        Assert.Equal(1, manager.CookingPlanCount);
    }

    // test 08
    [Fact]
    public void RemoveRecipeFromCookingPlan_FalseVal(){
        var recipe = new Recipe {
                Id = 1,
                Title = "MasalaSoup",
                Ingredients = new List<string>{"Soup", "Chicken", "MaggieMasala"}
            };

        var manager = new RecipeManager(new List<Recipe>{recipe});

        var cookingPlan = manager.RemoveRecipeFromCookingPlan(2);
        Assert.False(cookingPlan);
    }

    [Fact]
    public void RemoveRecipeFromCookingPlan_TrueVal(){
        var recipe = new Recipe {
                Id = 1,
                Title = "MasalaSoup",
                Ingredients = new List<string>{"Soup", "Chicken", "MaggieMasala"}
            };

        var manager = new RecipeManager(new List<Recipe>{recipe});
        manager.AddRecipeToCookingPlan(1);

        var cookingPlan = manager.RemoveRecipeFromCookingPlan(1);
        Assert.True(cookingPlan);
        Assert.Equal(0, manager.CookingPlanCount);
    }

    // test 09
    [Fact]
    public void RestoreRemoveRecipe_TrueVal(){
        var recipe = new Recipe {
                Id = 1,
                Title = "MasalaSoup",
                Ingredients = new List<string>{"Soup", "Chicken", "MaggieMasala"}
            };

        var manager = new RecipeManager(new List<Recipe>{recipe});
        manager.AddRecipeToCookingPlan(1);

        var cookingPlan = manager.RemoveRecipeFromCookingPlan(1);

        Assert.Equal(1, manager.RemovedRecipeCount);
    }

    // test 10
    [Fact]
    public void PeekRemoveRecipe_TrueVal(){
        var recipe = new Recipe {
                Id = 1,
                Title = "MasalaSoup",
                Ingredients = new List<string>{"Soup", "Chicken", "MaggieMasala"}
            };

        var manager = new RecipeManager(new List<Recipe>{recipe});
        manager.AddRecipeToCookingPlan(1);

        var cookingPlan = manager.RemoveRecipeFromCookingPlan(1);

        Assert.Equal(1, manager.PeekLastRemovedRecipe());
    }

    // test 11
    [Fact]
    public void startCooking_instructionTrue(){
        var recipe = new Recipe {
            Id = 101,
            Title = "EggsToast",
            Ingredients = new List<string>{"Eggs", "Bread", "Butter", "Jam", "Salsa", "Sriracha Sauce"},
            Instructions = new List<string>{"Take 4 Eggs", "Boil them", "Toast 3 Breads", "Spread with butter", "Toast until black layer is visible over the toast"}
        };

        var manager = new RecipeManager(new List<Recipe>{recipe});

        Assert.True(manager.StartCooking(101));
        Assert.Equal(5, manager.PendingInstructionCount);
        Assert.Equal("Take 4 Eggs", manager.PeekNextInstruction());
    }

    // [Fact]
    // public void Constructor_BuildsRecipeDictionary()
    // {
    //     var manager = CreateManager();
    //     Assert.Equal(2, manager.RecipeCount);
    //     Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    // }

    // [Fact]
    // public void InstructionsAreCompletedInFileOrder()
    // {
    //     var manager = CreateManager();
    //     Assert.True(manager.StartCooking(10));
    //     Assert.Equal("First step", manager.PeekNextInstruction());
    //     Assert.Equal("First step", manager.CompleteNextInstruction());
    //     Assert.Equal("Second step", manager.PeekNextInstruction());
    // }

    // [Fact]
    // public void RemovedRecipesAreRestoredLastInFirstOut()
    // {
    //     var manager = CreateManager();
    //     manager.AddRecipeToCookingPlan(10);
    //     manager.AddRecipeToCookingPlan(20);
    //     manager.RemoveRecipeFromCookingPlan(10);
    //     manager.RemoveRecipeFromCookingPlan(20);
    //     Assert.Equal(20, manager.PeekLastRemovedRecipe());
    //     Assert.True(manager.RestoreLastRemovedRecipe());
    //     Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    // }

    private static RecipeManager CreateManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B"
            }
        });
    }
}
