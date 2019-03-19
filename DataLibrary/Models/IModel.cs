using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public interface IModel<T>
    {
        int? ID { get; set; }
        T Clone(T model);
        
    }
}
