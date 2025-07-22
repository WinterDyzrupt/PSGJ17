using System;
using UnityEngine;

namespace PersistentData
{
    [Serializable]
    public class IntReference
    {
        public bool useConstant = true;
        public int constantValue;
        public IntVariable variable;

        public int Value
        {
            get => useConstant ? constantValue : variable?.value ?? 0;
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
            constantValue = 0;
            variable?.ResetValue();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
