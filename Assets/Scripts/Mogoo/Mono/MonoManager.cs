using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

namespace Mogoo.Mono
{
    public class MonoManager : Singleton<MonoManager>
    {
        private readonly MonoController _controller;

        public MonoManager()
        {
            var gameObject = new GameObject("MonoController");
            _controller = gameObject.AddComponent<MonoController>();
        }

        public void AddUpdateListener(UnityAction func)
        {
            _controller.AddUpdateListener(func);
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            _controller.RemoveUpdateListener(func);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return _controller.StartCoroutine(methodName);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return _controller.StartCoroutine(methodName, value);
        }

    }
}

