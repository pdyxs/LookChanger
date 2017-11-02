using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PDYXS.Skins
{
    [CustomEditor(typeof(SkinnedObjectParent))]
    public class SkinnedObjectParentEditor : Editor
    {
        public SkinnedObjectParent objectParent {
            get {
                return target as SkinnedObjectParent;
            }
        }

        private string newObjectName = "";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var list = SkinConfig.Get().objectNames;
            var i = EditorGUILayout.Popup(
                "Object",
                list.IndexOf(objectParent.objectName),
                list.ToArray()
            );
            if (i >= 0) {
                objectParent.objectName = list[i];
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            newObjectName = EditorGUILayout.TextField(newObjectName);
            EditorGUI.BeginDisabledGroup(newObjectName.Length == 0 || SkinConfig.Get().objectNames.Contains(newObjectName));
            if (GUILayout.Button("+")) {
                SkinConfig.Get().objectNames.Add(newObjectName);
                objectParent.objectName = newObjectName;
                newObjectName = "";
                EditorUtility.SetDirty(SkinConfig.Get());
                EditorUtility.SetDirty(target);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }
}