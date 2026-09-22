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
