using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        private readonly string _value;

        internal ClassifiedAdText(string value)
        {
            if (value.Length > 100)
            {
                throw new ArgumentException("Text cannot be longer that 100 characters", nameof(value));
            }

            _value = value;

        }

        public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

        public static implicit operator string(ClassifiedAdText self) => self._value;




    }
}
