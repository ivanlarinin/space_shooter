using UnityEngine;

public class StarfieldParallaxController : MonoBehaviour
{
    public Material starfieldMaterial;
    public Transform target; // Usually your camera or player

    void Update()
    {
        if (starfieldMaterial && target)
        {
            // You can scale the position if you want to adjust parallax speed
            Vector3 pos = target.position;
            starfieldMaterial.SetVector("_ViewOffset", new Vector4(pos.x, pos.y, 0, 0));
        }
    }
}