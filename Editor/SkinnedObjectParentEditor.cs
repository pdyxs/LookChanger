using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

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

            if (objectParent.initialisingObject != null) {
                using (new EditorGUILayout.HorizontalScope())
                {

                    var components = objectParent.initialisingObject.GetComponents<Component>();
                    var componentNames = System.Array.ConvertAll<Component, string>(
                        components,
                        (input) => input.GetType().Name
                    );

                    objectParent.initialisingObject = components[
                        EditorGUILayout.Popup(
                            System.Array.IndexOf(components, objectParent.initialisingObject),
                            componentNames
                        )];

                    var fields = objectParent.initialisingObject.GetType().GetFields(
                          BindingFlags.Public
                        | BindingFlags.Instance
                        | BindingFlags.GetProperty
                        | BindingFlags.GetField
                    );
                    var properties = objectParent.initialisingObject.GetType().GetProperties(
                          BindingFlags.Public
                        | BindingFlags.Instance
                        | BindingFlags.GetProperty
                        | BindingFlags.GetField
                    );

                    var fieldNames = System.Array.ConvertAll(
                        fields, (input) => input.Name
                    );

                    var fnl = new List<string>(fieldNames);
                    fnl.Insert(0, "");
                    fnl.AddRange(System.Array.ConvertAll(
                        properties, (input) => input.Name
                    ));

                    var index = EditorGUILayout.Popup(
                            fnl.IndexOf(objectParent.initialisingObjectProperty),
                                        fnl.ToArray()
                    );
                    if (fnl.IndexOf(objectParent.initialisingObjectProperty) != index)
                    {
                        if (index > 0 && index <= fields.Length)
                        {
                            objectParent.initialisingObjectField = fields[index - 1].Name;
                            objectParent.initialisingObjectProperty = "";
                        }
                        else if (index > fields.Length)
                        {
                            objectParent.initialisingObjectProperty = properties[index - fields.Length - 1].Name;
                            objectParent.initialisingObjectField = "";
                        }
                        else
                        {
                            objectParent.initialisingObjectProperty = "";
                            objectParent.initialisingObjectField = "";
                        }
                    }

                    if (GUILayout.Button("Clear")) {
                        objectParent.initialisingObjectProperty = "";
                        objectParent.initialisingObjectField = "";
                    }
                }
            } 

            var list = SkinConfig.Get().objectNames;
            var i = EditorGUILayout.Popup(
                "Object",
                list.IndexOf(objectParent.objectName),
                list.ToArray()
            );
            if (i >= 0 && i != list.IndexOf(objectParent.objectName)) {
                objectParent.objectName = list[i];
                objectParent.EditorBuild();
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

            if (GUILayout.Button("Build")) {
                objectParent.EditorBuild();
            }
        }
    }
}