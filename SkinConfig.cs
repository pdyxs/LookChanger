using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PDYXS.Skins
{
    [SaveLocation("Assets/Config/Resources", "Look Changer")]
    public class SkinConfig : ScriptableConfig<SkinConfig>
    {

#if UNITY_EDITOR
        [MenuItem("Assets/Configuration/Look Changer")]
        public static void Create() {
            create();
        }
#endif

        public Skin defaultSkin;

        public List<string> objectNames;
    }
}