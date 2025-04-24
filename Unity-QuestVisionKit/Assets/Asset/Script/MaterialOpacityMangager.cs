
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
public class MaterialOpacityMangager : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public List<Renderer> rendererList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateRenderersAlpha(float alpha)
    {
        foreach (var renderer in rendererList)
        {
            Material mat = renderer.material;
            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }

        }
    }
}
