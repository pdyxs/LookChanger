using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using PDYXS.ThingSpawner;

namespace PDYXS.Skins
{
    public class SkinnedObjectParent : MonoBehaviour
    {
        [HideInInspector]
        public string objectName;

        public bool automaticallyInitialise = false;

        public Component initialisingObject;
        [HideInInspector]
        public string initialisingObjectProperty;
        [HideInInspector]
        public string initialisingObjectField;

        private SkinnedObject skinnedObject;

        private delegate void ListenerFunc();
        private UnityAction listenerFunc = null;

        [SerializeField]
        private PrefabSaver saver;

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
            foreach (Transform obj in transform)
            {
                DestroyImmediate(obj.gameObject);
            }
            if (automaticallyInitialise)
            {
                StartCoroutine(WaitToInitialise());
            }
        }

        private IEnumerator WaitToInitialise() {
            yield return new WaitForEndOfFrame();
            if (initialisingObject != null)
            {
                if (initialisingObjectProperty != "")
                {
                    var field = initialisingObject.GetType().GetProperty(initialisingObjectProperty);
                    if (field != null)
                    {
                        Init(field.GetValue(initialisingObject));
                    }
                }
                else
                {
                    Init(initialisingObject);
                }
            }
            else
            {
                Init();
            }
        }

#if UNITY_EDITOR
        [ExecuteInEditMode]
        private void Awake()
        {
            if (!Application.isPlaying)
            {
                EditorBuild();
            }
        }

        public void EditorBuild() {
            var skinManager = FindObjectOfType<SkinManager>();
            if (skinManager != null)
            {
                EditorBuild(skinManager.Skin);
            }
        }

        public void EditorBuild(Skin skin) {
            foreach (Transform obj in transform) {
                DestroyImmediate(obj.gameObject);
            }

            if (skin != null && skin.prefabs[objectName] != null) {
                var obj = UnityEditor.PrefabUtility.InstantiatePrefab(
                    skin.prefabs[objectName]
                ) as MonoBehaviour;
                obj.transform.SetParent(transform);
                obj.transform.localScale = Vector3.one;
                obj.gameObject.hideFlags = HideFlags.DontSaveInBuild;
            }
        }
#endif

        public void Init()
        {
            BuildObject();
            listenerFunc = Replace;
            SkinManager.instance.OnSkinChanged.AddListener(listenerFunc);
        }

        public void Init(object obj)
        {
            BuildObject(obj);
            listenerFunc = () =>
            {
                Replace(obj);
            };
            SkinManager.instance.OnSkinChanged.AddListener(listenerFunc);
        }

        public void Init(object obj0, object obj1)
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

        public void Replace(object obj)
        {
            skinnedObject.Recycle();
            BuildObject(obj);
        }

        public void Replace(object obj0, object obj1)
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

        private void BuildObject(object obj) {
            DoSpawn();
            skinnedObject.Init(obj);
        }

        private void BuildObject(object obj0, object obj1)
        {
            DoSpawn();
            skinnedObject.Init(obj0, obj1);
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