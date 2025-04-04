﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreAndNorthWindDb.Models;

public class Product
{
    public int ProductId { get; set; } // The primary key.
    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!;
    // Property name is different from the column name.
    [Column("UnitPrice", TypeName = "money")]
    public decimal? Cost { get; set; }
    [Column("UnitsInStock")]
    public short? Stock { get; set; }
    public bool Discontinued { get; set; }
    // These two properties define the foreign key relationship
    // to the Categories table.
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}
