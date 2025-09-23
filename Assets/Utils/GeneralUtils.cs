using System;
using System.Collections;
using UnityEngine;

namespace GeneralUtils
{
    public class ChangeCheck<T>
    {
        /// <summary>
        /// Without looping, will check if the value returned by the getter has changed since the last check.
        /// </summary>
        /// <param name="getter">the function to get the value of the field, usually () => value</param>
        public ChangeCheck(Func<T> getter)
        {
            this.getter = getter;
            oldValue = getter();
        }

        /// <summary>
        /// With Looping, will check if the value returned by the getter has changed this frame.
        /// </summary>
        /// <param name="getter">the function to get the value of the field, usually () => value</param>
        /// <param name="caller">a MonoBehaviour to run the coroutine on</param>
        /// <param name="OnChanged">function to call when the value has changed</param>
        public ChangeCheck(Func<T> getter, Action<T, T> OnChanged, MonoBehaviour caller)
        {
            this.getter = getter;
            oldValue = getter();

            this.OnChanged = OnChanged;

            this.caller = caller;
            this.coroutine = caller.StartCoroutine(Loop());
        }

        private readonly Func<T> getter;
        private T oldValue;
        private readonly Action<T, T> OnChanged;

        public bool Check()
        {
            T currentValue = getter.Invoke();

            if (!Equals(oldValue, currentValue))
            {
                OnChanged?.Invoke(oldValue, currentValue);
                oldValue = currentValue;
                return true;
            }
            return false;
        }

        private MonoBehaviour caller;
        private Coroutine coroutine;
        public void Stop()
        {
            caller.StopCoroutine(coroutine);
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                Check();
                yield return null;
            }
        }
    }
}
