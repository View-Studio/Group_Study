using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTester : MonoBehaviour
{
    public void AddTutorial<T>() where T : Component
    {
        T tutorial = GetComponent<T>();
        if(tutorial != null)
            Destroy(gameObject.GetComponent<T>());

        gameObject.AddComponent<T>();
    }
}
