using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QByte.View;
using UnityEngine.Events;

namespace PDYXS.Skins
{
    public class SkinnedObjectParent : MonoBehaviour
    {
        [HideInInspector]
        public string objectName;

        public bool automaticallyInitialise = false;

        public UnityEngine.Object initialisingObject;

        private SkinnedObject skinnedObject;

        private delegate void ListenerFunc();
        private UnityAction listenerFunc = null;

        private void OnDisable()
        {
            if (listenerFunc != null && SkinManager.isInitialized)
            {
                SkinManager.instance.OnSkinChanged.RemoveListener(listenerFunc);
            }
        }

        private void OnEnable()
        {
            if (listenerFunc != null)
            {
                SkinManager.instance.OnSkinChanged.AddListener(listenerFunc);
            }
        }

        protected virtual void Start()
        {
            if (automaticallyInitialise) {
                foreach (Transform child in transform)
                {
                    child.Recycle();
                }
                if (initialisingObject != null) {
                    Init(initialisingObject);
                } else {
                    Init();
                }
            }
        }

        public void Init()
        {
            BuildObject();
            listenerFunc = Replace;
            SkinManager.instance.OnSkinChanged.AddListener(listenerFunc);
        }

        public void Init<T>(T obj)
        {
            BuildObject(obj);
            listenerFunc = () =>
            {
                Replace(obj);
            };
            SkinManager.instance.OnSkinChanged.AddListener(listenerFunc);
        }

        public void Init<T, U>(T obj0, U obj1)
        {
            BuildObject(obj0, obj1);
            listenerFunc = () =>
            {
                Replace(obj0, obj1);
            };
            SkinManager.instance.OnSkinChanged.AddListener(listenerFunc);
        }

        public void Replace() {
            skinnedObject.Recycle();
            BuildObject();
        }

        public void Replace<T>(T obj)
        {
            skinnedObject.Recycle();
            BuildObject(obj);
        }

        public void Replace<T, U>(T obj0, U obj1)
        {
            skinnedObject.Recycle();
            BuildObject(obj0, obj1);
        }

        private void BuildObject() {
            DoSpawn();
            if (skinnedObject != null)
            {
                skinnedObject.Init();
            }
        }

        private void BuildObject<T>(T obj) {
            DoSpawn();
            var so = skinnedObject as SkinnedObject<T>;
            if (so != null)
            {
                so.Init(obj);
            } else {
                Debug.LogWarning("Could not initialise Skinned Object: " + gameObject.name);
            }
        }

        private void BuildObject<T, U>(T obj0, U obj1)
        {
            DoSpawn();
            var so = skinnedObject as SkinnedObject<T,U>;
            if (so != null)
            {
                so.Init(obj0, obj1);
            }
            else
            {
                Debug.LogWarning("Could not initialise Skinned Object: " + gameObject.name);
            }
        }

        private void DoSpawn() 
        {
            if (SkinManager.instance.Skin.prefabs.ContainsKey(objectName) &&
                SkinManager.instance.Skin.prefabs[objectName] != null)
            {
                skinnedObject = SkinManager.instance.Skin.prefabs[objectName].Spawn(
                    transform
                );
            }
        }

    }
}