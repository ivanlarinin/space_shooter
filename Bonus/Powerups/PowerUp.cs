using UnityEngine;

/// <summary>
/// Abstract base class for all power-up objects in the game.
/// Requires a CircleCollider2D (set as trigger) so it can detect when a ship collects it.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

        if (ship != null && Player.Instance.ActiveShip)
        {
            OnPickedUp(ship);
        }
        Destroy(gameObject);
    }

    protected abstract void OnPickedUp(SpaceShip ship);
}