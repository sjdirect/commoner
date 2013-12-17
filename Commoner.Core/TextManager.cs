using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Commoner.Core
{
    public interface ITextManager
    {
        /// <summary>
        /// Creates a string from the words
        /// </summary>
        string GetTextFromWords(IList<string> words);

        /// <summary>
        /// Creates a list of words from a string
        /// </summary>
        IList<string> GetWordsFromText(string text);

        /// <summary>
        /// Remove stop words from the text
        /// </summary>
        string RemoveStopWords(string text);

        /// <summary>
        /// Remove stop words from the words
        /// </summary>
        IList<string> RemoveStopWords(IList<string> words);

        /// <summary>
        /// Removes all Removable param items from text
        /// </summary>
        string Remove(string text, Removable item);

        /// <summary>
        /// Removes all Removable param items from text
        /// </summary>
        IList<string> Remove(IList<string> words, Removable item);
    }

    [Flags]
    public enum Removable
    {
        All = 1,
        ExtraSpaces = 2,
        Html = 4,
        IllegalXmlChars = 8,
        NewLineCharacters = 16,
        NoBreakSpace = 32,
        Possesion = 64,
        SpecialChars = 128,
        SpecialCharOnlyWords = 256
    }

    public class TextManager : ITextManager
    {
        readonly IList<string> _stopWords = new List<string> { "a", "am", "an", "and", "are", "as", "at", "be", "by", "com", "de", "en", "for", "from", "how", "in", "is", "it", "la", "of", "on", "or", "that", "the", "this", "to", "was", "what", "when", "where", "who", "will", "with", "www", "we", "you", "your", "me", "mine", "my", "i" };
        readonly IList<string> _specialCharacters = new List<string> { "\n", "\r", "\t", "&nbsp;", "&amp;", "\"", ",", ";", ":", ".", "(", ")", "[", "]", "{", "}", "+", ".", "=", "\\", "/", "“", "”", "<", ">", "|", "*", "&", "^", "$", "#", "@", "!", "?", "%", "'", "`", "‘", "’" };

        public string GetTextFromWords(IList<string> words)
        {
            CheckWords(words, "words");

            StringBuilder builder = new StringBuilder();
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    builder.Append(word);
                    builder.Append(" ");
                }
            }

            return builder.ToString().Trim();
        }

        public IList<string> GetWordsFromText(string text)
        {
            CheckText(text, "text");

            List<string> words = new List<string>(text.Trim().Split(' '));
            return (words.Where(w => !string.IsNullOrWhiteSpace(w))).ToList();
        }

        public string RemoveStopWords(string text)
        {
            CheckText(text, "text");
            return RemoveWords(text, _stopWords);
        }

        public IList<string> RemoveStopWords(IList<string> words)
        {
            CheckWords(words, "words");
            return GetWordsFromText(RemoveWords(GetTextFromWords(words), _stopWords));
        }

        public string Remove(string text, Removable item)
        {
            CheckText(text, "text");

            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.Html))
                text = RemoveHtml(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.NoBreakSpace))
                text = RemoveNoBreakSpace(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.Possesion))
                text = TransformPossession(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.IllegalXmlChars))
                text = RemoveIllegalXmlCharacters(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.SpecialChars))
                text = RemoveSpecialCharacters(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.SpecialCharOnlyWords))
                text = RemoveSpecialCharacterOnlyWords(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.NewLineCharacters))
                text = RemoveNewLineCharacters(text);
            if (item.HasFlag(Removable.All) || item.HasFlag(Removable.ExtraSpaces))
                text = RemoveExtraSpaces(text);

            return text;
        }

        public IList<string> Remove(IList<string> words, Removable item)
        {
            CheckWords(words, "words");

            List<string> cleanText = new List<string>();
            foreach (string word in words)
                cleanText.Add(Remove(word, item));

            return cleanText;
        }

        private string RemoveExtraSpaces(string text)
        {
            CheckText(text, "text");
            return Regex.Replace(text, @"\s+", " ").Trim();
        }

        private string RemoveHtml(string text)
        {
            CheckText(text, "text");

            //Replacing with " " to avoid.. <div>aaa</div><div>bbbb</div> ===> aaabbbb
            return Regex.Replace(text, "<.*?>", " ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }

        private string RemoveIllegalXmlCharacters(string text)
        {
            StringBuilder cleanText = new StringBuilder();
            foreach (char c in text)
            {
                if (IsLegalXmlChar(c))
                    cleanText.Append(c);
            }

            return cleanText.ToString();
        }

        private string RemoveSpecialCharacters(string text)
        {
            foreach (string specialChar in _specialCharacters)
                text = text.Replace(specialChar, " ");

            return text;
        }

        private string RemoveSpecialCharacterOnlyWords(string text)
        {
            CheckText(text, "text");

            IList<string> words = GetWordsFromText(text);

            List<string> wordList = new List<string>();
            foreach (string word in words)
            {
                bool wordIsSpecialCharsOnly = true;
                foreach (char c in word.ToCharArray())
                {
                    if (char.IsLetterOrDigit(c))
                        wordIsSpecialCharsOnly = false;
                }

                if (!wordIsSpecialCharsOnly)
                {
                    wordList.Add(word);
                }
            }

            return GetTextFromWords(wordList);
        }

        private string RemoveWords(string text, IList<string> wordsToRemove)
        {
            string newString = text;
            foreach (string wordToRemove in wordsToRemove)
            {
                newString = Regex.Replace(newString, @"\b" + wordToRemove + @"\b", "", RegexOptions.IgnoreCase);
            }

            return RemoveExtraSpaces(newString);
        }

        private string RemoveNewLineCharacters(string text)
        {
            string replaceWith = " ";
            return text.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
        }

        private string RemoveNoBreakSpace(string text)
        {
            return text.Replace("&nbsp;", " ");
        }

        private string TransformPossession(string text)
        {
            string transformedText = text;

            ////Add tokens for "it's" and "what's" so they are not considered possesive
            //string itsToken = "@@it@@";
            //string whatsToken = "@@what@@";
            //string excludeWordFormatString = "(^|\\s({0}['`’]s))|(\\s+{0}['`’]s)";
            //transformedText = Regex.Replace(transformedText, string.Format(excludeWordFormatString, "it"), itsToken, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //transformedText = Regex.Replace(transformedText, string.Format(excludeWordFormatString, "what"), whatsToken, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //Remove possession
            transformedText = Regex.Replace(transformedText, "(['`’]s)", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            transformedText = Regex.Replace(transformedText, "(s['`’])", "s", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //Re-add "it's" and "what's"
            //Regex itsTokenPattern = new Regex("@@it@s", RegexOptions.Compiled);
            //Regex whatsTokenPattern = new Regex("(@@what@s)", RegexOptions.Compiled);
            //transformedText = itsTokenPattern.Replace(transformedText, "it's");
            //transformedText = whatsPattern.Replace(transformedText, "whats's");

            return transformedText;
        }

        private bool IsLegalXmlChar(int character)
        {
            return (
            character == 0x9 /* == '\t' == 9   */          ||
            character == 0xA /* == '\n' == 10  */          ||
            character == 0xD /* == '\r' == 13  */          ||
            (character >= 0x20 && character <= 0xD7FF) ||
            (character >= 0xE000 && character <= 0xFFFD) ||
            (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        private void CheckText(string text, string name)
        {
            if (text == null)
                throw new ArgumentNullException(name);
        }

        private void CheckWords(IList<string> words, string fieldName)
        {
            if (words == null)
                throw new ArgumentNullException(fieldName);
        }
    }
}
