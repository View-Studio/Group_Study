using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_UserSkill : TutorialController
{
    readonly int BLUE_NUMBER = 2;
    readonly int YELLOW_NUMBER = 3;

    protected override void Init()
    {
        Managers.ClientData.GetExp(SkillType.태극스킬, 1);
        Managers.ClientData.GetExp(SkillType.판매보상증가, 1);
        Managers.ClientData.EquipSkillManager.ChangedEquipSkill(UserSkillClass.Main, SkillType.태극스킬);
        Managers.ClientData.EquipSkillManager.ChangedEquipSkill(UserSkillClass.Sub, SkillType.판매보상증가);
        FindObjectOfType<EffectInitializer>().SettingEffect(new UserSkillInitializer().InitUserSkill());
        ChangeMaxUnitSummonColor(BLUE_NUMBER);
        print(Multi_GameManager.instance.BattleData.UnitSummonData.maxColorNumber);
        FindObjectOfType<UI_Status>().UpdateMySkillImage();
    }

    protected override void AddTutorials()
    {
        AddReadCommend("태극 스킬을 발동시키셨군요?");
        AddReadCommend("태극은 동일한 클래스 내에서 빨간, 파란색 유닛만\n존재할 때 버프를 주는 스킬입니다");
        AddReadCommend("지금처럼 기사에게 적용된 경우\n대미지가 25에서 300으로 증가하게 됩니다.");
        AddReadCommend("하지만 태극이 적용되었다고 해도 빨강, 파랑이 아닌\n다른 색깔의 기사가 소환된다면 버프는 사라집니다");
        AddReadCommend("그런 경우 유닛을 판매해 다시 태극을 발동시킬 수 있습니다.");
        AddActionCommend(() => Multi_SpawnManagers.NormalUnit.Spawn(2, 0));
        AddUnitHighLightCommend("예시를 위해 제가 지금 막 노란 기사를 소환했습니다.", new UnitFlags(2, 0));
        AddReadCommend("빨강, 파랑이 아닌 색깔의 유닛이 생성되었으므로\n기사들이 다시 약해질 것입니다.");
        AddReadCommend("그럼 저 불순물을 팔아보도록 합시다");
        AddClickCommend("", "PaintButton");
        AddClickCommend("", "YellowPaintButtonBackGround");
        AddClickCommend("", "UI_UnitTracker");
        AddClickCommend("", "UnitSellButton");
        AddReadCommend("유닛을 판매해 빨강, 파랑 색깔의\n기사들만 존재하게 됐으므로 다시 태극이 발동될 것입니다.");
        AddUI_HighLightCommend("이처럼 태극은 유닛을 판매할 일이 많다보니\n서브 스킬로 판매 보상 강화를 드는 경우가 많습니다.", "SubSkill");
        AddReadCommend("이 외에도 다양한 스킬이 있으니\n여러 조합을 시도해보면서 게임을 즐기시기 바랍니다.");
        AddActionCommend(() => ChangeMaxUnitSummonColor(YELLOW_NUMBER));
    }

    protected override bool TutorialStartCondition() => CheckOnTeaguke();

    bool CheckOnTeaguke() => Multi_UnitManager.Instance.UnitCountByFlag[new UnitFlags(0, 0)] >= 1
            && Multi_UnitManager.Instance.UnitCountByFlag[new UnitFlags(1, 0)] >= 1;

    void ChangeMaxUnitSummonColor(int colorNumber)
        => Multi_GameManager.instance.BattleData.UnitSummonData.maxColorNumber = colorNumber;

    void OnDestroy()
    {
        Managers.ClientData.EquipSkillManager.ChangedEquipSkill(UserSkillClass.Main, SkillType.None);
        Managers.ClientData.EquipSkillManager.ChangedEquipSkill(UserSkillClass.Sub, SkillType.None);
    }
}
