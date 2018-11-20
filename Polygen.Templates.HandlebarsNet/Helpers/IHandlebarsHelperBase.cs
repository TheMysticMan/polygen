using HandlebarsDotNet;

namespace Polygen.Templates.HandlebarsNet.Helpers
{
    public interface IHandlebarsHelperBase
    {
        string Name { get; }
    }
    
    public interface IHandlebarsHelper: IHandlebarsHelperBase
    {
        HandlebarsHelper Helper { get; }
    }
    
    public interface IHandlebarsBlockHelper: IHandlebarsHelperBase
    {
        string Name { get; set; }
        HandlebarsBlockHelper BlockHelper { get; }
    }
}
