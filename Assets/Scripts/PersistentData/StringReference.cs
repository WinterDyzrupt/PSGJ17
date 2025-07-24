using System;
using System.Globalization;
using UnityEngine;

namespace PersistentData
{
    [Serializable]
    public class StringReference
    {
        public bool useConstant = true;
        public string constantValue;
        public StringVariable variable;

        public string Value
        {
            get => useConstant ? constantValue : variable?.value ?? string.Empty;
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                }
                else
                {
                    variable.value = value;
                }
            }
        }

        public void ResetValue()
        {
            constantValue = string.Empty;
            variable?.ResetValue();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(StringReference stringReference) => stringReference.Value;
    }
}