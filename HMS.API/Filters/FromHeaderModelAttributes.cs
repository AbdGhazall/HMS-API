using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HMS.API.Filters
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FromHeaderModel : Attribute, IBindingSourceMetadata, IModelNameProvider
    {
        public BindingSource BindingSource => BindingSource.Query;

        public string Name { get; set; }
    }
}