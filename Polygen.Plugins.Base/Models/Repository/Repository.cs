using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Repository
{
    public class Repository : Core.Impl.ClassModel.ClassModel
    {
        public string Entity { get; set; }
        
        public Repository(string name, string entity, INamespace ns, IXmlElement element = null) : base(name, nameof(Repository), ns, element)
        {
            Entity = entity;
        }
    }
}
