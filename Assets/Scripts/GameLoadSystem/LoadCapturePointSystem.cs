using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadCapturePointSystem
{
    public static IEnumerator LoadCapturePoint(List<CapturePointData> capturePointDatas)
    {
        CapturePointManager.Instance.LoadCapturePoint(capturePointDatas);

        yield return null;
    }
}