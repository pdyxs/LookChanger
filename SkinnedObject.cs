using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PDYXS.Skins
{
    public class SkinnedObject : MonoBehaviour
    {
        public void Init() {
            initialise();
        }

        protected virtual void initialise() {}
    }

    public abstract class SkinnedObject<T> : SkinnedObject
    {
        public void Init(T obj) {
            Init();
            initialise(obj);
        }

        protected override void initialise() {}

        protected abstract void initialise(T obj);
    }

    public abstract class SkinnedObject<T, U> : SkinnedObject
    {
        public void Init(T obj0, U obj1)
        {
            Init();
            initialise(obj0, obj1);
        }

        protected override void initialise() { }

        protected abstract void initialise(T obj0, U obj1);
    }
}