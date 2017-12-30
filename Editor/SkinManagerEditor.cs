using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PDYXS.Skins
{
    [CustomEditor(typeof(SkinManager))]
    public class SkinManagerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty("skin")
                );
                serializedObject.ApplyModifiedProperties();

                if (check.changed) {
                    foreach (var par in FindObjectsOfType<SkinnedObjectParent>())
                    {
                        par.EditorBuild((target as SkinManager).Skin);
                    }
                }
            }

            if (GUILayout.Button("Build")) {
                foreach (var par in FindObjectsOfType<SkinnedObjectParent>())
                {
                    par.EditorBuild((target as SkinManager).Skin);
                }
            }
        }
    }
}