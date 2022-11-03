using UnityEngine;

namespace Service
{
    public class GameStorage : MonoBehaviour
    {
        public static GameStorage Instanse { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private GameObject _bum;
        [SerializeField] private GameObject _movingCenter;

        [Header("Variables")]
        [SerializeField] private float _capturePointZPos;

        public GameObject Bum { get => _bum; }

        public GameObject MovingCenter { get => _movingCenter; }

        public float CapturePointZPos { get => _capturePointZPos; }

        private void Awake()
        {
            if (Instanse == null)
                Instanse = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}
