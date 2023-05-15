using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodTypeC.Logic
{
    public class Formatters
    {
        public enum CapitalsOptions
        {
            None = 0,
            FirstWord = 1,
            EachWord = 2,
        }
        /// <summary>
        /// Removes duplicate spaces (and optionally sets the first letter as capital in the first or each word).
        /// Can also remove any non-letter characters with the exception of the dash character.
        /// </summary>
        public static string AsNameOrTitle(string name, CapitalsOptions capsOpt, bool alphabetDashOnly)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = Regex.Replace(name, @"\s+", " ");

                // Remove all chars that are not a dash, space or letter
                if (alphabetDashOnly)
                {
                    name = Regex.Replace(name.ToLowerInvariant(), @"[^\w\-][0-9]", string.Empty);
                    name = Regex.Replace(name.Trim(), @"-+", "-");

                    // Remove dash if it's the first or last character
                    name = name.IndexOf("-") == 0 ? name.Substring(1) : name;
                    if (name.Contains("-") && name.Length > 1)
                    {
                        name = name.LastIndexOf("-") == name.Length - 1 ? name.Remove(name.Length - 1) : name;
                    }

                    name = Regex.Replace(name, @"\s+", " ");
                }

                switch (capsOpt)
                {
                    case CapitalsOptions.None:
                        return name;
                    case CapitalsOptions.FirstWord:
                        name = Char.ToUpper(name[0]) + name.Substring(1);
                        break;
                    case CapitalsOptions.EachWord:
                        name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
                        break;
                }
            return name.Trim();
            }
            else
            {
                return null;
            }
        }

        public static List<string> AsTags(string tagsInput)
        {
            /// <summary>
            /// Removes any multiple spaces and changes the input of tags separated by any non-letter 
            /// characters into a list of lowercase tags.
            /// </summary>
            if (string.IsNullOrWhiteSpace(tagsInput))
            {
                return new List<string>();
            }

            var tags = new List<string>();
            tagsInput = new String(tagsInput.Normalize(NormalizationForm.FormD)
                            .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            tagsInput = tagsInput.Replace("Ł", "L");
            tagsInput = tagsInput.Replace("ł", "l");
            tagsInput = Regex.Replace(tagsInput.ToLower(), "[^a-z ]", " ");
            tagsInput = Regex.Replace(tagsInput, @"\s+", " ");
            string[] tagsArray = tagsInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (string tag in tagsArray)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    tags.Add(tag);
                }
            }
            return tags;
        }

        public static double AsScoreOrABV(string valueInput, double maxValue)
        {
            /// <summary>
            /// Replaces commas with a dot and removes any unnecessary characters, then parses
            /// the input into a correct value within the given boundaries.
            /// </summary>
            valueInput = valueInput.Replace(".", ",");
            valueInput = Regex.Replace(valueInput, @"\.+", ",");
            valueInput = Regex.Replace(valueInput, "[^0-9,]", "");
            _ = double.TryParse(valueInput, out double value);
            if (value > maxValue)
            {
                value = 0;
            }
            return Math.Round(value, 2);
        }
    }
}
