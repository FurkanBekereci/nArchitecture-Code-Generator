using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Entities;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Abstract
{
    public abstract class BaseRegistrator
    {
        public void Register(string entityName)
        {
            try
            {
                DoExtraBefore();

                var filePath = GetFilePath();
                var lines = FileHelper.ReadLinesOfFile(filePath);
                var lineContentToAdd = GetLineContentToAdd(entityName);
                
                bool isLineExist = IsLineExist(lines, lineContentToAdd);
                if (isLineExist) return;

                ControlUsingStatements(ref lines, entityName);

                var index = GetIndexToAdd(lines);
                if (index == -1) return;

                lines.Insert(index, lineContentToAdd);
                FileHelper.AddLinesToFile(filePath, lines);
                DoExtraAfter();
            }
            catch (Exception)
            {

                // Console log.
            }
        }

        private bool IsLineExist(List<string> lines, string lineContentToAdd)
        {
            return lines.IndexOf(lines.Where(line => line.Contains(lineContentToAdd)).FirstOrDefault()) != -1;
        }

        private int GetIndexToAdd(List<string> lines)
        {
            int index = -1;

            //Soru = Bu liste ne işe yarıyor ????
            //Cevap = Eşleşmesi gereken satır önceliğine bir line pattern ve extra satır sayısı listesi eklendi,
            //LinePattern ile eşleşen satırı buluyorum.
            //ExtraLineToAdd ile ek olarak eklenmesi yada çıkarılması gereken satır sayısı belirlenir.
            List<LineNumberDecider> lineNumberDeciderList = GetLineNumberDeciderList();

            foreach (var lineNumberDecider in lineNumberDeciderList)
            {

                var tempIndex = lines.IndexOf(lines.Where(line => Regex.IsMatch(line, lineNumberDecider.LinePattern)).LastOrDefault());
                if (tempIndex != -1)
                {
                    index = tempIndex + lineNumberDecider.ExtraLineToAdd;

                    //Küme parantezi aramam gerek olabilir.
                    if (lineNumberDecider.ControlCurlyBracePossibility)
                        index = GetCorrectIndexAgainstPossibilityOfCurlyBraces(lines, index);

                    break;
                }
            }

            return index;
        }

        private int GetCorrectIndexAgainstPossibilityOfCurlyBraces(List<string> lines, int index)
        {
            var skipCount = index + 1;
            var searchingLines = lines.Skip(skipCount).ToList();
            var firstIndexOfCurlyBrace = searchingLines.FindIndex(l => l.Contains("{"));
            var correctIndex = skipCount + firstIndexOfCurlyBrace + 1;
            return correctIndex;
            
        }


        protected abstract string GetLineContentToAdd(string entityName);
        protected abstract string GetFilePath();
        protected abstract List<LineNumberDecider> GetLineNumberDeciderList();
        protected abstract void ControlUsingStatements(ref List<string> lines, string entityName);
        protected virtual void DoExtraBefore() { }
        protected virtual void DoExtraAfter() { }
    }
}
