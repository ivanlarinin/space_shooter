using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Restricts an object's position within the playable area.
    /// Works together with the LevelBoundary script if present in the scene.
    /// Attach this to any object that needs to be kept inside the level boundaries.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            // If there's no LevelBoundary instance, do nothing
            if (LevelBoundary.Instance == null) return;

            var lb = LevelBoundary.Instance;
            var r = lb.Radius; // The boundary radius

            // If the object is outside the boundary
            if (transform.position.magnitude > r)
            {
                // Clamp the object to the edge of the boundary
                if (lb.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                // Teleport the object to the opposite side of the boundary
                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }
}
