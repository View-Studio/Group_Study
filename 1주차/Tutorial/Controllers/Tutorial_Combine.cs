using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tutorial_Combine : TutorialController
{
    readonly string TARGET_UI_INDEX_NAME = "TutorialCombineUI";
    protected override void AddTutorials()
    {
        AddReadCommend("안녕하세요!!");
        AddReadCommend("저희 게임에서는 조합이라는 시스템이 있는데 \n 기사의 상위 유닛인 궁수를 조합할 수 있게 되서 나왔습니다.");
        AddClickCommend("우선 물감을 선택하세요", "PaintButton");
        AddUI_HighLightCommend("이 돋보기 모양 버튼은 조합 가능한 유닛 중 \n 상위 4개 유닛을 보여주는 특별한 버튼입니다.", "CombineableUnitsBackGround");
        AddClickCommend("이제 돋보기를 클릭합니다", "CombineableUnitsBackGround");
        AddTargetUINameToIndexNameActionCommend<UI_UnitTracker>((ui) => ui.UnitFlags.UnitClass == UnitClass.Archer, TARGET_UI_INDEX_NAME);
        AddClickCommend("이제 조합 가능한 궁수 유닛이 보입니다. \n 클릭합시다", TARGET_UI_INDEX_NAME);
        AddClickCommend("조합 버튼을 클릭해 조합합니다.", "CombineButton 2");
        AddUnitHighLightCommend("기사 3개를 조합해 궁수를 조합했습니다!!", UnitClass.Archer);
    }

    protected override bool TutorialStartCondition()
        => new UnitCombineSystem()
            .GetCombinableUnitFalgs(Multi_UnitManager.Instance.Master.GetUnits(Multi_Data.instance.Id)
            .Select(x => x.UnitFlags))
            .Any(x => x.ClassNumber == 1);
}
