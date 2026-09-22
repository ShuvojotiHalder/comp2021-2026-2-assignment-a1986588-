using System;
using System.Collections.Generic;


namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
    // TODO Part A: add your private collection fields here.
    private Dictionary<int, Recipe> _recipes;
    private List<string> _shoppingList;

    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        // TODO Part A: validate recipes and build Dictionary<int, Recipe>.
        if(recipes == null)
            throw new ArgumentException(nameof(recipes));

        // Initialzing the values    
        _recipes = new Dictionary<int, Recipe>();
        _shoppingList = new List<string>();

        foreach(Recipe recipe in recipes) {
            if(_recipes.ContainsKey(recipe.Id))
                throw new ArgumentException($"{recipe.Id} is already present.");
            _recipes.Add(recipe.Id, recipe);
        }
    }

    public int RecipeCount => _recipes.Count;
    public int ShoppingItemCount => _shoppingList.Count;
    public int CookingPlanCount => 0;
    public int PendingInstructionCount => 0;
    public int RemovedRecipeCount => 0;

    public bool AddRecipe(Recipe recipe) {
        if(recipe == null)
            return false;
        if(_recipes.ContainsKey(recipe.Id)) {
            throw new ArgumentException("Duplicate ID!");
            return false;
        }
         _recipes.Add(recipe.Id, recipe);
         return true;   
    }

    public Recipe? FindRecipe(int recipeId) {
        if(_recipes.TryGetValue(recipeId, out Recipe? recipe));
            return recipe;
        return null;
    }

    public bool RemoveRecipe(int recipeId) {
        if(!_recipes.ContainsKey(recipeId))
            throw new NotImplementedException("Invalid Recipe.");
        return _recipes.Remove(recipeId);
    }

    public int AddIngredientsToShoppingList(int recipeId) {
        Recipe? recipe = FindRecipe(recipeId);

        if(recipe == null)
            return 0;
        foreach(string ingredient in recipe.Ingredients) {
            _shoppingList.Add(ingredient);
        }
        return recipe.Ingredients.Count;
    }

    public IReadOnlyList<string> GetShoppingList() {
        return _shoppingList.AsReadOnly();
    }

    public void ClearShoppingList() {
        _shoppingList.Clear();
    }

    public bool AddRecipeToCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement AddRecipeToCookingPlan.");

    public bool RemoveRecipeFromCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement RemoveRecipeFromCookingPlan.");

    public bool RestoreLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement RestoreLastRemovedRecipe.");

    public int? PeekLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement PeekLastRemovedRecipe.");

    public IReadOnlyList<int> GetCookingPlan() =>
        throw new NotImplementedException("Part A: implement GetCookingPlan.");

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}
