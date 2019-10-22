using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMethods
{
    public static IEnumerator HitStopCor()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(.025f);

        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(.025f);
    }
}
