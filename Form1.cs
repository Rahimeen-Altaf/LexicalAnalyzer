using LexicalAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace LexicalAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        // Separators: punctuation that separates code elements (single-char)
        private static readonly string[] separators = { ";", "{", "}", ":", ".", "(", ")", "[", "]", "," };
        private static readonly HashSet<char> separatorChars = new HashSet<char>(separators.Select(s => s[0]));

        // Operators: list multi-char first is ensured by sorting when used
        private static readonly string[] operators = {
            ">>>=", "<<<=",
            "&&", "||", "++", "--", "==", "<=", ">=", "!=", "+=", "-=", "*=", "/=", "%=",
            "->", "??", "?.", "<<", ">>",
            "+", "-", "*", "/", "%", "=", "<", ">", "!", "?", "&", "|", "~", "^", "`"
        };

        // Keywords (expanded)
        private static readonly HashSet<string> keywords = new HashSet<string>(StringComparer.Ordinal)
        {
            "abstract","as","base","bool","break","by","byte","case","catch","char","checked","class","const","continue",
            "decimal","default","delegate","do","double","descending","explicit","event","extern","else","enum","false",
            "finally","fixed","float","for","foreach","from","goto","group","if","implicit","in","int","interface","internal",
            "into","is","lock","long","new","null","namespace","object","operator","out","override","orderby","params",
            "private","protected","public","readonly","ref","return","switch","struct","sbyte","sealed","short","sizeof",
            "stackalloc","static","string","select","this","throw","true","try","typeof","uint","ulong","unchecked","unsafe",
            "ushort","using","var","virtual","volatile","void","while","where","yield","async","await","nameof","when",
            "record","init","with","not","and","or","function","let","const","def","lambda","import","export","module",
            "package","extends","implements","super","constructor","final","synchronized","transient","assert","native",
            "strictfp","instanceof","template","typename","auto","register","union","typedef","inline","friend","mutable",
            "explicit","virtual","nullptr","pass","raise","except","finally","with","as","global","nonlocal","yield",
            "async","await","match","case","elif","then","end","begin","repeat","until"
        };

        private int tokenCount = 0;

        // Pre-sorted operators for quick matching (longest first)
        private readonly string[] sortedOperators = operators.OrderByDescending(o => o.Length).ToArray();

        public void Program()
        {
            outputBox.Text = "";
            tokenCount = 0;

            string code = textBox.Text ?? "";

            // Remove comments but preserve string/char literals (so comment markers inside strings are not removed)
            code = RemoveComments(code);

            // Tokenize with a simple single-pass scanner that preserves string/char literals (including spaces/newlines)
            var tokens = Tokenize(code);

            // Analyze tokens (classify and print)
            foreach (var tok in tokens)
                CheckLexicalAnalyzer(tok);

            // Update token count label
            tokenCountLabel.Text = $"Total Tokens: {tokenCount}";
        }

        // Remove comments while preserving string/char literals
        private string RemoveComments(string code)
        {
            if (string.IsNullOrEmpty(code))
                return code;

            var sb = new StringBuilder(code.Length);
            int i = 0;
            while (i < code.Length)
            {
                // If we encounter a string or char literal, copy it verbatim (including escaped quotes) to avoid stripping comment markers inside it
                if (code[i] == '"' || code[i] == '\'')
                {
                    char quote = code[i];
                    sb.Append(code[i++]);
                    while (i < code.Length)
                    {
                        // copy char
                        if (code[i] == '\\' && i + 1 < code.Length)
                        {
                            // escaped sequence, copy both
                            sb.Append(code[i]);
                            sb.Append(code[i + 1]);
                            i += 2;
                            continue;
                        }

                        sb.Append(code[i]);
                        if (code[i] == quote)
                        {
                            i++;
                            break;
                        }
                        i++;
                    }
                    continue;
                }

                // Single-line comment //
                if (i + 1 < code.Length && code[i] == '/' && code[i + 1] == '/')
                {
                    // skip until end of line or end of file
                    i += 2;
                    while (i < code.Length && code[i] != '\n')
                        i++;
                    continue;
                }

                // Multi-line comment /* ... */
                if (i + 1 < code.Length && code[i] == '/' && code[i + 1] == '*')
                {
                    i += 2;
                    while (i + 1 < code.Length)
                    {
                        if (code[i] == '*' && code[i + 1] == '/')
                        {
                            i += 2;
                            break;
                        }
                        i++;
                    }
                    continue;
                }

                // Python single-line comment starting with #
                // This is checked here (after literal handling) so '#' inside a string is preserved.
                if (code[i] == '#')
                {
                    // skip until end of line or end of file
                    i++;
                    while (i < code.Length && code[i] != '\n')
                        i++;
                    continue;
                }

                // otherwise copy char
                sb.Append(code[i]);
                i++;
            }

            return sb.ToString();
        }

        // Single-pass tokenizer: preserves strings (with spaces/newlines), handles unclosed quotes as an error token
        private List<string> Tokenize(string code)
        {
            var result = new List<string>();
            int i = 0;
            int n = code.Length;

            while (i < n)
            {
                char c = code[i];

                // Whitespace (including newlines) - skip as delimiter
                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                // String or char literal
                if (c == '"' || c == '\'')
                {
                    char quote = c;
                    var sb = new StringBuilder();
                    sb.Append(quote);
                    i++;
                    bool closed = false;

                    while (i < n)
                    {
                        // support escaped characters
                        if (code[i] == '\\' && i + 1 < n)
                        {
                            sb.Append(code[i]);
                            sb.Append(code[i + 1]);
                            i += 2;
                            continue;
                        }

                        sb.Append(code[i]);

                        if (code[i] == quote)
                        {
                            i++;
                            closed = true;
                            break;
                        }

                        i++;
                    }

                    if (!closed)
                    {
                        // Unterminated string/char literal -> mark as error token (preserve content)
                        result.Add(sb.ToString()); // content without closing quote
                        // We'll let CheckLexicalAnalyzer detect/print an error message for unterminated strings
                    }
                    else
                    {
                        result.Add(sb.ToString());
                    }

                    continue;
                }

                // Number literal (integer or float) - support forms like 123, 123.45, .5, 1e-3, trailing suffix f/F/d/D/m/M
                if (char.IsDigit(c) || (c == '.' && i + 1 < n && char.IsDigit(code[i + 1])))
                {
                    var sbNum = new StringBuilder();
                    bool hasDot = false;
                    bool hasExp = false;

                    // leading dot (e.g. .5)
                    if (c == '.')
                    {
                        hasDot = true;
                        sbNum.Append('.');
                        i++;
                    }

                    // main loop: digits, optional single dot, optional exponent
                    while (i < n)
                    {
                        char cur = code[i];

                        if (char.IsDigit(cur))
                        {
                            sbNum.Append(cur);
                            i++;
                            continue;
                        }

                        // decimal point
                        if (cur == '.' && !hasDot && !hasExp && i + 1 < n && char.IsDigit(code[i + 1]))
                        {
                            hasDot = true;
                            sbNum.Append(cur);
                            i++;
                            continue;
                        }

                        // exponent part
                        if ((cur == 'e' || cur == 'E') && !hasExp)
                        {
                            hasExp = true;
                            sbNum.Append(cur);
                            i++;
                            // optional sign after exponent
                            if (i < n && (code[i] == '+' || code[i] == '-'))
                            {
                                sbNum.Append(code[i]);
                                i++;
                            }
                            // digits after exponent
                            bool expDigits = false;
                            while (i < n && char.IsDigit(code[i]))
                            {
                                expDigits = true;
                                sbNum.Append(code[i]);
                                i++;
                            }
                            // if there were no digits after exponent, break (we keep what we have; parser may flag)
                            if (!expDigits)
                                break;
                            continue;
                        }

                        break;
                    }

                    // optional suffix characters commonly used in C# (f, F, d, D, m, M)
                    if (i < n)
                    {
                        char suf = code[i];
                        if (suf == 'f' || suf == 'F' || suf == 'd' || suf == 'D' || suf == 'm' || suf == 'M')
                        {
                            sbNum.Append(suf);
                            i++;
                        }
                    }

                    result.Add(sbNum.ToString());
                    continue;
                }

                // Operators (try to match longest operator at position)
                bool matchedOperator = false;
                foreach (var op in sortedOperators)
                {
                    if (i + op.Length <= n && code.Substring(i, op.Length) == op)
                    {
                        result.Add(op);
                        i += op.Length;
                        matchedOperator = true;
                        break;
                    }
                }
                if (matchedOperator)
                    continue;

                // Separators (single-char)
                if (separatorChars.Contains(c))
                {
                    result.Add(c.ToString());
                    i++;
                    continue;
                }

                // Identifier / other token: consume until whitespace or operator/separator/quote
                var tokenSb = new StringBuilder();
                while (i < n)
                {
                    char cur = code[i];

                    // break on whitespace, separators, quotes, or start of any operator
                    if (char.IsWhiteSpace(cur) || cur == '"' || cur == '\'' || separatorChars.Contains(cur))
                        break;

                    // detect operator start: check if any operator begins here (we only need to check first char)
                    bool opStarts = false;
                    foreach (var op in sortedOperators)
                    {
                        if (op.Length > 0 && cur == op[0])
                        {
                            // if the full operator matches ahead we should stop identifier here
                            if (i + op.Length <= n && code.Substring(i, op.Length) == op)
                            {
                                opStarts = true;
                                break;
                            }
                        }
                    }
                    if (opStarts)
                        break;

                    tokenSb.Append(cur);
                    i++;
                }

                if (tokenSb.Length > 0)
                    result.Add(tokenSb.ToString());
                else
                {
                    // If we get here, consume one char to avoid infinite loop
                    result.Add(code[i].ToString());
                    i++;
                }
            }

            return result;
        }

        // Classify token and print to outputBox similar to original behaviour.
        // This also handles unterminated strings as an error.
        private void CheckLexicalAnalyzer(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return;

            // Unterminated string/char (starts with quote but does not end with same quote)
            if ((str.StartsWith("\"") && !str.EndsWith("\"")) || (str.StartsWith("'") && !str.EndsWith("'")))
            {
                outputBox.AppendText($" (UnterminatedLiteral, <{EscapeForDisplay(str)}>)\n");
                tokenCount++;
                return;
            }

            // Integer
            if (Int32.TryParse(str, out _))
            {
                outputBox.AppendText($" (integerValue, <{str}>)\n");
                tokenCount++;
                return;
            }

            // Float / Real numbers (detect decimals, exponent, and common suffixes f/F/d/D/m/M)
            if (IsFloatLiteral(str))
            {
                outputBox.AppendText($" (floatValue, <{str}>)\n");
                tokenCount++;
                return;
            }

            // Operators
            if (sortedOperators.Contains(str))
            {
                outputBox.AppendText($" (operators, <{str}>)\n");
                tokenCount++;
                return;
            }

            // Separators
            if (separators.Contains(str))
            {
                outputBox.AppendText($" (separator, <{str}>)\n");
                tokenCount++;
                return;
            }

            // Keywords
            if (keywords.Contains(str))
            {
                outputBox.AppendText($" (keywords, <{str}>)\n");
                tokenCount++;
                return;
            }

            // String literal (closed)
            if (str.Length >= 2 && str.StartsWith("\"") && str.EndsWith("\""))
            {
                outputBox.AppendText($" (String, <{EscapeForDisplay(str)}>)\n");
                tokenCount++;
                return;
            }

            // Char literal (closed)
            if (str.Length >= 2 && str.StartsWith("'") && str.EndsWith("'"))
            {
                outputBox.AppendText($" (Char, <{EscapeForDisplay(str)}>)\n");
                tokenCount++;
                return;
            }

            // Default: identifier
            outputBox.AppendText($" (identifier, <{str}>)\n");
            tokenCount++;
        }

        // Helper: determine if token is a floating literal (supports 123.45, .5, 1e-3, trailing f/F/d/D/m/M)
        private bool IsFloatLiteral(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            string body = s;
            char suffix = '\0';
            if (s.Length > 1)
            {
                char last = s[s.Length - 1];
                if (last == 'f' || last == 'F' || last == 'd' || last == 'D' || last == 'm' || last == 'M')
                {
                    suffix = last;
                    body = s.Substring(0, s.Length - 1);
                    if (body.Length == 0)
                        return false;
                }
            }

            // Try double parse for typical float/double formats
            double d;
            if (double.TryParse(body, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
                return true;

            // If suffix is decimal 'm' try decimal parse
            if ((suffix == 'm' || suffix == 'M') && decimal.TryParse(body, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
                return true;

            return false;
        }

        // Simple helper to make newlines/returns visible in outputBox display for literals
        private string EscapeForDisplay(string s)
        {
            return s.Replace("\r", "\\r").Replace("\n", "\\n");
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