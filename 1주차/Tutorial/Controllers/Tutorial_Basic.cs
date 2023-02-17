using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Basic : TutorialController
{
    protected override void AddTutorials()
    {
        AddReadCommend("컬러 랜덤 디펜스에 오신 것을 환영합니다!!");
        AddReadCommend("이 게임은 상대방보다 먼저 필드의 몬스터 수가 50이 넘으면\n패배하는 \"버티기\" 게임입니다.");
        AddActionCommend(() => Multi_GameManager.instance.GameStart(), () => Multi_GameManager.instance.isGameStart);
        AddObjectHighLightCommend("게임이 시작하면 적 유닛이 나옵니다", new Vector3(-45, 5, 35));
        AddReadCommend("몬스터는 유닛을 뽑아서 처치할 수 있습니다.");
        AddClickCommend("버튼을 눌러 유닛을 뽑아보세요", "Create_Defenser_Button");
        AddUnitHighLightCommend("유닛을 뽑으면 5골드가 소모되며\n빨간, 파란, 노란 기사 중 무작위로 하나를 획득합니다.", UnitClass.Swordman);
        AddUI_HighLightCommend("옆에 보이는 버튼들을 이용해 획득한 유닛들을\n관리할 수 있습니다.", "UnitButtons");
        AddUI_HighLightCommend("초록색 정보창에서 자신의 상태를 확인할 수 있습니다.", "MyCount");
        AddReadCommend("이제 잠시 게임을 플레이해보세요!!\n때가 되면 다시 돌아오겠습니다.");
    }

    protected override bool TutorialStartCondition() => true;
}
