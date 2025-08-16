using UnityEngine;

/// <summary>
/// Controls a starfield material to create a parallax scrolling effect
/// based on the position of a target (e.g., the player ship or camera).
/// </summary>

[DefaultExecutionOrder(100)]
public class StarfieldParallaxController : MonoBehaviour
{
    public Material starfieldMaterial;
    public Transform target;

    void Update()
    {
        if (starfieldMaterial && target)
        {
            Vector3 pos = target.position;
            starfieldMaterial.SetVector("_ViewOffset", new Vector4(pos.x, pos.y, 0, 0));
        }
    }
}