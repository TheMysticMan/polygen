using Polygen.Core.DataType;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.Entity.Parser;
using Polygen.Plugins.Base.Models.Repository.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Schema
{
    /// <summary>
    /// Defines EntityAttribute design model schema.
    /// </summary>
    public class RepositorySchema : StageHandlerBase
    {
        public RepositorySchema() : base(StageType.RegisterSchemas, nameof(RepositorySchema), nameof(EntitySchema))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            // Define the schema elements.
            var schema = Schemas.GetSchemaByNamespace(BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = DataTypeRegistry.Get(BasePluginConstants.DataType_string);

            var entityDesignModelElement = schema.RootElement.FindChildElement("Namespace");
            entityDesignModelElement.GetBuilder()
                .CreateElement("Repository", "Defines a repository for an entity", c => c.AllowMultiple = true);

            var repositoryDesignModelElement = schema.RootElement.FindChildElement("Namespace/Repository");

            repositoryDesignModelElement
                .GetBuilder()
                .CreateAttribute("name", stringType, "Attribute name", c => c.IsMandatory = true)
                .CreateAttribute("entity", stringType, "Entity name", c => c.IsMandatory = true);

            // Register parser.
            DesignModelConverterFactory.RegisterFactory(repositoryDesignModelElement,
                new RepositoryParser());
        }
    }
}
