using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class NewBehaviourScript : Editor
{
    [MenuItem("GameObject/CustomUI/Button", priority = 0)]
    public static void AddButton()
    {
        Create("Button");
    }

    private static GameObject clickedObject;

    static GameObject Create(string objectName)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>("UI/Button"));
        instance.name = objectName;
        clickedObject = Selection.activeObject as GameObject;
        if(clickedObject != null)
        {
            instance.transform.SetParent(clickedObject.transform, false);
        }

        return instance;
    }
}
