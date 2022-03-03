using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathfs
{
    public static float Remap(float oldMin, float oldMax, float newMin, float newMax, float value)
    {
        return (Mathf.Lerp(newMin, newMax, Mathf.InverseLerp(oldMin, oldMax, value)));
    }
}
