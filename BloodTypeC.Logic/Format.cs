using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodTypeC.Logic
{
    public class Format
    {
        public enum CapitalsOptions
        {
            FirstWord = 1,
            EachWord = 2,
        }

        public static string AsNameOrTitle(string name, CapitalsOptions capsOpt, bool alphabetDashOnly)
        {
            // Remove diacritics
            name = new String(name.Normalize(NormalizationForm.FormD)
                        .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            name = name.Replace("Ł", "L");
            name = name.Replace("ł", "l");
            // Remove all chars that are not a dash, space or letter
            if (alphabetDashOnly)
            {
                name = Regex.Replace(name, "[^A-Z^a-z -]", "");
                name = Regex.Replace(name, @"\s+", " ");
                name = Regex.Replace(name.Trim(), @"-+", "-");

                // Remove dash if it's the first or last character
                name = name.IndexOf("-") == 0 ? name.Substring(1) : name;
                if (name.Contains("-") && name.Length > 1)
                {
                    name = name.LastIndexOf("-") == name.Length - 1 ? name.Remove(name.Length - 1) : name;
                }
            }
            name = Regex.Replace(name, @"\s+", " ");

            switch (capsOpt)
            {
                case CapitalsOptions.FirstWord:
                    name = Char.ToUpper(name[0]) + name.Substring(1);
                    break;
                case CapitalsOptions.EachWord:
                    name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
                    break;
            }
            return name.Trim();
        }

        public static List<string> AsTags(string tagsInput)
        {
            // Remove any multiple spaces and change the input of tags separated by commas
            // and/or spaces into a list of lowercase tags.
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
            // Replace commas with a dot and remove any unnecessary characters, then parse
            // the input into a correct value.
            valueInput = valueInput.Replace(".", ",");
            valueInput = Regex.Replace(valueInput, @"\.+", ",");
            valueInput = Regex.Replace(valueInput, "[^0-9,]", "");
            double value = 0;
            double.TryParse(valueInput, out value);
            if (value > maxValue)
            {
                Console.WriteLine($"Did you mean {value}? It exceeds the accepted maximum!");
                value = 0;
            }
            return Math.Round(value, 2);
        }
    }
}
