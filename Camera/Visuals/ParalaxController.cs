using UnityEngine;

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