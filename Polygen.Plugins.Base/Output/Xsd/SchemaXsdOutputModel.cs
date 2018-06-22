using Polygen.Common.Xml;
using Polygen.Core.Project;
using System.Xml.Linq;
using Polygen.Core.OutputModel;

namespace Polygen.Plugins.Base.Output.Xsd
{
    public class SchemaXsdOutputModel : XmlOutputModel
    {
        public SchemaXsdOutputModel(XElement element, IProjectFile file = null) : base(element, null, file, BasePluginConstants.OutputModelName_SchemaXSD)
        {
            this.Renderer = new XmlOutputModelRenderer();
            this.MergeMode = OutputModelMergeMode.Replace;
        }
    }
}
