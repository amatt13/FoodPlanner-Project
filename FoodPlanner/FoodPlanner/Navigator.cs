﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using FoodPlanner.Models;
using FoodPlanner.ViewModels;
using FoodPlanner.Views;
using System.Windows.Input;
using System.Windows.Controls;
using MvvmFoundation.Wpf;

namespace FoodPlanner
{
    public class Navigator
    {

        #region Fields

        private static NavigationService _navigationService;
        private static List<KeyValuePair<string, Uri>> _pages;
        private static ICommand _goToInventoryCommand;
        private static ICommand _goToShoppingListCommand;
        private static ICommand _goToRecipeSearchCommand;
        private static ICommand _goToSettingsCommand;
        private static ICommand _goToMealPlanCommand;
        private static ICommand _goToRecipeCommand;
        private static ICommand _goToRecipeFromMealCommand;
        private static ICommand _goBackCommand;

        #endregion

        private Navigator() { }

        #region Properties & Commands

        public static List<KeyValuePair<string, Uri>> Pages
        {
            get
            {
                if (_pages == null)
                {
                    _pages = new List<KeyValuePair<string, Uri>>() {
                        new KeyValuePair<string, Uri>("Mealplan",       new Uri("Views/MealPlanPage.xaml", UriKind.Relative)),
                        new KeyValuePair<string, Uri>("Search",         new Uri("Views/RecipeSearchPage.xaml", UriKind.Relative)),
                        new KeyValuePair<string, Uri>("Shopping List",  new Uri("Views/ShoppingListPage.xaml", UriKind.Relative)),
                        new KeyValuePair<string, Uri>("Inventory",      new Uri("Views/InventoryPage.xaml", UriKind.Relative)),
                        new KeyValuePair<string, Uri>("Settings",       new Uri("Views/SettingsPage.xaml", UriKind.Relative))
                    };
                }
                return _pages;
            }
        }

        public static NavigationService NavigationService
        {
            get
            {
                return _navigationService;
            }
            set
            {
                _navigationService = value;
                _navigationService.Navigated += NavigationService_Navigated;
            }
        }

        public static ICommand GoToInventoryCommand
        {
            get
            {
                if (_goToInventoryCommand == null)
                {
                    _goToInventoryCommand = new RelayCommand(() => Navigator.Navigate(new InventoryPage()));
                }
                return _goToInventoryCommand;
            }
        }

        public static ICommand GoToShoppingListCommand
        {
            get
            {
                if (_goToShoppingListCommand == null)
                {
                    _goToShoppingListCommand = new RelayCommand(() => Navigator.Navigate(new ShoppingListPage()));
                }
                return _goToShoppingListCommand;
            }
        }

        public static ICommand GoToRecipeSearchCommand
        {
            get
            {
                if (_goToRecipeSearchCommand == null)
                {
                    _goToRecipeSearchCommand = new RelayCommand(() => Navigator.Navigate(new RecipeSearchPage()));
                }
                return _goToRecipeSearchCommand;
            }
        }

        public static ICommand GoToSettingsCommand
        {
            get
            {
                if (_goToSettingsCommand == null)
                {
                    _goToSettingsCommand = new RelayCommand(() => Navigator.Navigate(new SettingsPage()));
                }
                return _goToSettingsCommand;
            }
        }

        public static ICommand GoToMealPlanCommand
        {
            get
            {
                if (_goToMealPlanCommand == null)
                {
                    _goToMealPlanCommand = new RelayCommand(() => Navigator.Navigate(new MealPlanPage()));
                }
                return _goToMealPlanCommand;
            }
        }

        public static ICommand GoToRecipeCommand
        {
            get
            {
                if (_goToRecipeCommand == null)
                {
                    _goToRecipeCommand = new RelayCommand<Recipe>(recipe =>
                    {
                        RecipeViewModel rvm = new RecipeViewModel(recipe);
                        RecipePage rp = new RecipePage();
                        rp.DataContext = rvm;
                        Navigator.Navigate(rp);
                    });
                }
                return _goToRecipeCommand;
            }
        }

        public static ICommand GoToRecipeFromMealCommand
        {
            get
            {
                if (_goToRecipeFromMealCommand == null)
                {
                    _goToRecipeFromMealCommand = new RelayCommand<Meal>(meal =>
                    {
                        RecipeViewModel rvm = new RecipeViewModel(meal);
                        RecipePage rp = new RecipePage();
                        rp.DataContext = rvm;
                        Navigator.Navigate(rp);
                    });
                }
                return _goToRecipeFromMealCommand;
            }
        }

        public static ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(() => NavigationService.GoBack());
                }
                return _goBackCommand;
            }
        }

        #endregion

        #region Methods

        private static void Navigate(Page page)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(page);
            }
            else
            {
                Console.WriteLine("Navigation Service not available!");
            }
        }

        private static void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            // Only allow back-navigation from the Recipe page
            if (e.Content.GetType() != typeof(FoodPlanner.Views.RecipePage))
            {
                _navigationService.RemoveBackEntry();
            }
        }

        #endregion

    }
}
