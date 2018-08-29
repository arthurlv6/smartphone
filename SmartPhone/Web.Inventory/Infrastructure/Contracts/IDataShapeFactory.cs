using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Infrastructure.Contracts
{
    public interface IDataShapeFactory
    {
        object CreateDataShapedObject<T>(T expense, List<string> lstOfFields);
    }
}
