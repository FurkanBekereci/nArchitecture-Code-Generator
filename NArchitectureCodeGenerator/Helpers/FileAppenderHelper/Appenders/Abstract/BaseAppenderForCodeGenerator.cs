using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Entities;
using NArchitectureCodeGenerator.Helpers;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;

namespace NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Abstract
{
    public abstract class BaseAppenderForCodeGenerator : BaseAppender
    {
        
        protected EntityInfo _entityInfo;

        protected BaseAppenderForCodeGenerator(IFileService fileService) : base(fileService)
        {

        }

        private void SetEntityInfo(EntityInfo entityInfo) => _entityInfo = entityInfo;

        public void AppendLinesForCodeGenerator(EntityInfo entityInfo)
        {
            SetEntityInfo(entityInfo);

            AppendLines();

        }

        //private bool IsLineContentExist(string filePath, string lineContentToAdd)
        //{
        //    return _fileService.ReadFileContent(filePath)?.Contains(lineContentToAdd.Trim()) ?? false;
        //}

        //private int GetIndexToAdd(List<string> lines)
        //{
        //    int index = -1;

        //    //Soru = Bu lineNumberDeciderList ne işe yarıyor ????
        //    //Cevap = Eşleşmesi gereken satır önceliğine göre bir line pattern ve extra satır sayısı listesi eklendi,
        //    //LinePattern ile eşleşen satırı buluyorum.
        //    //ExtraLineToAdd ile bulunan satır indexine ek olarak eklenmesi yada çıkarılması gereken extra satır sayısı belirleniyor.
        //    List<LineNumberDecider> lineNumberDeciderList = GetLineNumberDeciderList();

        //    foreach (var lineNumberDecider in lineNumberDeciderList)
        //    {

        //        var tempIndex = lines.IndexOf(lines.Where(line => RegexHelper.IsMatch(line, lineNumberDecider.LinePattern)).LastOrDefault());
        //        if (tempIndex != -1)
        //        {
        //            index = tempIndex + lineNumberDecider.ExtraLineToAdd;

        //            //Küme parantezi aramam gerek olabilir.
        //            if (lineNumberDecider.ControlCurlyBracePossibility)
        //                index = GetCorrectIndexAgainstPossibilityOfCurlyBraces(lines, index);

        //            break;
        //        }
        //    }

        //    return index;
        //}

        //private int GetCorrectIndexAgainstPossibilityOfCurlyBraces(List<string> lines, int index)
        //{
        //    var skipCount = index + 1;
        //    var searchingLines = lines.Skip(skipCount).ToList();
        //    var firstIndexOfCurlyBrace = searchingLines.FindIndex(l => l.Contains("{"));
        //    var correctIndex = skipCount + firstIndexOfCurlyBrace + 1;
        //    return correctIndex;
            
        //}



    }
}
