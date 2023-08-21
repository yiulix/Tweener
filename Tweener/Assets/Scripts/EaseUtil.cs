using System;

public enum EaseMode
{
    Linear,
    EaseInSine,
    EaseOutSine,
    EaseInOutSine,
    EaseInBack,
    EaseOutBack,
    EaseInOutBack,
}


public static class EaseUtil
{
    const double c1 = 1.70158;
    const double c2 = c1 * 1.525;
    const double c3 = c1 + 1;
    
    public static double Ease(EaseMode easeMode, float t)
    {
        switch (easeMode)
        {
            case EaseMode.Linear:
                return t;
            case EaseMode.EaseInSine:
                return 1 - Math.Cos((t * Math.PI) / 2);
            case EaseMode.EaseOutSine:
                return Math.Sin((t * Math.PI) / 2);
            case EaseMode.EaseInOutSine:
                return -(Math.Cos(Math.PI * t) - 1) / 2;
            case EaseMode.EaseInBack:
                return c3 * t * t * t - c1 * t * t;
            case EaseMode.EaseOutBack:
                return 1 + c3 * Math.Pow(t - 1, 3) + c1 * Math.Pow(t - 1, 2);
            case EaseMode.EaseInOutBack:
                return t < 0.5
                    ? (Math.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
                    : (Math.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
            default:
                throw new ArgumentOutOfRangeException(nameof(easeMode), easeMode, null);
        }
    }
}
