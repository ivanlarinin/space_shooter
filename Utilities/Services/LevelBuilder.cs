using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject m_PlayerHUDPrefab;
    [SerializeField] private GameObject m_LevelGUIPrefab;
    [SerializeField] private GameObject m_BackgroundPrefab;

    [Header("Dependencies")]
    [SerializeField] private PlayerSpawner m_PlayerSpawner;

    private void Awake()
    {
        Player player = m_PlayerSpawner.Spawn();

        Instantiate(m_PlayerHUDPrefab);
        Instantiate(m_LevelGUIPrefab);

        GameObject background = Instantiate(m_BackgroundPrefab);
        background.AddComponent<SyncTransform>().SetTarget(player.FollowCamera.transform);
    }
}
