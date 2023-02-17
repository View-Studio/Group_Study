using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tutorial_AI : MonoBehaviour
{
    int _gold;
    readonly byte AI_ID = 1;

    void Awake()
    {
        _gold = 15;
        Multi_GameManager.instance.OnGameStart += DrawUnits;
        Multi_StageManager.Instance.OnUpdateStage += (stage) => OnChangeStage();
    }

    void OnChangeStage()
    {
        _gold += 10;
        DrawUnits();
    }

    void DrawUnits() => StartCoroutine(Co_DrawUnits());

    IEnumerator Co_DrawUnits()
    {
        while (_gold >= 5)
        {
            _gold -= 5;
            Multi_SpawnManagers.NormalUnit.Spawn(new UnitFlags(Random.Range(0, 3), 0), AI_ID);
            yield return new WaitForSeconds(0.2f);
            TryCombine();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void TryCombine()
    {
        var flags = new UnitCombineSystem().GetCombinableUnitFalgs(Multi_UnitManager.Instance.Master.GetUnits(AI_ID).Select(x => x.UnitFlags));
        if (flags.Count() == 0)
            return;

        Multi_UnitManager.Instance.Combine(flags.First(), AI_ID);
    }
}
