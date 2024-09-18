using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Domain.Entities
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int id) : base($"Entity ({entityName}) with the specified ID ({id}) could not be found") {
            
        }
    }
}