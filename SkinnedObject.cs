using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDYXS.ThingSpawner;

namespace PDYXS.Skins
{
    public class SkinnedObject : MonoBehaviour, IPrefabSaveable
    {
        public void Init() {
            initialise();
        }

        public void Init(object o) {
            initialise(o);
        }

        public void Init(object o1, object o2)
        {
            initialise(o1, o2);
        }

        protected virtual void initialise() {}

        protected virtual void initialise(object o) { }

        protected virtual void initialise(object o1, object o2) { }
    }
}