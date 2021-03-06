﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PDYXS.Skins
{
    public class SkinManager : MonoSemiSingleton<SkinManager>
    {
        private List<Skin> initialisedSkins = new List<Skin>();

        [SerializeField]
        [HideInInspector]
        private Skin skin;

        public class SkinChangeEvent : UnityEvent {}
        public SkinChangeEvent OnSkinChanged = new SkinChangeEvent();

        public Skin Skin
        {
            get
            {
                if (skin == null)
                {
                    skin = SkinConfig.Get().defaultSkin;
                    SetupSkin();
                }
                return skin;
            }
#if UNITY_EDITOR
            set
            {
                skin = value;
            }
#endif
        }

        public void ChangeSkin(Skin newSkin) {
            if (newSkin == null || newSkin == skin) {
                return;
            }
            skin = newSkin;
            SetupSkin();
            OnSkinChanged.Invoke();
        }

        private void SetupSkin() {
            if (!initialisedSkins.Contains(skin)) {
                initialisedSkins.Add(skin);
                foreach (var obj in skin.prefabs.Values)
                {
                    ObjectPool.CreatePool(obj, 1);
                }
            }
        }
    }
}