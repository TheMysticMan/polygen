using Polygen.Common.Class.OutputModel;
using Polygen.Common.Class.Renderer;
using Polygen.Core.Impl.DesignModel;
using Polygen.TestUtils.NamingConvention;
using FluentAssertions;
using System.IO;
using Polygen.Templates.Razor;
using RazorLight;
using Xunit;

namespace Polygen.Common.Tests
{
    public class ClassOutputModelRendererTests
    {
        [Fact]
        public void Generate_CSharp_from_output_model()
        {
            var ns = new Namespace("MyApp.MyTest", null);
            var builder = new ClassOutputModelBuilder("test", null, new TestClassNamingConvention());

            builder.CreateClass("MyClass", ns);
            builder.CreateProperty("Name", "string");

            var outputModel = builder.Build();
            var instance = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();
            
            var template = new Template("test", instance, TemplateSource.CreateForText(@"
@model Polygen.Common.Class.OutputModel.ClassOutputModel
@{
DisableEncoding = true;
}
namespace @Model.ClassNamespace
public class @Model.ClassName
{
@foreach(var prop in Model.Properties) {
<text>    public @prop.Type.TypeName @prop.Name { get; set; }
</text>
}
}
"));
            var renderer = new ClassOutputModelRenderer(template);
            var actual = default(string);
            var expected = @"
namespace MyApp.MyTest
public class MyClass
{
    public string Name { get; set; }
}
";

            using (var writer = new StringWriter())
            {
                
                renderer.Render(outputModel, writer);
                actual = writer.ToString();
            }

            actual.Should().Be(expected);
        }
    }
}
