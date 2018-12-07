using System;
using System.Collections.Generic;
using System.Text;

namespace DalLayout.Entity
{
    public class Customer: BaseEntity<int>
    {
        public string  Name { get; set; }
    }
}
