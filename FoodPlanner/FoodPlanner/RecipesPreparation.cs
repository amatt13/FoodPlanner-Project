//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodPlanner.Models
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class RecipesPreparation
    {
        public RecipesPreparation()
        {
            this.Recipes = new ObservableCollection<Recipe>();
        }
    
        public int ID { get; set; }
        public string Preparation { get; set; }
    
        public virtual ObservableCollection<Recipe> Recipes { get; set; }
    }
}
