using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Polygen.Core.Exceptions;
using Polygen.Core.Template;
using Polygen.Core.Utils;
using RazorLight;

namespace Polygen.Templates.Razor
{
    public class TemplateCollection : ITemplateCollection
    {
        private Dictionary<string, Template> _templateMap = new Dictionary<string, Template>();

        public TemplateCollection()
        {   
            this.Instance = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();
        }

        public IRazorLightEngine Instance { get; }

        public Polygen.Core.Template.ITemplate GetTemplate(string name, bool throwIfMissing = true)
        {
            if (!this._templateMap.TryGetValue(name, out var template) && throwIfMissing)
            {
                throw new ConfigurationException($"Template '{name}' is not registered.");
            }

            return template;
        }

        public void AddTemplate(string name, string templateText, bool overrideExisting = false)
        {
            var template = new Template(name, this.Instance, TemplateSource.CreateForText(templateText));

            this.RegisterTemplate(template, overrideExisting);
        }

        public void LoadTemplates(params string[] folders)
        {
            foreach (var folder in folders)
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.hbs", SearchOption.AllDirectories))
                {
                    var templateDir = PathUtils.GetRelativePath(folder, Path.GetDirectoryName(file)).Replace("\\", "/");
                    var templateName = Path.GetFileNameWithoutExtension(file);

                    if (!string.IsNullOrWhiteSpace(templateDir))
                    {
                        templateName = templateDir + "/" + templateName;
                    }

                    var template = new Template(templateName, this.Instance, TemplateSource.CreateForFile(file));

                    this.RegisterTemplate(template, true);
                }
            }
        }

        public void LoadTemplates(params Assembly[] assemblies)
        {
            var pattern = new Regex(@"^.*\.Templates\.(.+)\.hbs$", RegexOptions.IgnoreCase);

            foreach (var assembly in assemblies)
            {
                foreach (var resourceName in assembly.GetManifestResourceNames())
                {
                    var match = pattern.Match(resourceName);

                    if (!match.Success)
                    {
                        continue;
                    }

                    var contents = default(string);

                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    using (var reader = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        contents = reader.ReadToEnd();
                    }

                    var templateName = match.Groups[1].Value.Replace('.', '/');
                    var template = new Template(templateName, this.Instance, TemplateSource.CreateForText(contents));

                    this.RegisterTemplate(template, true);
                }
            }
        }

        public void RegisterTemplate(Polygen.Core.Template.ITemplate template, bool overrideExisting = false)
        {
            var internalTemplate = template as Template;

            if (internalTemplate == null)
            {
                throw new CodeGenerationException("Template must be of type HandlebarsNetTemplate");
            }

            if (this._templateMap.ContainsKey(internalTemplate.Name))
            {
                if (!overrideExisting)
                {
                    throw new ConfigurationException($"Template '{internalTemplate.Name}' it already registered.");
                }
                else
                {
                    this._templateMap.Remove(internalTemplate.Name);
                }
            }

            this._templateMap.Add(internalTemplate.Name, internalTemplate);
            /*this.Instance.RegisterTemplate(internalTemplate.Name,
                (writer, data) => { internalTemplate.GetRenderFn().Invoke(writer, data); });*/
        }
    }
}
