using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PDYXS.Skins
{
    [CustomEditor(typeof(Skin))]
    public class SkinEditor : Editor
    {
        public Skin skin {
            get {
                return target as Skin;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var oname in SkinConfig.Get().objectNames) {
                if (!skin.prefabs.ContainsKey(oname)) {
                    skin.prefabs[oname] = null;
                }
            }

            EditorGUILayout.LabelField("Prefabs", EditorStyles.boldLabel);
            foreach (var prefabInfo in skin.prefabs) {

                SkinnedObject prefab = prefabInfo.Value;
                if (!SkinConfig.Get().objectNames.Contains(prefabInfo.Key)) {
                    var c = GUI.color;
                    GUI.color = Color.red;

                    EditorGUILayout.BeginHorizontal();

                    var list = new List<string>(SkinConfig.Get().objectNames);
                    list.Insert(0, prefabInfo.Key);

                    int i = EditorGUILayout.Popup(0, list.ToArray());
                    if (i != 0) {
                        skin.prefabs[list[i]] = prefabInfo.Value;
                        skin.prefabs.Remove(prefabInfo.Key);
                        EditorUtility.SetDirty(target);
                        break;
                    }

                    prefab = EditorGUILayout.ObjectField(
                        prefabInfo.Value, typeof(SkinnedObject), false
                    ) as SkinnedObject;

                    if (GUILayout.Button("X", GUILayout.Width(30))) {
                        skin.prefabs.Remove(prefabInfo.Key);
                        EditorUtility.SetDirty(target);
                        break;
                    }

                    EditorGUILayout.EndHorizontal();
                    GUI.color = c;
                } else {
                    prefab = EditorGUILayout.ObjectField(
                        prefabInfo.Key, prefabInfo.Value, typeof(SkinnedObject), false
                    ) as SkinnedObject;
                }

                if (prefab != prefabInfo.Value)
                {
                    skin.prefabs[prefabInfo.Key] = prefab;
                    EditorUtility.SetDirty(target);
                    break;
                }
            }
        }

    }
}