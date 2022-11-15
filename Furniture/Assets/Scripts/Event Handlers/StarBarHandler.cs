using Gameplay.Counters;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(StarBar))]
    public class StarBarHandler : MonoBehaviour
    {
        private StarBar _starBar;

        private void Awake()
        {
            _starBar = GetComponent<StarBar>();
        }

        private void OnEnable()
        {
            ImpactManagerHandler.AddToStarBar += _starBar.Add;
        }

        private void OnDisable()
        {
            ImpactManagerHandler.AddToStarBar -= _starBar.Add;
        }
    }
}
