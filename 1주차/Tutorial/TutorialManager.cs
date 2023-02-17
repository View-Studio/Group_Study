using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    void Awake()
    {
        gameObject.AddComponent<Tutorial_Basic>();
        gameObject.AddComponent<Tutorial_OtherPlayer>();
        gameObject.AddComponent<Tutorial_Tower>();
        gameObject.AddComponent<Tutorial_Boss>();
        gameObject.AddComponent<Tutorial_Combine>();
        gameObject.AddComponent<Tutorial_UserSkill>();
    }
}
