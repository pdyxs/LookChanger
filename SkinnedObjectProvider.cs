using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PDYXS.Skins
{
    public class SkinnedObjectProvider<T> where T : MonoBehaviour
    {
        private T _object;
        public T obj {
            get {
                if (_object == null) {
                    _object = behaviour.GetComponent<T>();
                }
                if (_object == null) {
                    _object = behaviour.GetComponentInChildren<T>();
                }
                return _object;
            }
        }

        public SkinnedObjectProvider(MonoBehaviour b) {
            behaviour = b;
            var skinnedObjectParent = behaviour.GetComponent<SkinnedObjectParent>();
            if (skinnedObjectParent != null) {
                SkinManager.instance.OnSkinChanged.AddListener(OnSkinChanged);
            }
        }

        private void OnSkinChanged()
        {
            _object = null;
        }

        private MonoBehaviour behaviour;
    }

    public static class SkinnedObjectGetter {
        public static SkinnedObjectProvider<T> Provide<T>(this MonoBehaviour b) 
            where T : MonoBehaviour 
        {
            return new SkinnedObjectProvider<T>(b);
        }
    }
}