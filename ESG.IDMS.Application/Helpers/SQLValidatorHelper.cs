using System.Text.RegularExpressions;

namespace ESG.IDMS.Application.Helpers
{
    public static partial class SQLValidatorHelper
    {
        public static string Validate(string? sqlScript)
        {
            var validationResult = "";
            if (sqlScript != null)
            {
                if (ForbidInsertRegex().IsMatch(sqlScript))
                {
                    validationResult += "Sql Script has `Insert`. ";
                }
                if (ForbidDeleteRegex().IsMatch(sqlScript))
                {
                    validationResult += "Sql Script has `Delete`. ";
                }
                if (ForbidUpdateRegex().IsMatch(sqlScript))
                {
                    validationResult += "Sql Script has `Update`. ";
                }
                if (ForbitCreateRegex().IsMatch(sqlScript))
                {
                    validationResult += "Sql Script has `Create`. "; 
                }
                if (ForbidAlterRegex().IsMatch(sqlScript))
                {
                    validationResult += "Sql Script has `Alter`. ";
                }

                // Add a condition to check for DROP but allow DROP TABLE #TempTable or tables with hash "#"
                if (ForbidDropRegex().IsMatch(sqlScript))
                {
                    validationResult += "Sql Script has `Drop`. ";
                }
            }
            return validationResult;
        }

        [GeneratedRegex(@"\bINSERT\b(?!.*INTO\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ForbidInsertRegex();
        [GeneratedRegex(@"\bDELETE\b", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ForbidDeleteRegex();
        [GeneratedRegex(@"\bUPDATE\b(?!.*#TempTable\b)(?!.*#)", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ForbidUpdateRegex();
        [GeneratedRegex(@"\bCREATE\b(?!.*TABLE\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ForbitCreateRegex();
        [GeneratedRegex(@"\bALTER\b(?!.*TABLE\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ForbidAlterRegex();
        [GeneratedRegex(@"\bDROP\b(?!.*TABLE\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ForbidDropRegex();
    }
}
