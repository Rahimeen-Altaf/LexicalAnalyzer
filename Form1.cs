using LexicalAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LexicalAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        // Separators: punctuation that separates code elements
        private static String[] separators = { ";", "{", "}", ":", ".", "(", ")", "[", "]", "," };

        // Operators: symbols that perform operations
        // Multi-character operators MUST be listed before their single-character components
        private static String[] operators = {
            // 3-character operators first
            ">>>=", "<<<=",
            // 2-character operators
            "&&", "||", "++", "--", "==", "<=", ">=", "!=", "+=", "-=", "*=", "/=", "%=",
            "->", "??", "?.", "<<", ">>",
            // Single-character operators (NO parentheses/brackets - those are separators)
            "+", "-", "*", "/", "%", "=", "<", ">", "!", "?", "&", "|", "~", "^", "`"
        };

        // Expanded keywords for C#, Java, Python, JavaScript, and C++
        private static String[] keywords = {
            // C# keywords
            "abstract", "as", "base", "bool", "break", "by", "byte", "case", "catch",
            "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double",
            "descending", "explicit", "event", "extern", "else", "enum", "false", "finally", "fixed", "float", "for",
            "foreach", "from", "goto", "group", "if", "implicit", "in", "int", "interface", "internal", "into", "is",
            "lock", "long", "new", "null", "namespace", "object", "operator", "out", "override", "orderby", "params",
            "private", "protected", "public", "readonly", "ref", "return", "switch", "struct", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "select", "this", "throw", "true", "try", "typeof", "uint", "ulong",
            "unchecked", "unsafe", "ushort", "using", "var", "virtual", "volatile", "void", "while", "where", "yield",
            "async", "await", "nameof", "when", "record", "init", "with", "not", "and", "or",

            // Additional common keywords from other languages
            "function", "let", "const", "def", "lambda", "import", "export", "module", "package",
            "extends", "implements", "super", "constructor", "final", "synchronized", "transient",
            "assert", "native", "strictfp", "instanceof", "template", "typename", "auto", "register",
            "union", "typedef", "inline", "friend", "mutable", "explicit", "virtual", "nullptr",
            "pass", "raise", "except", "finally", "with", "as", "global", "nonlocal", "yield",
            "async", "await", "match", "case", "elif", "then", "end", "begin", "repeat", "until"
        };

        private static String[] comments = { "//", "/*", "*/" };
        private static String[] constants = { "\"", "\'" };
        private static String[] words;
        private static String data = "";
        private int tokenCount = 0;

        private static bool CheckSeparator(String str) => separators.Contains(str);
        private static bool CheckOperators(String str) => operators.Contains(str);
        private static bool CheckKeywords(String str) => keywords.Contains(str);
        private static bool CheckComments(String str) => comments.Contains(str);
        private static bool CheckConstants(String str) => constants.Contains(str);

        public void Program()
        {
            outputBox.Text = "";
            tokenCount = 0;
            data = textBox.Text;

            // Step 1: Remove comments first (before adding spaces)
            data = RemoveComments(data);

            // Step 2: Sort operators by length (longest first) to ensure multi-char operators are processed first
            var sortedOperators = operators.OrderByDescending(op => op.Length).ToArray();

            // Step 3: Protect multi-character operators from being split by separator processing.
            // We'll replace each operator with a unique marker that won't be modified when we add spaces around separators.
            var markerMap = new Dictionary<string, string>(); // marker -> operator
            for (int j = 0; j < sortedOperators.Length; j++)
            {
                string op = sortedOperators[j];
                // create a simple unique marker using index and a GUID fragment to avoid collisions
                string marker = $"__OP_{j}_{Guid.NewGuid().ToString("N").Substring(0, 8)}__";
                markerMap[marker] = op;
                data = data.Replace(op, marker);
            }

            // Step 4: Add spaces around separators (now safe because operators are protected by markers)
            for (int i = 0; i < separators.Length; i++)
                data = data.Replace(separators[i], " " + separators[i] + " ");

            // Step 5: Restore operators from markers as distinct tokens (add spaces around them)
            foreach (var kvp in markerMap)
            {
                string marker = kvp.Key;
                string op = kvp.Value;
                data = data.Replace(marker, " " + op + " ");
            }

            // Step 6: Normalize common whitespace characters into spaces
            data = data.Replace("\r", " ");
            data = data.Replace("\n", " ");
            data = data.Replace("\t", " ");

            // Step 7: Split by spaces and filter empty strings (collapses multiple spaces automatically)
            words = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Step 8: Analyze each word
            for (int i = 0; i < words.Length; i++)
                CheckLexicalAnalyzer(words[i]);

            // Update token count label
            tokenCountLabel.Text = $"Total Tokens: {tokenCount}";
        }

        private string RemoveComments(string code)
        {
            StringBuilder result = new StringBuilder();
            int i = 0;

            while (i < code.Length)
            {
                // Check for single-line comment //
                if (i < code.Length - 1 && code[i] == '/' && code[i + 1] == '/')
                {
                    // Skip until end of line
                    while (i < code.Length && code[i] != '\n')
                        i++;
                    continue;
                }

                // Check for multi-line comment /* */
                if (i < code.Length - 1 && code[i] == '/' && code[i + 1] == '*')
                {
                    i += 2; // Skip /*
                    // Skip until we find */
                    while (i < code.Length - 1)
                    {
                        if (code[i] == '*' && code[i + 1] == '/')
                        {
                            i += 2; // Skip */
                            break;
                        }
                        i++;
                    }
                    continue;
                }

                // Regular character, keep it
                result.Append(code[i]);
                i++;
            }

            return result.ToString();
        }

        private String Parse(String item)
        {
            StringBuilder str = new StringBuilder();

            if (CheckSeparator(item) == true)
                str.Append(" (separator, <" + item + ">) ");
            else if (CheckOperators(item) == true)
                str.Append(" (operators, <" + item + ">) ");
            else if (CheckKeywords(item) == true)
                str.Append(" (keywords, <" + item + ">) ");
            else if (item.Equals("\r") || item.Equals("\n") || item.Equals("\r\n"))
                str.Append(" (NewLine, <" + item + ">) ");
            else
                str.Append(" (identifier, <" + item + ">) ");
            return str.ToString();
        }

        private void CheckLexicalAnalyzer(String str)
        {
            // Skip empty strings
            if (string.IsNullOrWhiteSpace(str))
                return;

            // Check the WHOLE string first (this handles multi-character operators correctly)
            int intValue;

            // Check if it's an integer
            if (Int32.TryParse(str, out intValue))
            {
                outputBox.Text += (" (integerValue, <" + str + ">) ") + "\n";
                tokenCount++;
                return;
            }

            // Check if it's an operator (including multi-character like ++, --, ==, etc.)
            if (CheckOperators(str))
            {
                outputBox.Text += (" (operators, <" + str + ">) ") + "\n";
                tokenCount++;
                return;
            }

            // Check if it's a separator
            if (CheckSeparator(str))
            {
                outputBox.Text += (" (separator, <" + str + ">) ") + "\n";
                tokenCount++;
                return;
            }

            // Check if it's a keyword
            if (CheckKeywords(str))
            {
                outputBox.Text += (" (keywords, <" + str + ">) ") + "\n";
                tokenCount++;
                return;
            }

            // Check if it's a string literal
            if (str.StartsWith("\"") && str.EndsWith("\""))
            {
                outputBox.Text += (" (String, <" + str + ">) ") + "\n";
                tokenCount++;
                return;
            }

            // Check if it's a character literal
            if (str.StartsWith("\'") && str.EndsWith("\'"))
            {
                outputBox.Text += (" (Char, <" + str + ">) ") + "\n";
                tokenCount++;
                return;
            }

            // Otherwise, it's an identifier
            outputBox.Text += (" (identifier, <" + str + ">) ") + "\n";
            tokenCount++;
        }

        private void BrowseFile()
        {
            textBox.Text = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    textBox.Text = System.IO.File.ReadAllText(file);
                    path.Text = openFileDialog1.FileName;
                }
                catch (System.IO.IOException) { }
            }
        }

        private void analyzed_Click(object sender, EventArgs e) => Program();
        private void find_Click(object sender, EventArgs e) => BrowseFile();

        private void clearBtn_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            outputBox.Text = "";
            path.Text = "";
            tokenCount = 0;
            tokenCountLabel.Text = "Total Tokens: 0";
        }
    }
}