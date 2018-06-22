using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Repository.Parser
{
    /// <summary>
    /// Entity design model parser.
    /// </summary>
    public class RepositoryParser: DesignModelGeneratorBase
    {
        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            return new Repository(xmlElement.GetStringAttributeValue("name"), xmlElement.GetStringAttributeValue("entity"), context.Namespace, xmlElement);
        }
    }
}
