using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Boss : TutorialController
{
    protected override void AddTutorials()
    {
        AddObjectHighLightCommend("저건 보스입니다.", new Vector3(-45, 20, 35), 30f);
        AddReadCommend("보스는 10스테이지당 한번씩 등장하며\n 등장 시 모든 유닛이 보스를 공격합니다.");
        AddReadCommend("또한, 보스를 처치하면 소소한 보상을 줍니다.\n그러니 열심히 잡도록 합시다.");
    }

    protected override bool TutorialStartCondition() => Multi_StageManager.Instance.CurrentStage == 10;
}
