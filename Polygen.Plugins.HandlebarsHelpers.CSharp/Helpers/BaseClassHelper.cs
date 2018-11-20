using System.Collections.Generic;
using System.Linq;
using HandlebarsDotNet;
using Polygen.Common.Class.OutputModel;
using Polygen.Templates.HandlebarsNet.Helpers;

namespace Polygen.Plugins.HandlebarsHelpers.CSharp.Helpers
{
    public class BaseClassHelper : IHandlebarsHelper
    {
        public string Name => "baseclasses";

        public HandlebarsHelper Helper => (output, context, arguments) =>
        {
            var baseClassList = default(List<BaseClass>);
            if (!arguments.Any())
            {
                var dictContext = (Dictionary<string, object>) context;
                if (dictContext.ContainsKey("Model"))
                {
                    var model = (ClassOutputModel) dictContext["Model"];
                    baseClassList = model.BaseClasses;
                }
                else
                {
                    throw new ModelNotFoundInContextException("Expected to find Model in Context but couldn't find it");
                }
            }
            else
            {
                baseClassList = (List<BaseClass>) arguments.First();
            }

            if (baseClassList.Any())
            {
                var strBaseClasses = string.Join(",", baseClassList.Select(b =>
                {
                    var strName = b.Name;
                    if (b.HasGenericTypeArgs)
                    {
                        strName += $"<{string.Join(",", b.GenericTypeArgs)}>";
                    }

                    return strName;
                }));
                output.Write($": {strBaseClasses}");
            }
        };
    }
}
