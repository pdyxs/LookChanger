using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PDYXS.Skins
{
    [CustomPropertyDrawer(typeof(SkinDictionary))]
    public class SkinDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}