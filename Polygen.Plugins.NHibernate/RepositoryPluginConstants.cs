namespace Polygen.Plugins.NHibernate
{
    public static class RepositoryPluginConstants
    {
        public const string DesignModelType_Repository = "Repository";

        public const string OutputModelType_Repository_Interface = "Repository/Interface";
        //public const string OutputModelType_Entity_CustomClass = "Entity/CustomClass";

        public const string OutputTemplateName_Repository_Interface = OutputModelType_Repository_Interface;
        //public const string OutputTemplateName_Entity_CustomClass = OutputModelType_Entity_CustomClass;
        
        public const string RepositoryBaseClass = "IEleganceRepository";
    }
}
