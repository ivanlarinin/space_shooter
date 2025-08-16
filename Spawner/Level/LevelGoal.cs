using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player's ship
        SpaceShip playerShip = collision.transform.root.GetComponent<SpaceShip>();

        if (playerShip != null)
        {
            Debug.Log("Player reached the goal!");
        }
    }
}