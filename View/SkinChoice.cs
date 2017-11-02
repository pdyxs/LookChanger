using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PDYXS.Skins.View
{
    [RequireComponent(typeof(Toggle))]
    public class SkinChoice : MonoBehaviour
    {

        public Skin skin;

        // Use this for initialization
        void Start()
        {
            var toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleChanged);
            toggle.isOn = SkinManager.instance.Skin == skin;
        }

        private void OnToggleChanged(bool val)
        {
            if (val == true) {
                SkinManager.instance.ChangeSkin(skin);
            }
        }
    }
}