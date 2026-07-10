using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform parallaxLayer;
        [Range(0,1)] public float parallaxFactor;
    }

    public ParallaxLayer[] layers;
    [SerializeField] private Transform mainCam;
    [SerializeField] private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = mainCam.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = mainCam.position - lastCameraPosition;
            
        foreach(ParallaxLayer layer in layers)
        {
            float moveX = cameraDelta.x * layer.parallaxFactor;
            float moveY = cameraDelta.y * layer.parallaxFactor;

            layer.parallaxLayer.position += new Vector3(moveX, moveY, 0);
        }

        lastCameraPosition = mainCam.position;
    }
}