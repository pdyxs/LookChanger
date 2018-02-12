using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PDYXS.Skins
{
    [CustomEditor(typeof(SkinnedObject), true)]
    public class SkinnedObjectEditor : Editor
    {
        public SkinnedObject skinnedObject {
            get {
                return target as SkinnedObject;
            }
        }

        public SkinnedObjectParent parent {
            get {
                if (_parent == null) {
                    var p = skinnedObject.transform.parent;
                    if (p != null)
                    {
                        _parent = p.GetComponent<SkinnedObjectParent>();
                    }
                }
                return _parent;
            }
        }
        private SkinnedObjectParent _parent;

        public static Skin[] skins {
            get {
                if (_skins == null) {
                    var skinGuids = AssetDatabase.FindAssets("t:Skin");
                    _skins = System.Array.ConvertAll(
                        skinGuids,
                        (input) => AssetDatabase.LoadAssetAtPath<Skin>(AssetDatabase.GUIDToAssetPath(input))
                    );
                }
                return _skins;
            }
        }
        private static Skin[] _skins;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (parent != null && parent.objectName != "")
            {
                foreach (var skin in skins)
                {
                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("Switch", GUILayout.Width(80))) {
                        var manager = FindObjectOfType<SkinManager>();
                        if (manager != null) {
                            manager.Skin = skin;
                            EditorUtility.SetDirty(manager);
                        }
                        foreach (var par in FindObjectsOfType<SkinnedObjectParent>())
                        {
                            par.EditorBuild(skin);
                        }
                    }

                    var isSkin = PrefabUtility.GetPrefabParent(skinnedObject) == skin.prefabs[parent.objectName];
                    if (EditorGUILayout.Toggle(skin.name, isSkin)) {
                        if (!isSkin) {
                            skin.prefabs[parent.objectName] = PrefabUtility.GetPrefabParent(skinnedObject) as SkinnedObject;
                            EditorUtility.SetDirty(skin);
                        }
                    } else {
                        if (isSkin) {
                            skin.prefabs[parent.objectName] = null;
                            EditorUtility.SetDirty(skin);
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}