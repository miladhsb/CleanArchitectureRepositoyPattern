using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTemplateRepositoyPattern.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string Key) : base($"{Key} was not found")
        {

        }
    }
}
