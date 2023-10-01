using UnityEngine;

namespace Core.Common
{
    [System.Serializable]
    public class PlayersSpawnPoints
    {
        [field: SerializeField] public Vector2 RedPlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Vector2 BluePlayerSpawnPoint { get; private set; }
    }
}