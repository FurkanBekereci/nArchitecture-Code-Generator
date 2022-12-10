using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Extension;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Service
{
    public class EntityAnalyzerManager : IEntityAnalyzerService
    {
        private readonly IFileService _fileService;

        public EntityAnalyzerManager(IFileService fileService)
        {
            _fileService = fileService;
        }

        public List<EntityInfo> GetAnalyzedEntityInfoList(List<string> entityNames)
        {
            return entityNames.Select(entityName => GetAnalyzedEntityInfo(entityName)).ToList();
        }

        public EntityInfo GetAnalyzedEntityInfo(string entityName)
        {
            var entityFilePath = _fileService.ExtractFilePathOfEntity(entityName);
            var entityLines = _fileService.ReadLinesFromFile(entityFilePath);
            var entityPropertyInfoList = GetEntityPropertyInfoList(entityLines);
            var entityInfo = new EntityInfo
            {
                Name = entityName,
                FilePath = entityFilePath,
                Namespace = GetNamespaceOfEntity(entityLines),
                Lines = entityLines,
                PropertyInfoList = entityPropertyInfoList,
                Content = _fileService.ReadFileContent(entityFilePath),
                EntityTypeConfigurationContent = GetEntityTypeConfigurationContentOfEntity(entityPropertyInfoList, entityName),
                DtoContent = GetDtoContent(entityLines)
            };

            return entityInfo;
        }

        private string GetEntityTypeConfigurationContentOfEntity(List<EntityPropertyInfo> entityPropertyList, string entityName)
        {
            List<string> entityTypeConfigurationLines = new List<string>() 
            { 
                $"builder.ToTable(\"{entityName.Pluralize()}\").HasKey(k => k.Id);", 
                "builder.Property(p => p.Id).HasColumnName(\"Id\");" 
            };
            entityTypeConfigurationLines.AddRange(entityPropertyList.Select(entityProperty => entityProperty.ToEntityTypeConfigurationLine().Trim()).ToList());
            var entityTypeConfigurationContent = string.Join("\n\t\t\t", entityTypeConfigurationLines);
            return entityTypeConfigurationContent;
        }

        private List<string> GetPropertyLines(List<string> lines)
        {
            const string pattern = ".*{.*get.*set.*}.*";
            var propertyLines = RegexHelper.GetFilteredLinesMatchesByPattern(lines, pattern);
            return propertyLines;
        }

        private string GetDtoContent(List<string> lines)
        {
            var propertyLines = GetPropertyLines(lines);
            var dtoLines = new List<string>() { "public int Id { get; set; }"};
            dtoLines.AddRange(propertyLines.FindAll(propertyLine => !IsLineArray(propertyLine) && IsLineTypeBuiltIn(propertyLine)));
            var dtoContent = string.Join("\n\t\t", dtoLines.Select(line => line.Trim()));
            return dtoContent;
        }

        private List<EntityPropertyInfo> GetEntityPropertyInfoList(List<string> lines)
        {

            var propertyLines = GetPropertyLines(lines);

            // { get; set; } kısmından önceki bölümleri çalışmak gerekir. Bundan dolayı "{" a göre split yapıp 0. index alınır.
            // Örnek olarak "public string Id { get; set; }" den "public string Id" bölümü elde edilmeye çalışılıyor.
            var getSetRemovedPartOfPropertyLines = propertyLines.Select(pl => pl.Split('{')?[0].Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray());

            return getSetRemovedPartOfPropertyLines.Select(result => new EntityPropertyInfo
            {
                Name = result[result.Length - 1],
                Type = result[result.Length - 2],
                IsArray = IsLineArray(result[result.Length - 2].Trim()),
                IsBuiltInType = IsLineTypeBuiltIn(result[result.Length - 2].Trim())
            }).ToList();
        }

        private bool IsLineTypeBuiltIn(string type)
        {
            string[] builtInTypes = { "string", "int", "uint", "bool", "datetime", "short", "long", "ulong" , "guid", "double" , "decimal" , "byte" , "float" , "char"};
            return builtInTypes.FirstOrDefault(builtInType => type.ToLower().Contains(builtInType)) != null;
        }

        private bool IsLineArray(string type)
        {
            string[] criterias = { "List<.*>", "Collection<.*>" , "\\[\\]", "Set<.*>" }; 
            return criterias.FirstOrDefault(criteria => RegexHelper.IsMatch(type,criteria)) != null;
        }

        private string GetNamespaceOfEntity(List<string> lines)
        {
            return lines.FirstOrDefault(line => line.Trim().ToLower().StartsWith("namespace")) ?? string.Empty;
        }

        
    }
}
