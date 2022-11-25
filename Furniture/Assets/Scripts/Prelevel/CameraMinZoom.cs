using System;
using UnityEngine;

namespace Prelevel
{
    public class CameraMinZoom : MonoBehaviour, IPrelevelExecutable
    {
        public static event Action SetMinCameraZoom;

        public void Execute()
        {
            SetMinCameraZoom?.Invoke();
        }
    }
}
