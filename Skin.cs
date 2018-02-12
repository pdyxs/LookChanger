using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PDYXS.Skins
{
    [CreateAssetMenu(fileName = "Game Skin", menuName = "Game Skin", order = 100)]
    public class Skin : ScriptableObject
    {
        [HideInInspector]
        public SkinDictionary prefabs = new SkinDictionary();

        public SkinnedObject GetSkinPrefab(string name) {
            return prefabs[name];
        }

        public List<string> PrefabNames {
            get {
                return prefabs.Keys.ToList();
            }
        }
    }

    [System.Serializable]
    public class SkinDictionary : SerializableDictionary<string, SkinnedObject> {}
}