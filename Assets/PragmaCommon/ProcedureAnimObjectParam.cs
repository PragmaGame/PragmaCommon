using System;
using UnityEngine;

namespace Pragma.Common
{
    [Serializable]
    public class ProcedureAnimObjectParam<T> : ProcedureAnimParam<T> where T : struct
    {
        public Transform animObject;
    }
}