using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweener
{
    public static class UIComponentExtension
    {
        public static Tweener DoMove(this Transform trans, Vector3 to, float duration)
        {
            Tweener<Vector3> tweener = new Tweener<Vector3>();
            tweener.Init(()=>trans.localPosition, value => { trans.localPosition = value;}, to, duration, new VectorTypeHelper());
            DTween.AddTweener(tweener);
            return tweener;
        }
    
        public static Tweener DoScale(this Transform trans, Vector3 to, float duration)
        {
            Tweener<Vector3> tweener = new Tweener<Vector3>();
            tweener.Init(()=>trans.localScale, value => { trans.localScale = value;}, to, duration, new VectorTypeHelper());
            DTween.AddTweener(tweener);
            return tweener;
        }
    }
}
