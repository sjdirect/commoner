using System;
using System.Text;

namespace Commoner.Core.Testing
{
    public class DataGenerator
    {
        /// <summary>
        /// Gets a randam alphanumeric string with no spaces or special chars. Only 0-9, A-Z, a-z.
        /// </summary>
        /// <param name="length">Length of the string to be created</param>
        /// <returns>The random string of the requested length</returns>
        public string GetRandomAlphaNumericString(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char charToAppend = ' ';
            for (int i = 0; i < length; i++)
            {
                switch (random.Next(0, 3))
                {
                    case 0:
                        {
                            charToAppend = Convert.ToChar(random.Next(48, 58));    //0-9
                            break;
                        }
                    case 1:
                        {
                            charToAppend = Convert.ToChar(random.Next(65, 91));    //A-Z
                            break;
                        }
                    case 2:
                        {
                            charToAppend = Convert.ToChar(random.Next(97, 123));   //a-z
                            break;
                        }
                }
                builder.Append(charToAppend);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets a randam alpha string with no spaces or special chars. Only A-Z, a-z.
        /// </summary>
        /// <param name="length">Length of the string to be created</param>
        /// <returns>The random string of the requested length</returns>
        public string GetRandomAlphaString(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char charToAppend = ' ';
            for (int i = 0; i < length; i++)
            {
                switch (random.Next(0, 2))
                {
                    case 0:
                        {
                            charToAppend = Convert.ToChar(random.Next(65, 91));    //A-Z
                            break;
                        }
                    case 1:
                        {
                            charToAppend = Convert.ToChar(random.Next(97, 123));   //a-z
                            break;
                        }
                }
                builder.Append(charToAppend);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets a random numeric string with no spaces or special chars. Only 0-9.
        /// </summary>
        /// <param name="length">Length of the string to be created</param>
        /// <returns>The random string of the requested length</returns>
        public string GetRandomNumericString(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char charToAppend = ' ';
            for (int i = 0; i < length; i++)
            {
                charToAppend = Convert.ToChar(random.Next(48, 58));    //0-9
                builder.Append(charToAppend);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets a randam integer
        /// </summary>
        public int GetRandomInteger(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
