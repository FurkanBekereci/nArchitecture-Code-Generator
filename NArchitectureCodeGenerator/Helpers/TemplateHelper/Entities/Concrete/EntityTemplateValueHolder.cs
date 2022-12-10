using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Enums;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete
{
    public class EntityTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder, ITemplateDynamicContentHolder
    {
        public TemplateType TemplateType => TemplateType.Entity;
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Content { get; set; }

        public EntityTemplateValueHolder()
        {

        }

        public EntityTemplateValueHolder(string name, string @namespace, string content)
        {
            Name = name;
            Namespace = @namespace;
            Content = content;
        }

    }
}
