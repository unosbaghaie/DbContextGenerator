using AbstractLayer;
using System;
using System.Collections.Generic;

namespace SampleProject1.Common.Models
{
    public partial class Product : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<byte> ProductTypeId { get; set; }
    }
}
