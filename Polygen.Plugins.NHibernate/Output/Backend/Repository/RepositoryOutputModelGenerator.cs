using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Plugins.Base;
using Polygen.Plugins.NHibernate.Output.Backend.Entity;

namespace Polygen.Plugins.NHibernate.Output.Backend.Repository
{
    public class RepositoryOutputModelGenerator : IOutputModelGenerator
    {
        private readonly RepositoryConverter _repositoryConverter;

        public RepositoryOutputModelGenerator(RepositoryConverter repositoryConverter)
        {
            _repositoryConverter = repositoryConverter;
        }

        public IEnumerable<IOutputModel> GenerateOutputModels(IDesignModel designModel)
        {
            var repository = (Base.Models.Repository.Repository)designModel;

            return new[] {
                _repositoryConverter.CreateRepositoryInterface(repository, BasePluginConstants.Language_CSharp)
                //_repositoryConverter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp)
            };
        }
    }
}
