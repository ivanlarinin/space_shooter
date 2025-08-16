using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Spawns two teams of AI bots that fight each other when player enters trigger area
/// </summary>
public class TeamBattleSpawner : MonoBehaviour
{
    [Header("Spawn Configuration")]
    [SerializeField] private GameObject m_BotPrefab;           // Prefab for AI bots
    [SerializeField] private int m_BotsPerTeam = 4;           // Number of bots per team
    [SerializeField] private CircleArea m_Team1SpawnArea;      // Spawn area for team 1
    [SerializeField] private CircleArea m_Team2SpawnArea;      // Spawn area for team 2
    [SerializeField] private bool shieldsEnabled = true;
    [SerializeField] private float shieldHealth = 50f;

    [Header("Team Settings")]
    [SerializeField] private int m_Team1Id = 1;               // Team ID for first team
    [SerializeField] private int m_Team2Id = 2;               // Team ID for second team
    
    [Header("Trigger Settings")]
    [SerializeField] private bool m_OneTimeSpawn = true;       // Only spawn once or respawn when re-entering
    [SerializeField] private float m_TriggerCooldown = 5f;     // Cooldown between spawns if not one-time
    

    
    private bool m_HasSpawned = false;
    private float m_LastSpawnTime;
    private List<GameObject> m_SpawnedBots = new List<GameObject>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if a SpaceShip entered the trigger (check root GameObject)
        SpaceShip spaceShip = other.GetComponentInParent<SpaceShip>();
        if (spaceShip != null)
        {
            if (m_OneTimeSpawn && m_HasSpawned)
                return;
                
            if (!m_OneTimeSpawn && Time.time - m_LastSpawnTime < m_TriggerCooldown)
                return;
            
            SpawnTeams();
            m_HasSpawned = true;
            m_LastSpawnTime = Time.time;
        }
    }
    
    /// <summary>
    /// Spawns two teams of bots that will fight each other
    /// </summary>
    private void SpawnTeams()
    {
        // Clear any existing bots
        ClearExistingBots();
        
        // Spawn Team 1
        if (m_Team1SpawnArea != null)
        {
            SpawnTeam(m_Team1SpawnArea, m_Team1Id, "Team 1");
        }
        else
        {
            Debug.LogWarning("Team 1 spawn area is not assigned!");
        }
        
        // Spawn Team 2
        if (m_Team2SpawnArea != null)
        {
            SpawnTeam(m_Team2SpawnArea, m_Team2Id, "Team 2");
        }
        else
        {
            Debug.LogWarning("Team 2 spawn area is not assigned!");
        }
        
        Debug.Log($"Spawned {m_BotsPerTeam * 2} bots in team battle! Team 1 (ID: {m_Team1Id}) vs Team 2 (ID: {m_Team2Id})");
    }
    
    /// <summary>
    /// Spawns a team of bots within the specified spawn area
    /// </summary>
    private void SpawnTeam(CircleArea spawnArea, int teamId, string teamName)
    {
        for (int i = 0; i < m_BotsPerTeam; i++)
        {
            // Get random position within the spawn area
            Vector2 spawnPosition = spawnArea.GetRandomInsideZone();
            
            // Spawn the bot
            GameObject bot = Instantiate(m_BotPrefab, spawnPosition, Quaternion.identity);
            m_SpawnedBots.Add(bot);
            
            // Set team ID
            Destructable destructible = bot.GetComponent<Destructable>();
            if (destructible != null)
            {
                destructible.SetTeamId(teamId);
            }

            ShieldSystem shield = bot.GetComponent<ShieldSystem>();
            if (shield != null)
            {
                if (shieldsEnabled)
                {
                    shield.SetMaxShield(shieldHealth);
                }
                else
                {
                    shield.SetMaxShield(0f); // disables shields
                }
            }

            // Set up AI controller if present
            AIController aiController = bot.GetComponent<AIController>();
            if (aiController != null)
            {
                // Create a patrol point for this bot
                GameObject patrolPoint = new GameObject($"{teamName} Bot {i + 1} Patrol");
                patrolPoint.transform.position = spawnPosition;
                
                AIPointPatrol patrol = patrolPoint.AddComponent<AIPointPatrol>();
                patrol.SetRadius(spawnArea.Radius * 0.5f); // Set patrol radius to half of spawn area
                
                aiController.SetPatrolBehaviour(patrol);
            }
            
            // Set bot name for debugging
            bot.name = $"{teamName} Bot {i + 1}";
            
            // Apply team color tint
            if (teamId == m_Team2Id)
            {
                ApplyBlueTint(bot);
            }
        }
    }
    
    /// <summary>
    /// Applies a blue tint to the bot's visual components
    /// </summary>
    private void ApplyBlueTint(GameObject bot)
    {
        // Try to tint the SpriteRenderer
        SpriteRenderer spriteRenderer = bot.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.7f, 0.7f, 1.0f, 1.0f); // Light blue tint
        }
        
        // Try to tint child SpriteRenderers as well
        SpriteRenderer[] childSprites = bot.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer childSprite in childSprites)
        {
            childSprite.color = new Color(0.7f, 0.7f, 1.0f, 1.0f); // Light blue tint
        }
    }
    
    /// <summary>
    /// Clears all existing spawned bots
    /// </summary>
    private void ClearExistingBots()
    {
        foreach (GameObject bot in m_SpawnedBots)
        {
            if (bot != null)
            {
                Destroy(bot);
            }
        }
        m_SpawnedBots.Clear();
    }
    
    /// <summary>
    /// Public method to manually trigger team spawning
    /// </summary>
    public void TriggerSpawn()
    {
        SpawnTeams();
    }
    
    /// <summary>
    /// Public method to clear all spawned bots
    /// </summary>
    public void ClearBots()
    {
        ClearExistingBots();
        m_HasSpawned = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        // Draw team spawn areas if assigned
        if (m_Team1SpawnArea != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(m_Team1SpawnArea.transform.position, m_Team1SpawnArea.Radius);
        }
        
        if (m_Team2SpawnArea != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_Team2SpawnArea.transform.position, m_Team2SpawnArea.Radius);
        }
        
        // Draw trigger area
        Gizmos.color = Color.green;
        Collider2D trigger = GetComponent<Collider2D>();
        if (trigger != null)
        {
            Gizmos.DrawWireCube(transform.position, trigger.bounds.size);
        }
    }
} 