using System;
using UnityEngine;

namespace PersistentData
{
    [Serializable]
    public class FloatReference
    {
        public bool useConstant = true;
        public float constantValue;
        public FloatVariable variable;

        public float Value => useConstant ? constantValue : variable.value;
    }
}
