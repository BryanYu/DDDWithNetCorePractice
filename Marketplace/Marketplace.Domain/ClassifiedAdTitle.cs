using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Marketplace.Domain
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        private readonly string _value;

        internal ClassifiedAdTitle(string value)
        {
            if (value.Length > 100)
            {
                throw new ArgumentException("Title cannot be longer that 100 characters", nameof(value));
            }

            _value = value;

        }

        public static ClassifiedAdTitle FromString(string title)
        {
            CheckValidity(title);
            return new ClassifiedAdTitle(title);
        }

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle.Replace("<i>", "*")
                .Replace("</i>", "*").Replace("<b>", "**")
                .Replace("</b>", "**");
            var value = Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty);
            CheckValidity(value);
            return new ClassifiedAdTitle(value);
        }
        public static implicit operator string(ClassifiedAdTitle self) => self._value;

        private static void CheckValidity(string value)
        {
            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Title cannot be longer that 100 characters");
        }

    }
}
