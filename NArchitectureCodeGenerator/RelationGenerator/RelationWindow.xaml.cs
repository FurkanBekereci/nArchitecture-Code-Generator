using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.RelationGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NArchitectureCodeGenerator.RelationGenerator
{
    /// <summary>
    /// Interaction logic for RelationWindow.xaml
    /// </summary>
    public partial class RelationWindow : Window
    {
        private EntityRelationInfo _selectedEntityRelationInfo;
        private EntityRelationInfo _otherEntityRelationInfo;
        private EntityInfo _selectedEntityInfo;
        private List<EntityInfo> _listOfOtherEntityInfos;
        private List<EntityInfo> _listOfSelectedEntityInfo; //This is for seleceted entity combobox.
        private RelationWindowDialogResult _dialogResult;
        public RelationWindow(EntityInfo selectedEntityInfo, List<EntityInfo> listOfOtherEntityInfos)
        {
            _selectedEntityInfo = selectedEntityInfo;

            _listOfOtherEntityInfos = listOfOtherEntityInfos;
            _listOfSelectedEntityInfo = new List<EntityInfo>() { _selectedEntityInfo};

            InitializeEntityRelationInfos();
            InitializeComponent();            
            InitializeComboboxes();
        }

        private void InitializeEntityRelationInfos()
        {
            _selectedEntityRelationInfo = new EntityRelationInfo()
            {
                EntityInfo = _selectedEntityInfo,
                EntityAppendLine = "",
                EntityTypeConfigurationAppendLine = ""            
            };

            _otherEntityRelationInfo = new EntityRelationInfo()
            {
                EntityInfo = _listOfOtherEntityInfos[0],
                EntityAppendLine = "",
                EntityTypeConfigurationAppendLine = ""
            };
        }

        private void InitializeComboboxes()
        {
            cmbEntity1.ItemsSource = _listOfSelectedEntityInfo;
            cmbEntity1.DisplayMemberPath = "Name";

            cmbEntity2.ItemsSource = _listOfOtherEntityInfos;
            cmbEntity2.DisplayMemberPath = "Name";
        }

        private void cmbEntity2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _otherEntityRelationInfo.EntityInfo = cmbEntity2.SelectedItem as EntityInfo;
            SetAppendLinesByRelationType();
        }

        private void cmbRelationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetAppendLinesByRelationType();
        }

        private void SetAppendLinesByRelationType()
        {
            switch (cmbRelationType.SelectedValue.ToString())
            {
                case "One to many":
                    _selectedEntityRelationInfo.EntityAppendLine = GetManyRelationEntityAppendLines(_otherEntityRelationInfo.EntityInfo.Name);
                    _otherEntityRelationInfo.EntityAppendLine = GetOneRelationEntityAppendLines(_selectedEntityRelationInfo.EntityInfo.Name);
                    _selectedEntityRelationInfo.EntityTypeConfigurationAppendLine = GetManyRelationEntityTypeConfigurationAppendLines(_otherEntityRelationInfo.EntityInfo.Name);
                    _otherEntityRelationInfo.EntityTypeConfigurationAppendLine = GetOneRelationEntityTypeConfigurationAppendLines(_selectedEntityRelationInfo.EntityInfo.Name);
                    return;
                case "Many to one":
                    _selectedEntityRelationInfo.EntityAppendLine = GetOneRelationEntityAppendLines(_otherEntityRelationInfo.EntityInfo.Name);
                    _otherEntityRelationInfo.EntityAppendLine = GetManyRelationEntityAppendLines(_selectedEntityRelationInfo.EntityInfo.Name);
                    _selectedEntityRelationInfo.EntityTypeConfigurationAppendLine = GetOneRelationEntityTypeConfigurationAppendLines(_otherEntityRelationInfo.EntityInfo.Name);
                    _otherEntityRelationInfo.EntityTypeConfigurationAppendLine = GetManyRelationEntityTypeConfigurationAppendLines(_selectedEntityRelationInfo.EntityInfo.Name);
                    return;
                case "Many to many":
                    _selectedEntityRelationInfo.EntityAppendLine = GetManyRelationEntityAppendLinesWhenManyToMany();
                    _otherEntityRelationInfo.EntityAppendLine = GetManyRelationEntityAppendLinesWhenManyToMany();
                    _selectedEntityRelationInfo.EntityTypeConfigurationAppendLine = GetManyRelationEntityTypeConfigurationAppendLinesWhenManyToMany();
                    _otherEntityRelationInfo.EntityTypeConfigurationAppendLine = GetManyRelationEntityTypeConfigurationAppendLinesWhenManyToMany();
                    return;

                default:
                    _selectedEntityRelationInfo.EntityAppendLine = "";
                    _otherEntityRelationInfo.EntityAppendLine = "";
                    _selectedEntityRelationInfo.EntityTypeConfigurationAppendLine = "";
                    _otherEntityRelationInfo.EntityTypeConfigurationAppendLine = "";
                    break;
            }
        }


        private string GetOneRelationEntityAppendLines(string otherEntityName)
        {
            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"\t\tpublic int {otherEntityName}Id {{ get; set; }}");
            lineBuilder.AppendLine($"\t\tpublic virtual {otherEntityName}? {otherEntityName} {{ get; set; }}");
            return lineBuilder.ToString();
        }

        private string GetManyRelationEntityAppendLines(string otherEntityName)
        {

            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"\t\tpublic virtual ICollection<{otherEntityName}> {otherEntityName.Pluralize()} {{ get; set; }}");
            return lineBuilder.ToString();
        }

        private string GetOneRelationEntityTypeConfigurationAppendLines(string otherEntityName)
        {
            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"\t\t\tbuilder.Property(p => p.{otherEntityName}Id).HasColumnName(\"{otherEntityName}Id\");");
            lineBuilder.AppendLine($"\t\t\tbuilder.HasOne(p => p.{otherEntityName});");
            return lineBuilder.ToString();
        }

        private string GetManyRelationEntityTypeConfigurationAppendLines(string otherEntityName)
        {
            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"\t\t\tbuilder.HasMany(p => p.{otherEntityName.Pluralize()});");
            return lineBuilder.ToString();
        }

        private string GetManyRelationEntityAppendLinesWhenManyToMany()
        {
            var entityName = $"{_selectedEntityInfo.Name}{_otherEntityRelationInfo.EntityInfo.Name}";
            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"\t\tpublic virtual ICollection<{entityName}> {entityName.Pluralize()} {{ get; set; }}");
            return lineBuilder.ToString();
        }
        private string GetManyRelationEntityTypeConfigurationAppendLinesWhenManyToMany()
        {
            var entityName = $"{_selectedEntityInfo.Name}{_otherEntityRelationInfo.EntityInfo.Name}";
            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"\t\t\tbuilder.HasMany(p => p.{entityName.Pluralize()});");
            return lineBuilder.ToString();
        }

        private void btnCreateRelation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedEntityRelationInfo.EntityAppendLine))
            {
                MessageBox.Show("Please select a relation type");
                return;
            }
            SetDialogResult();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetDialogResult()
        {            
            _dialogResult = new RelationWindowDialogResult()
            {
                SelectedEntityRelationInfo = _selectedEntityRelationInfo,
                OtherEntityRelationInfo = _otherEntityRelationInfo,
                IsManyToMany = cmbRelationType.SelectedValue.ToString() == "Many to many",
                ManyToManyRelationEntityName = GetManyToManyRelationEntityName(),
                ManyToManyRelationEntityContent = GetManyToManyRelationEntityContent(),
            };
        }

        public RelationWindowDialogResult GetDialogResult() => _dialogResult;

        private string GetManyToManyRelationEntityName()
        {
            return $"{_selectedEntityRelationInfo.EntityInfo.Name}{_otherEntityRelationInfo.EntityInfo.Name}";
        }

        private string GetManyToManyRelationEntityContent()
        {
            var lineBuilder = new StringBuilder();
            lineBuilder.AppendLine($"public int {_selectedEntityRelationInfo.EntityInfo.Name}Id {{ get; set; }}");
            lineBuilder.AppendLine($"\t\tpublic int {_otherEntityRelationInfo.EntityInfo.Name}Id {{ get; set; }}");
            lineBuilder.AppendLine($"\t\tpublic virtual {_selectedEntityRelationInfo.EntityInfo.Name} {_selectedEntityRelationInfo.EntityInfo.Name} {{ get; set; }}");
            lineBuilder.AppendLine($"\t\tpublic virtual {_otherEntityRelationInfo.EntityInfo.Name} {_otherEntityRelationInfo.EntityInfo.Name} {{ get; set; }}");

            return lineBuilder.ToString();
        }
    }
}
