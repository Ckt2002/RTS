using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadCapturePointSystem : MonoBehaviour
{
    public static IEnumerable LoadCapturePoint(List<CapturePointData> capturePointDatas)
    {
        foreach (var data in capturePointDatas) Debug.Log(data.CapturePointIndex);
        yield return null;
    }
}