//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodPlanner
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecipeIngredients
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }
        public decimal Quantity { get; set; }
    
        public virtual Ingredients Ingredients { get; set; }
        public virtual Recipes Recipes { get; set; }
    }
}
