using AbstractLayer;
using System;
using System.Collections.Generic;

namespace SampleProject2.Common.Models
{
    public partial class ProductType : Entity
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
