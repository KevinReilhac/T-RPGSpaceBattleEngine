using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class TransformExtention
{
    public static void ClearChilds(this Transform transform)
    {
        if (!Application.isPlaying)
            ClearChildsImmediate(transform);

       int childs = transform.childCount;

       for (int i = childs - 1; i >= 0; i--)
           GameObject.Destroy(transform.GetChild(i).gameObject);
    }

    public static void ClearChildsImmediate(this Transform transform)
    {
       int childs = transform.childCount;

       for (int i = childs - 1; i >= 0; i--)
           GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
    }

    public static void SetGraphicsAlpha(this Transform transform, float alpha)
    {
        List<Graphic> graphics = transform.GetComponentsInChildren<Graphic>().ToList();

        foreach (Graphic graphic in graphics)
        {
            Color color = graphic.color;
            color.a = alpha;

            graphic.color = color;
        }
    }

    public static void DoFade(this Transform transform, float endValue, float duration, float startValue, Ease easeCurve = Ease.Linear)
    {
        List<Graphic> graphics = transform.GetComponentsInChildren<Graphic>().ToList();

        foreach (Graphic graphic in graphics)
        {
            Color color = graphic.color;

            color.a = startValue;
            graphic.DOFade(endValue, duration)
                .SetEase(easeCurve)
                .ChangeStartValue(color);
        }
    }
}