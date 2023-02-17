using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorial
{
    void TutorialAction();
    bool EndCondition();
    void EndAction();
}
