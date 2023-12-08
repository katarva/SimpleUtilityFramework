using System.Collections;
using UnityEngine;

namespace Main.Core
{
    /// <summary>
    /// Simple wrapper interface for Mono that used for coroutines
    /// </summary>
    public interface ICoroutineSource
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopAllCoroutines();
    }
}