using UnityEngine;

namespace Service
{
    public class GameStorage : MonoBehaviour
    {
        public static GameStorage Instanse { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private GameObject _bum;
        [SerializeField] private GameObject _movingCenter;
        [SerializeField] private GameObject _capturePoint;

        [Header("Variables")]
        [SerializeField] private float _capturePointScale;

        public GameObject Bum { get => _bum; }
        public GameObject MovingCenter { get => _movingCenter; }
        public GameObject CapturePoint { get => _capturePoint; }
        public float CapturePointScale { get => _capturePointScale; }

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
