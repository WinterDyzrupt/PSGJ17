using System;
using System.Globalization;
using UnityEngine;

namespace PersistentData
{
    [Serializable]
    public class FloatReference
    {
        public bool useConstant = true;
        public float constantValue;
        public FloatVariable variable;

        public FloatReference()
        {
        }

        public FloatReference(float value)
        {
            useConstant = true;
            Value = value;
        }

        public float Value
        {
            get => useConstant ? constantValue : variable?.value ?? 0f;
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
            constantValue = 0f;
            variable?.ResetValue();
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
        
        public static implicit operator float(FloatReference floatReference) => floatReference.Value;
    }
}
