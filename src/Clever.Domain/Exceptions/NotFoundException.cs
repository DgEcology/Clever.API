using System;

namespace Clever.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int id) : base($"Entity ({entityName}) with the specified ID ({id}) could not be found") {}

    }
}