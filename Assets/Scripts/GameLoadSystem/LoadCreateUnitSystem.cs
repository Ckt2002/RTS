using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCreateUnitSystem : MonoBehaviour
{
    public static IEnumerator LoadCreateUnitProgress(List<BuyUnitData> buyUnitDatas)
    {
        foreach (var data in buyUnitDatas) Debug.Log(data.unitName);
        yield return null;
    }
}