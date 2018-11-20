using System;

namespace Polygen.Plugins.HandlebarsHelpers.CSharp.Helpers
{
    public class ModelNotFoundInContextException : Exception
    {
        public ModelNotFoundInContextException(string message) : base(message)
        {
        }
    }
}