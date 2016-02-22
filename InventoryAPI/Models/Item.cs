using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    /// <summary>
    /// Item Class - The model holds the information related to the "in memory" repository 
    /// of items that are part of the "Invertory"
    /// Data Anotation used to decorate the attributes of the "Item" class, provide server 
    /// side validation and Error Messaging that can be bubbled up to the UI interface.
    /// </summary>
    [Serializable]
    public class Item
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Please enter a name for the Item")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage ="Please enter an item price")]
        [RegularExpression(@"^d{1,10}", ErrorMessage ="Please enter upto 10 digit price")]
        [MaxLength(10)]
        public int Price { get; set; }
        public int Quantity { get; set; }    
    }
}