using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Entities
{
    public class LineNumberDecider
    {
        public string LinePattern { get; set; }
        public int ExtraLineToAdd { get; set; }
        public bool ControlCurlyBracePossibility { get; set; }
        public LineNumberDecider()
        {

        }

        public LineNumberDecider(string linePattern, int extraLineToAdd)
        {
            LinePattern = linePattern;
            ExtraLineToAdd = extraLineToAdd;
        }

        public LineNumberDecider(string linePattern, int extraLineToAdd, bool controlCurlyBracePossibility)
        {
            LinePattern = linePattern;
            ExtraLineToAdd = extraLineToAdd;
            ControlCurlyBracePossibility = controlCurlyBracePossibility;
        }
    }
}
