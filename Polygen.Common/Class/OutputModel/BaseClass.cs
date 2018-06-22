using System.Collections.Generic;
using System.Linq;

namespace Polygen.Common.Class.OutputModel
{
    public class BaseClass
    {
        public string Name { get; set; }
        public List<string> GenericTypeArgs { get; set; } = new List<string>();

        public BaseClass(string name, List<string> genericTypeArgs)
        {
            Name = name;
            if (genericTypeArgs != null)
            {
                GenericTypeArgs = genericTypeArgs;
            }
        }

        public bool HasGenericTypeArgs => GenericTypeArgs.Any();
    }
}
