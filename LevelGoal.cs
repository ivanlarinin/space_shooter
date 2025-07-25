using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player's ship
        SpaceShip playerShip = collision.transform.root.GetComponent<SpaceShip>();

        if (playerShip != null)
        {
            // The player has reached the goal!
            Debug.Log("Player reached the goal!");

            // Here you would add logic for winning the level:
            // - Load next level
            // - Show a win screen
            // - Stop player movement
            // Example:
            // LevelManager.Instance.LoadNextLevel(); // If you have a LevelManager singleton
            // playerShip.ThrustControl = 0;
            // playerShip.TorqueControl = 0;
        }
    }
}