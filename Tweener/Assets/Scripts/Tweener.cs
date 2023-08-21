using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweener
{
    public abstract class Tweener
    {
        public abstract void Kill();
    
        public abstract void Tick(float deltaTime);
    
        public abstract bool IsActive { get; }

        public abstract void SetEase(EaseMode easeMode);
    }
    
    public class Tweener<T> : Tweener
    {
        public delegate T Getter();
    
        public delegate void Setter(T value);
        
        private Getter _getter;
        private Setter _setter;
        internal float Time;
        internal float Duration;

        internal EaseMode EaseMode = EaseMode.Linear;
    
        internal T StartValue;
        internal T EndValue;

        private bool _isActive;
    
        private TypeHelper<T> _typeHelper;
    
        public override  bool IsActive => _isActive;
        
        public override void SetEase(EaseMode easeMode)
        {
            EaseMode = easeMode;
        }

        public void Init(Getter getter, Setter setter, T endValue, float duration, TypeHelper<T> typeHelper)
        {
            _getter = getter;
            _setter = setter;
            _typeHelper = typeHelper;
    
            StartValue = getter();
            EndValue = endValue;
    
            Duration = duration;
    
            this.Time = 0;
    
            if (duration > 0)
            {
                _isActive = true;
            }
        }
    
        public override void Kill()
        {
            _isActive = false;
        }
    
        public override void Tick(float deltaTime)
        {
            if (!_isActive)
            {
                return;
            }
            
            this.Time += deltaTime;
            
            if (this.Time >= Duration)
            {
                _setter(EndValue);
                _isActive = false;
                return;
            }
    
            T value = _Estimate();
    
            _setter(value);
        }
    
        private T _Estimate()
        {
            return _typeHelper.Estimate(this);
        }
    }

    public abstract class TypeHelper<T>
    {
        internal abstract T Estimate(Tweener<T> tweener);
    }
    
    public class VectorTypeHelper : TypeHelper<Vector3>
    {
        internal override Vector3 Estimate(Tweener<Vector3> tweener)
        {
            var progress = tweener.Time / tweener.Duration;
            progress = (float)EaseUtil.Ease(tweener.EaseMode, progress);
            return Vector3.Lerp(tweener.StartValue, tweener.EndValue, progress);
        }
    }
}
