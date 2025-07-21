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

        public int Value => useConstant ? constantValue : variable.value;
    }
}
