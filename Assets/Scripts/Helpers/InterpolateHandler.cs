using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InterpolateHandler 
{
    public static float NormalizeValue(float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (float)(newMin + (newMax - newMin) * (value - oldMin) / (oldMax - oldMin));
    }
}
