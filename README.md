# Lexical Analyzer - Coffee Edition ☕

A beautiful and enhanced lexical analyzer with a warm coffee-themed UI that tokenizes source code from multiple programming languages.

## Features

### Core Functionality
✓ **Comment Elimination** - Removes single-line (//) and multi-line (/* */) comments  
✓ **Whitespace Removal** - Eliminates blanks, tabs, and newline characters  
✓ **Token Generation** - Identifies and categorizes tokens from source code  

### Token Types Recognized
- **Keywords** - Reserved words from C#, Java, Python, JavaScript, and C++
- **Identifiers** - Variable and function names
- **Operators** - Arithmetic, logical, comparison, and assignment operators
- **Separators** - Punctuation and delimiters
- **Integer Values** - Numeric constants
- **String Literals** - Text in double quotes
- **Character Literals** - Single characters in single quotes

### Enhanced Features
- **Multi-Language Support** - Expanded keyword set covering:
  - C# (including modern keywords: async, await, record, init, with)
  - Java (extends, implements, instanceof, synchronized)
  - Python (def, lambda, pass, raise, except)
  - JavaScript (function, let, const, export, import)
  - C++ (template, typename, nullptr, friend)
  
- **Extended Operators** - Correctly handles multi-character operators as single tokens:
  - Compound assignment: `+=`, `-=`, `*=`, `/=`, `%=`
  - Increment/Decrement: `++`, `--`
  - Comparison: `==`, `!=`, `<=`, `>=`
  - Logical: `&&`, `||`
  - Bitwise shift: `<<`, `>>`
  - Null operators: `??`, `?.`
  - Pointer: `->`
- **Token Counter** - Real-time display of total tokens found
- **File Type Filtering** - Browse dialog supports .txt, .cs, .java, .py, .js, .cpp files
- **Clear Function** - Quick reset button to clear all fields

## UI Design - Coffee Theme 🎨

The interface features a warm, professional coffee color palette:
- **Background**: Soft cream (#EBD CC8)
- **Input/Output Areas**: Light latte (#FAF5EB)
- **Primary Buttons**: Rich espresso (#654321)
- **Secondary Buttons**: Mocha brown (#8B5A3C)
- **Text**: Dark roast (#4A3628)

Clean, modern layout with:
- Larger, more readable text areas
- Flat design with subtle borders
- Clear section labels
- Centered window on startup
- Professional Segoe UI and Consolas fonts

## How to Use

1. **Browse File** - Click to select a source code file
2. **Edit Code** - Optionally modify the code in the left panel
3. **Analyze** - Click to tokenize the source code
4. **View Tokens** - Results appear in the right panel with total count displayed
5. **Clear** - Reset all fields to start fresh

## Supported File Types
- Text Files (*.txt)
- C# Files (*.cs)
- Java Files (*.java)
- Python Files (*.py)
- JavaScript Files (*.js)
- C++ Files (*.cpp)
- All Files (*.*)

## Technical Details

**Language**: C# (.NET Framework 4.7.2)  
**UI Framework**: Windows Forms  
**Architecture**: Single-form application with modular token parsing

## Example Output

```
(keywords, <public>)
(keywords, <class>)
(identifier, <MyClass>)
(separator, <{>)
(keywords, <int>)
(identifier, <count>)
(operators, <=>)
(integerValue, <0>)
(separator, <;>)
(separator, <}>)
```

---

**Note**: This is an educational project designed to demonstrate lexical analysis concepts in a clean, user-friendly interface.
