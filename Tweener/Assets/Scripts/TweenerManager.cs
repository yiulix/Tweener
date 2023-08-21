using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweener
{
    public class TweenerManager : MonoBehaviour
    {
        private List<Tweener> _tweeners = new ();
        private List<int> _toRemoveList = new();
        
        void Update()
        {
            for(int i = 0; i < _tweeners.Count; i++)
            {
                var tweener = _tweeners[i];
                tweener.Tick(Time.deltaTime);
                if (!tweener.IsActive)
                {
                    _toRemoveList.Add(i);
                }
            }
        }
    
        private void LateUpdate()
        {
            for (int i = 0; i < _toRemoveList.Count; i++)
            {
                _tweeners.RemoveAt(_toRemoveList[i]);
            }
            
            _toRemoveList.Clear();
        }
    
        public void AddTweener(Tweener tweener)
        {
            _tweeners.Add(tweener);
        }
    
        public void Kill(Tweener tweener)
        {
            var index = _tweeners.IndexOf(tweener);
            if (index != -1)
            {
                _toRemoveList.Add(index);
            }
    
        }
        
    }
    
    public static class DTween
    {
        public static bool Inited;
        private static TweenerManager _tweenerManager;
    
        public static void Init()
        {
            GameObject tweenerManagerGo = new GameObject("TweenerManager");
            _tweenerManager = tweenerManagerGo.AddComponent<TweenerManager>();
            Inited = true;
        }
    
        public static void Kill(Tweener tweener)
        {
            _tweenerManager.Kill(tweener);
        }
    
        public static void AddTweener(Tweener tweener)
        {
            if (!Inited)
            {
                Init();
            }
            _tweenerManager.AddTweener(tweener);
        }
    }
}


