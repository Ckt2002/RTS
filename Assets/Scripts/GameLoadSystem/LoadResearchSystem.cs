using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadResearchSystem : MonoBehaviour
{
    public static IEnumerator LoadResearch(List<ResearchData> researchDatas)
    {
        foreach (var data in researchDatas) Debug.Log(data.unitName);
        yield return null;
    }
}