using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TutorialCommends;
using System;
using System.Linq;

public abstract class TutorialController : MonoBehaviour
{
    protected TutorialFuntions tutorialFuntions;

    void Start()
    {
        tutorialFuntions = FindObjectOfType<TutorialFuntions>();
        AddTutorials();
        StartCoroutine(Co_WaitCondition());
        Init();
    }
    protected abstract bool TutorialStartCondition();
    protected abstract void AddTutorials();
    protected virtual void Init() { }
    IEnumerator Co_WaitCondition()
    {
        yield return new WaitUntil(() => TutorialStartCondition());
        tutorialFuntions.OffLigth();
        StartCoroutine(Co_DoTutorials());
    }

    List<ITutorial> tutorialCommends = new List<ITutorial>();
    IEnumerator Co_DoTutorials()
    {
        yield return StartCoroutine(Co_DoTutorial(tutorialCommends.First(), 1f));
     
        foreach (var tutorial in tutorialCommends.Skip(1))
            yield return StartCoroutine(Co_DoTutorial(tutorial));
        // 모든 튜토리얼이 끝나면 게임 진행
        tutorialFuntions.GameProgress();
    }

    IEnumerator Co_DoTutorial(ITutorial tutorial, float delayTime = 0.1f)
    {
        var buttons = GameObject.FindObjectsOfType<Button>();
        SetAllButton(buttons, false);
        tutorial.TutorialAction();
        yield return new WaitForSecondsRealtime(delayTime);
        yield return new WaitUntil(() => tutorial.EndCondition());
        tutorial.EndAction();
        SetAllButton(buttons, true);

        yield return 1;
    }

    void SetAllButton(Button[] buttons, bool isActive)
    {
        foreach (var button in buttons.Where(x => x != null))
            button.enabled = isActive;
    }

    protected TutorialComposite CreateComposite() => new TutorialComposite();
    protected ReadTextCommend CreateReadCommend(string text) => new ReadTextCommend(text);
    protected SpotLightCommend CreateSpotLightCommend(Vector3 pos, float range) => new SpotLightCommend(pos, range);
    protected SpotLightActionCommend CreateSpotLightActionCommend(Func<Vector3> getPos) => new SpotLightActionCommend(getPos);
    protected Highlight_UICommend CreateUI_HighLightCommend(string uiName) => new Highlight_UICommend(uiName);
    protected ButtonClickCommend CreateClickCommend(string uiName) => new ButtonClickCommend(uiName);
    protected ActionCommend CreateActionCommend(Action tutorialAction, Func<bool> endCondtion = null, Action endActoin = null)
        => new ActionCommend(tutorialAction, endCondtion, endActoin);

    protected void AddCommend(ITutorial tutorial) => tutorialCommends.Add(tutorial);
    void AddCompositeCommend(string text, ITutorial commend)
    {
        var compositeCommend = CreateComposite();
        compositeCommend.AddCommend(CreateReadCommend(text));
        compositeCommend.AddCommend(commend);
        tutorialCommends.Add(compositeCommend);
    }
    protected void AddReadCommend(string text) => tutorialCommends.Add(CreateReadCommend(text));

    protected void AddUnitHighLightCommend(string text, UnitClass unitClass)
        => AddUnitHighLightCommend(text, () => Multi_UnitManager.Instance.FindUnit(0, unitClass).transform.position + new Vector3(0, 5, 0));

    protected void AddUnitHighLightCommend(string text, UnitFlags unitFlag)
        => AddCompositeCommend(text, CreateSpotLightActionCommend(() => Multi_UnitManager.Instance.FindUnit(0, unitFlag).transform.position + new Vector3(0, 5, 0)));

    protected void AddUnitHighLightCommend(string text, Func<Vector3> getPos)
        => AddCompositeCommend(text, CreateSpotLightActionCommend(getPos));

    protected void AddObjectHighLightCommend(string text, Vector3 pos, float range = 10f)
        => AddCompositeCommend(text, CreateSpotLightCommend(pos, range));

    protected void AddUI_HighLightCommend(string text, string uiName)
        => AddCompositeCommend(text, CreateUI_HighLightCommend(uiName));

    protected void AddClickCommend(string text, string uiName)
    {
        var clickCommend = CreateComposite();
        clickCommend.AddCommend(CreateUI_HighLightCommend(uiName));
        clickCommend.AddCommend(CreateClickCommend(uiName));
        AddCompositeCommend(text, clickCommend);
    }

    protected void AddActionCommend(Action tutorialAction, Func<bool> endCondtion = null, Action endActoin = null)
        => tutorialCommends.Add(CreateActionCommend(tutorialAction, endCondtion, endActoin));

    protected void AddTargetUINameToIndexNameActionCommend<T>(Func<T, bool> condition, string indexName) where T : UnityEngine.Object
        => AddActionCommend
        (() => FindObjectsOfType<T>() 
        .Where(condition)
        .First()
        .name = indexName);
}
