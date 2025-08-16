using SpaceShooter;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject m_PlayerHUDPrefab;
    [SerializeField] private GameObject m_LevelGUIPrefab;
    [SerializeField] private GameObject m_BackgroundPrefab;

    [Header("Dependencies")]
    [SerializeField] private PlayerSpawner m_PlayerSpawner;
    [SerializeField] private LevelBoundary levelBoundary;
    [SerializeField] private LevelController m_LevelController;

    private void Awake()
    {
        levelBoundary.Init();
        m_LevelController.Init();

        Player player = m_PlayerSpawner.Spawn();

        player.Init();

        Instantiate(m_PlayerHUDPrefab);
        Instantiate(m_LevelGUIPrefab);

        GameObject background = Instantiate(m_BackgroundPrefab);
        background.AddComponent<SyncTransform>().SetTarget(player.FollowCamera.transform);

        var parallax = background.GetComponent<StarfieldParallaxController>();
        if (parallax) parallax.target = player.FollowCamera.transform;
    }
}
