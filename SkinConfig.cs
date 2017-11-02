using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PDYXS.Skins
{
    public class SkinConfig : ScriptableConfig<SkinConfig>
    {

#if UNITY_EDITOR
        [MenuItem("Assets/Skin Configuration")]
        public static void Create() {
            create();
        }
#endif

        public Skin defaultSkin;

        public List<string> objectNames;
    }
}