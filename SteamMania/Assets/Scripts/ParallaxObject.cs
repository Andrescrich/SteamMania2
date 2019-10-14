
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxObject : MonoBehaviour
{
    private ParallaxingScript parallaxScript;
    [SerializeField] private bool parallaxEnX;
    [SerializeField] private bool parallaxEnXy;
    [SerializeField] private bool isParallaxed;


    private void Start() {
        parallaxScript = ParallaxingScript.instance;
        if (parallaxEnX && parallaxEnXy) {
            Debug.LogError("El Gameobject " + gameObject + "tiene parallax X y XY a la vez");
        }
        if (parallaxEnX && !isParallaxed) {
            parallaxScript.backgroundsX.Add(gameObject.transform);
            isParallaxed = true;
        } else if(!parallaxEnX && isParallaxed) {
            foreach(var parallaxObjectX in parallaxScript.backgroundsX) {
                if (parallaxObjectX != gameObject.transform) continue;
                parallaxScript.backgroundsX.Remove(parallaxObjectX);
                isParallaxed = false;
            }
        }
        if (parallaxEnXy && !isParallaxed) {
            parallaxScript.backgrounds.Add(gameObject.transform);
            isParallaxed = true;
        } else if (!parallaxEnXy && isParallaxed) {
            foreach (var parallaxObjectXy in parallaxScript.backgrounds) {
                if (parallaxObjectXy != gameObject.transform) continue;
                parallaxScript.backgrounds.Remove(parallaxObjectXy);
                isParallaxed = false;
            }
        }
    }
} 
    
        


