using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_OtherPlayer : TutorialController
{
    protected override void AddTutorials()
    {
        AddReadCommend("이곳은 상대방의 진영입니다");
        AddReadCommend("상대방 역시 유닛을 뽑아 적군을 막아야 합니다.");
        AddUI_HighLightCommend("빨간색 정보창은 상대방의 상황을 보여줍니다.", "OpponentCount");
        AddUI_HighLightCommend("유닛을 확인한 후 아래에 있는 버튼들을 이용해\n상대방에게 까다로운 특성을 가진 몬스터를 소환시킬 수 있습니다.", "MonsterSelectFocus");
        AddUI_HighLightCommend("마지막으로 왼쪽 위 UI를 통해\n상대방의 스킬을 확인할 수 있습니다", "EquipSkills");
    }

    protected override bool TutorialStartCondition() => Managers.Camera.IsLookOtherWolrd && Managers.Camera.IsLookEnemyTower == false;
}
