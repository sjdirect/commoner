using System;
using System.Collections.Generic;
using Commoner.Core;
using NUnit.Framework;

namespace Common.Test.Unit.Core
{
    [TestFixture]
    public class TimeoutTimerTest
    {
        [Test]
        public void Start_CallsBackAfterTimeout()
        {
            TimeoutTimer<int> uut = new TimeoutTimer<int>(25, 999);
            uut.Elapsed += (sender, e) => 

            Assert.AreEqual("Here it's   a  set of <b>words</b>", result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTextFromWords_NullWords_ThrowsException()
        {
            _unitUnderTest.GetTextFromWords(null);
        }

        [Test]
        public void GetTextFromWords_EmptyWords_ReturnsEmptyText()
        {
            Assert.AreEqual("", _unitUnderTest.GetTextFromWords(new List<string>()));
        }


        [Test]
        public void GetWordsFromText_ValidText_ReturnsWords()
        {
            string input = "Here it's a set of <b>words</b>";
            IList<string> result = _unitUnderTest.GetWordsFromText(input);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual("Here", result[0]);
            Assert.AreEqual("it's", result[1]);
            Assert.AreEqual("a", result[2]);
            Assert.AreEqual("set", result[3]);
            Assert.AreEqual("of", result[4]);
            Assert.AreEqual("<b>words</b>", result[5]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWordsFromText_NullText_ThrowsException()
        {
            _unitUnderTest.GetWordsFromText(null);
        }

        [Test]
        public void GetWordsFromText_EmptyText_ReturnsEmptyWords()
        {
            Assert.AreEqual(0, _unitUnderTest.GetWordsFromText("").Count);
        }


        [Test]
        public void Remove_FromText_ExtraSpaces_Removes()
        {
            string input = "   A   fox ran        across     ";
            string expected = "A fox ran across";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.ExtraSpaces));
        }

        [Test]
        public void Remove_FromText_Html_Removes()
        {
            string input = "<h3>A </h3> fox <div>ran</div>   <p>across";
            string expected = " A   fox  ran     across";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.Html));

            //No close to h3
            input = "<h3 A </h3> fox <div>ran</div>   <p>across";
            expected = "  fox  ran     across";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.Html));
        }

        [Test]
        public void Remove_FromText_IllegalXmlCharacters_Removes()
        {
            char[] chars = new char[] { 'a', '\x00', '\x01', '\x02', '\x03', '\x04', '\x05', '\x06', '\x07', 'b', '\x08', '\x0B', '\x0E', '\x0F', '\x10', '\x11', '\x12', '\x13', '\x14', '\x15', '\x1A', '\x1B', '\x1C', '\x1D', '\x1E', '\x1F', '\x16', '\x17', '\x18', '\x19', '\x7F', 'c' };
            string input = string.Join(" ", chars);
            string expected = "a         b                      c";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.IllegalXmlChars));
        }

        [Test]
        public void Remove_FromText_Possesion_Removes()
        {
            //Singular possesion
            string input = "A Randy's Randy`s, Randy’s,, RANDY'S RANDY`S, RANDY’S,, Randy'sss Randy'sddd wasn't";
            string expected = "A Randy Randy, Randy,, RANDY RANDY, RANDY,, Randyss Randyddd wasn't";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.Possesion));

            //Plural possesion
            input = "A Jones' Jones`, Jones’,, JONES' JONES`, JONES’,,";
            expected = "A Jones Jones, Jones,, JONEs JONEs, JONEs,,";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.Possesion));

            ////Exceptions to the rule 
            //input = "it's it`s, it’s,, IT'S IT`S, IT’S,, what's what`s, what’s,, WHAT'S WHAT`S, WHAT’S,,";
            //expected = "it's it`s, it’s,, IT'S IT`S, IT’S,, what's what`s, what’s,, WHAT'S WHAT`S, WHAT’S,,";
            //Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.Possesion));
        }

        [Test]
        public void Remove_FromText_SpecialCharOnlyWords_Removes()
        {
            string input = "it's a -- --- & **";
            string expected = "it's a";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.SpecialCharOnlyWords));
        }

        [Test]
        public void Remove_FromText_SpecialChars_Removes()
        {
            string input = "\n\r\t&nbsp;&amp;\",;:.()[]{}+.=\\/“”<>|*&^$#@!?%'`‘’-";
            string expected = "                                       -";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.SpecialChars));
        }

        [Test]
        public void Remove_FromText_NewLineCharacters_Removes()
        {
            string input = "Line 1\r\nLine 2\n\rLine 3\rLine 4\n";
            string expected = "Line 1 Line 2  Line 3 Line 4 ";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.NewLineCharacters));

            input = "&lt;img src=\"images/PostHeaderIcon.png\" width=\"26\" height=\"28\" alt=\"PostHeaderIcon\" /&gt;";
            expected = "&lt;img src=\"images/PostHeaderIcon.png\" width=\"26\" height=\"28\" alt=\"PostHeaderIcon\" /&gt;";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.NewLineCharacters));
        }

        [Test]
        public void Remove_FromText_NewBreakSpace_Removes()
        {
            string input = "a&nbsp;b &nbsp;&nbsp; c &nbsp; d";
            string expected = "a b    c   d";
            Assert.AreEqual(expected, _unitUnderTest.Remove(input, Removable.NoBreakSpace));
        }

        [Test]
        public void Remove_FromText_MultipleRemovables_Removes()
        {
            string dirtyText = "Randy's, fox<div>ran:</div><div>[down]</div> it's&nbsp;   <p>old-weathered, -- (road). Is that ok>?";
            string cleanText = "Randy fox ran down it old-weathered road Is that ok";

            //Set all flags
            Assert.AreEqual(cleanText, _unitUnderTest.Remove(dirtyText, Removable.ExtraSpaces | Removable.Html | Removable.IllegalXmlChars | Removable.Possesion | Removable.SpecialCharOnlyWords | Removable.SpecialChars));

            //Use short hand notation for all flags
            Assert.AreEqual(cleanText, _unitUnderTest.Remove(dirtyText, Removable.All));

            //Set only 2 flags
            Assert.AreEqual("it steven house <div></div>", _unitUnderTest.Remove("it's steven's       house <div></div>   ", Removable.ExtraSpaces | Removable.Possesion));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_FromText_NullText_ThrowExcption()
        {
            string nullText = null;
            _unitUnderTest.Remove(nullText, Removable.SpecialChars);
        }

        [Test]
        public void Remove_FromText_EmptyText_ReturnsEmpty()
        {
            Assert.AreEqual("", _unitUnderTest.Remove("", Removable.SpecialChars));
        }

        [Test]
        public void Remove_FromWords_MultipleRemovables_Removes()
        {
            IList<string> dirtyWords = new List<string> { "Randy's,", "fox<div>ran:</div><div>[down]</div>", "it's&nbsp;", "  ", "<p>old-weathered,", "--", "(road).", "Is", "that", "ok>?" };

            //Set all flags
            IList<string> result = _unitUnderTest.Remove(dirtyWords, Removable.ExtraSpaces | Removable.Html | Removable.IllegalXmlChars | Removable.Possesion | Removable.NoBreakSpace | Removable.SpecialCharOnlyWords | Removable.SpecialChars);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual("Randy", result[0]);
            Assert.AreEqual("fox ran down", result[1]);
            Assert.AreEqual("it", result[2]);
            Assert.AreEqual("", result[3]);
            Assert.AreEqual("old-weathered", result[4]);
            Assert.AreEqual("", result[5]);
            Assert.AreEqual("road", result[6]);
            Assert.AreEqual("Is", result[7]);
            Assert.AreEqual("that", result[8]);
            Assert.AreEqual("ok", result[9]);

            //Use short hand notation for all flags
            result = _unitUnderTest.Remove(dirtyWords, Removable.All);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual("Randy", result[0]);
            Assert.AreEqual("fox ran down", result[1]);
            Assert.AreEqual("it", result[2]);
            Assert.AreEqual("", result[3]);
            Assert.AreEqual("old-weathered", result[4]);
            Assert.AreEqual("", result[5]);
            Assert.AreEqual("road", result[6]);
            Assert.AreEqual("Is", result[7]);
            Assert.AreEqual("that", result[8]);
            Assert.AreEqual("ok", result[9]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_FromWords_NullText_ThrowExcption()
        {
            IList<string> nullList = null;
            _unitUnderTest.Remove(nullList, Removable.SpecialChars);
        }

        [Test]
        public void Remove_FromWords_EmptyList_ReturnsEmpty()
        {
            IList<string> emptyList = new List<string>();
            Assert.AreEqual(0, _unitUnderTest.Remove(emptyList, Removable.SpecialChars).Count);
        }


        [Test]
        public void RemoveStopWords_ValidText_WordsRemoved()
        {
            string phraseWithStopWords = "a am an and TEST 1 are as at be by com de en for TEST   2 from how in is it la of on or that the this to was what when where who will TEST 3 with www we you your me mine my i";
            Assert.AreEqual("TEST 1 TEST 2 TEST 3", _unitUnderTest.RemoveStopWords(phraseWithStopWords));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveStopWords_NullText_ThrowsException()
        {
            string nullText = null;
            _unitUnderTest.RemoveStopWords(nullText);
        }

        [Test]
        public void RemoveStopWords_EmptyText_ReturnsEmptyText()
        {
            Assert.AreEqual("", _unitUnderTest.RemoveStopWords(""));
        }

        [Test]
        public void RemoveStopWords_ValidWords_WordsRemoved()
        {
            IList<string> words = new List<string> { "a", " am an and TEST 1", "are as at be by com de en for TEST   2 from", "how in is it la of on or that the this to was what when where who will TEST 3 with www we you your me mine my i" };
            IList<string> result = _unitUnderTest.RemoveStopWords(words);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual("TEST", result[0]);
            Assert.AreEqual("1", result[1]);
            Assert.AreEqual("TEST", result[2]);
            Assert.AreEqual("2", result[3]);
            Assert.AreEqual("TEST", result[4]);
            Assert.AreEqual("3", result[5]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveStopWords_NullWords_ThrowsException()
        {
            IList<string> nullWords = null;
            _unitUnderTest.RemoveStopWords(nullWords);
        }

        [Test]
        public void RemoveStopWords_EmptyWords_ReturnsEmptyWords()
        {
            Assert.AreEqual(0, _unitUnderTest.RemoveStopWords(new List<string>()).Count);
        }


        [Test]
        public void TidyHtml_HtmlToBadToFix_ReturnsOriginal()
        {
            //Html to bad to fix
            string input = @"<a href=""#refs <p>no link</p>";
            Assert.AreEqual(input, _unitUnderTest.TidyHtml(input).Replace("\r\n", ""));
        }

        [Test]
        public void TidyHtml_MissingClosing_ClosedByTidy()
        {
            //Missing closing tag simple
            string input = @"<h1>heading<h2>subheading</h3>";
            string expected = @"<h1>heading</h1><h2>subheading</h2>";
            Assert.IsTrue(_unitUnderTest.TidyHtml(input).Replace("\r\n", "").Contains(expected));

            //Missing closing tag complex
            //This is an example of why we should NOT use tidy, it changes the content drastically in cases like the following
            input = "<html><title>titleval<body><h1>h1val<div>divval<p>pval";
            expected = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\"    \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>titleval</title></head><body><h1>h1val</h1><div><h1>divval</h1></div><p>pval</p></body></html>";
            string actual = _unitUnderTest.TidyHtml(input).Replace("\r\n", "");
            Assert.AreEqual(expected, actual);

            //Missing "/"
            input = @"<a href=""#refs"">References<a>";
            expected = @"<a href=""#refs"">References</a>";
            Assert.IsTrue(_unitUnderTest.TidyHtml(input).Replace("\r\n", "").Contains(expected));
        }

        [Test]
        public void TidyHtml_EmptyText_ThrowsException()
        {
            Assert.AreEqual("", _unitUnderTest.TidyHtml(""));
            Assert.AreEqual("", _unitUnderTest.TidyHtml("   "));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TidyHtml_NullText_ThrowsException()
        {
            _unitUnderTest.TidyHtml(null);
        }
    }
}