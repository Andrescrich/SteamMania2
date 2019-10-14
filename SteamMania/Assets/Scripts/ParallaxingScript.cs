using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxingScript : MonoBehaviour
{
    public static ParallaxingScript instance;

    [Header("Backgrounds that move in x and y")]
    public List<Transform> backgrounds;
    [Header("Background that only move in x")]
    public List<Transform> backgroundsX;
    [SerializeField] private float smoothing = 1f;

    private float[] parallaxScales;
    private float[] parallaxScalesX;
    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        instance = this;
        if (Camera.main != null) cam = Camera.main.transform;
    }

    private void Start() {
        previousCamPos = cam.position;
    }

    private void Update() {
        parallaxScales = new float[backgrounds.Count];
        for (var i = 0; i < backgrounds.Count; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
        parallaxScalesX = new float[backgroundsX.Count];
        for (var i = 0; i < backgroundsX.Count; i++) {
            parallaxScalesX[i] = backgroundsX[i].position.z * -1;
        }
        for (var i = 0; i < backgrounds.Count; i++) {
            var position = cam.position;
            var parallaxX = (previousCamPos.x - position.x) * parallaxScales[i];
            var parallaxY = (previousCamPos.y - position.y) * parallaxScales[i];

            var backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            var backgroundTargetPosY = backgrounds[i].position.y + parallaxY;

            var backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.fixedDeltaTime);
        }

        for (var i = 0; i < backgroundsX.Count; i++) {
            var parallaxX = (previousCamPos.x - cam.position.x) * parallaxScalesX[i];

            var backgroundTargetPosX = backgroundsX[i].position.x + parallaxX;

            var backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundsX[i].position.y, backgroundsX[i].position.z);
            backgroundsX[i].position = Vector3.Lerp(backgroundsX[i].position, backgroundTargetPos, smoothing * Time.fixedDeltaTime);
        }
        previousCamPos = cam.position;
    }
}
