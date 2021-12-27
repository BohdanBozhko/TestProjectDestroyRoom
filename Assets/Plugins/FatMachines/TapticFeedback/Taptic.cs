using System.Runtime.InteropServices;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum TapticEnum
{
    Light = 1,
    Medium,
    Heavy
}

public class Taptic : MonoBehaviour
{

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _PlayTaptic(string type);
        [DllImport("__Internal")]
        private static extern void _PlayTaptic6s(string type);
#endif

    public static bool tapticOn = true;
    private static bool isTicedOnThisFrame = false;

    private static int weightOfTic = 0;


    public static void Warning()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("warning");
                // } else {
                //         _PlayTaptic("warning");
                // }
#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.Warning);
#endif
    }
    public static void Failure()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("failure");
                // } else {
                //         _PlayTaptic("failure");
                // }
#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.Failure);
#endif
    }
    public static void Success()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("success");
                // } else {
                //         _PlayTaptic("success");
                // }
                 _PlayTaptic("success");
#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.Success);
#endif
    }
    public static void Light()
    {
        TapTicExample((int)TapticEnum.Light, () => LightFunc());
    }
    public static void LightFunc()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("light");
                // } else {
                //         _PlayTaptic("light");
                // }
                _PlayTaptic("light");

#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.LightImpact);
#endif
    }
    public static void Medium()
    {
        TapTicExample((int)TapticEnum.Medium, () => MediumFunc());
    }
    public static void MediumFunc()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("medium");
                // } else {
                //         _PlayTaptic("medium");
                // }
                _PlayTaptic("medium");
#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.MediumImpact);
#endif
    }
    public static void Heavy()
    {
        TapTicExample((int)TapticEnum.Heavy, () => Heavyfunc());
    }
    public static void Heavyfunc()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("heavy");
                // } else {
                //         _PlayTaptic("heavy");
                // }
#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.HeavyImpact);
#endif
    }
    public static void Default()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }
    public static void Vibrate()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("medium");
                // } else {
                //         _PlayTaptic("medium");
                // }
                _PlayTaptic("medium");
#elif UNITY_ANDROID
        AndroidTaptic.Vibrate();
#endif
    }
    public static void Selection()
    {
        if (!tapticOn || Application.isEditor)
        {
            return;
        }
#if UNITY_IOS
                // if (iPhone6s()) {
                //         _PlayTaptic6s("selection");
                // } else {
                //         _PlayTaptic("selection");
                // }
#elif UNITY_ANDROID
        AndroidTaptic.Haptic(HapticTypes.Selection);
#endif
    }

    private void LateUpdate()
    {
        isTicedOnThisFrame = false;
        weightOfTic = 0;
        //Debug.Log("Taptic log isTicedOnThisFrame " + isTicedOnThisFrame);

    }
    public static bool IsMayToTapTic()
    {
        if (!tapticOn)
            return false;
        return !isTicedOnThisFrame; //|| tapticOn
    }
    public static bool IsMayWithWeightFactor(int _weightFactor)
    {
        if (weightOfTic < _weightFactor)
        {
            weightOfTic = _weightFactor;
            return true;
        }
        return false;
    }
    public static void UsedTaptic()
    {
        isTicedOnThisFrame = true;
    }
    public static void TapTicExample(int _factorWeight, Action _someTaptic)
    {
        if (!IsMayWithWeightFactor(_factorWeight))
            return;
        if (!IsMayToTapTic())
            return;
        //some function
        _someTaptic();
        //Debug.Log("Taptic log is taptic " + _someTaptic.ToString());
        UsedTaptic();
    }
}