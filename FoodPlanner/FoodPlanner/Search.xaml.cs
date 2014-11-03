﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace FoodPlanner
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        private List<InventoryIngredient> inventoryList = MainWindow.db.InventoryIngredients.Where(ii => ii.UserID == MainWindow.CurrentUser.ID).ToList();

        public Search()
        {
            InitializeComponent();
        }

        private void startSearch_Click(object sender, RoutedEventArgs e)
        {
            List<string> searchQuery = searchBox.Text.Split(',').Select(s => s.Trim()).ToList();
            try
            {
                IQueryable<IGrouping<int, SearchResults>> recipeIngredient = (from rec in MainWindow.db.Recipes
                                                              join ri in MainWindow.db.RecipeIngredients on rec.ID equals ri.RecipeID
                                                              join ing in MainWindow.db.Ingredients on ri.IngredientID equals ing.ID
                                                              //join ii in MainWindow.db.InventoryIngredients on ri.IngredientID equals ii.IngredientID
                                                              where searchQuery.Any(s => ing.Name.Contains(s)) || searchQuery.Any(s => rec.Title.Contains(s))
                                                              select new SearchResults()
                                                              {
                                                                  RecipeID = ri.RecipeID,
                                                                  Recipe = ri.Recipe,
                                                                  RecipeQuantity = ri.Quantity,
                                                                  InventoryQuantity = (from ii in MainWindow.db.InventoryIngredients where ii.IngredientID == ing.ID select ii.Quantity).FirstOrDefault(),
                                                                  IngredientCount = ri.Recipe.RecipeIngredients.Count()
                                                              } into c
                                                              group c by c.Recipe.ID into g
                                                              select g);


                MessageBox.Show(recipeIngredient.Count().ToString());
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }



            /*
            var q3 = from ri in MainWindow.db.RecipeIngredients
                     join ii in MainWindow.db.InventoryIngredients on ri.IngredientID equals ii.IngredientID
                     select new { RecipeID = ri.RecipeID, Recipe = ri.Recipe, RecipeQuantity = ri.Quantity, InventoryRecipe = ii.Quantity, IngredientCount = ri.Recipe.RecipeIngredients.Count() } into c
                     group c by c.RecipeID into g
                     select g;
            */

            /*
            List<SearchResults> searchResults = new List<SearchResults>();

            List<string> searchQuery = searchBox.Text.Split(',').Select(s => s.Trim()).ToList();

            List<RecipeIngredient> recipeIngredient = (from rec in MainWindow.db.Recipes join ri in MainWindow.db.RecipeIngredients on rec.ID equals ri.RecipeID join ing in MainWindow.db.Ingredients on ri.IngredientID equals ing.ID where searchQuery.Any(s => ing.Name.Contains(s)) || searchQuery.Any(s => rec.Title.Contains(s)) select ri).Take(50).ToList();

            foreach (RecipeIngredient ri in recipeIngredient)
            {
                if (searchResults.Where(x => x.recipe.Title == ri.Recipe.Title).Count() == 0)
                {
                    searchResults.Add(new SearchResults(ri.Recipe, searchQuery.Any(s => ri.Ingredient.Name.Contains(s)), searchQuery.Any(s => ri.Recipe.Title.Contains(s))));
                }
                else
                {
                    if (searchQuery.Any(s => ri.Ingredient.Name.Contains(s))) // kan eventuelt optimeres
                    {
                        searchResults.Where(x => x.recipe.Title == ri.Recipe.Title).Single().match++;
                    }
                }

                if (inventoryList.Where(ii => ii.IngredientID == ri.IngredientID).Count() == 1)
                {
                    if (inventoryList.Where(ii => ii.IngredientID == ri.IngredientID).First().Quantity >= ri.Quantity)
                    {
                        searchResults.Where(x => x.recipe.Title == ri.Recipe.Title).Single().fullMatch++;
                    }
                    else
                    {
                        searchResults.Where(x => x.recipe.Title == ri.Recipe.Title).Single().partialMatch++;
                    }
                }
            }

            searchList.ItemsSource = searchResults.OrderByDescending(x => x.fullMatch).ThenByDescending(x => x.partialMatch).ThenByDescending(x => x.match).ThenBy(x => x.recipe.Title);
            */
        }

        private void searchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            var showRecipe = new ShowRecipe(((SearchResults)searchList.SelectedItem).recipe);
            showRecipe.ShowDialog();
            */
        }
    }
}
