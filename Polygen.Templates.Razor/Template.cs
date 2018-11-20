using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RazorLight;
using RazorLight.Extensions;
using ITemplate = Polygen.Core.Template.ITemplate;

namespace Polygen.Templates.Razor
{
    public class Template : ITemplate
    {
        private readonly IRazorLightEngine _razorEngineService;
        private string _templateText;
        private Action<TextWriter, object> _compiledTemplateInstance;

        public Template(string name, IRazorLightEngine razorEngineService, TemplateSource source)
        {
            _razorEngineService = razorEngineService;
            this.Name = name;
            this.Source = source;
        }

        public string Name { get; }
        public string Type { get; } = Constants.TemplateType;
        public TemplateSource Source { get; }

        public string GetTemplateText()
        {
            if (this._templateText == null)
            {
                if (this.Source.IsFile)
                {
                    this._templateText = File.ReadAllText(this.Source.FilePath, Encoding.UTF8);
                }
                else
                {
                    this._templateText = this.Source.Text;
                }
            }

            return this._templateText;
        }

        public void Render(dynamic data, TextWriter writer)
        {
            writer.Write(_razorEngineService.CompileRenderAsync(Name, GetTemplateText(), data).Result);
        }
    }

    internal static class DictionaryExtensions
    {
        internal static dynamic ConvertToExpandoObject(this Dictionary<string, object> dictionary)
        {
            IDictionary<string, object> eo = new ExpandoObject() as IDictionary<string, object>;
            foreach (var kvp in dictionary)
            {
                eo.Add(kvp.Key, kvp.Value);
            }

            return eo;
        }
    }
}
