using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Polygen.Common.Class.OutputModel;
using Polygen.Core.Exceptions;
using Polygen.Core.OutputModel;
using Polygen.Core.Template;

namespace Polygen.Plugins.NHibernate.Output.Backend.Repository
{
    /// <summary>
    /// Converts entities into backend output model.
    /// </summary>
    [PublicAPI]
    public class RepositoryConverter
    {
        private ITemplateCollection _templateCollection;

        public RepositoryConverter(ITemplateCollection templateCollection)
        {
            _templateCollection = templateCollection;
        }

        public ClassOutputModel CreateRepositoryInterface(Base.Models.Repository.Repository repository, string language)
        {
            var targetPlatform = repository.OutputConfiguration.GetTargetPlatformsForDesignModel(repository).FirstOrDefault();

            if (targetPlatform == null)
            {
                throw new ConfigurationException(
                    $"No target platforms defined for design model type '{repository.DesignModelType}'.");
            }

            var namingConvention = targetPlatform.GetClassNamingConvention(language);
            var outputModelType = RepositoryPluginConstants.OutputModelType_Repository_Interface;
            var builder = new ClassOutputModelBuilder(outputModelType, repository, namingConvention);

            builder.CreateInterface(repository.Name, repository.Namespace);
            builder.AddBaseClass(RepositoryPluginConstants.RepositoryBaseClass, new List<string>() {repository.Entity});
            builder.SetOutputFile(repository.OutputConfiguration, namingConvention, fileExtension: ".cs", mergeMode:OutputModelMergeMode.Replace);
            builder.SetOutputRenderer(targetPlatform.GetOutputTemplate(outputModelType));

            return builder.Build();
        }
    }
}
