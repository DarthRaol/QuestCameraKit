using UnityEngine;

public class LikedOptions : MonoBehaviour
{
    public Renderer Renderer;
     Material targetMaterial; // The material to update
    public Texture2D newImage;      // The image to assign
    private bool isLiked = false;   // Starts unliked

    void Start()
    {
        targetMaterial = Renderer.material;
        if (targetMaterial != null && newImage != null)
        {
            targetMaterial.SetTexture("_ClothImage", newImage);
        }
    }

    public void ToggleLike()
    {
        isLiked = !isLiked;

        targetMaterial.SetFloat("_Liked", isLiked?1f:0f);

    }
}
