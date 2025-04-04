﻿using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class CapturePointManager : MonoBehaviour
{
    public static CapturePointManager Instance;

    private List<CapturePoint> capturePoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        capturePoints = new List<CapturePoint>();
    }

    public void AddNewCapturePoint(CapturePoint capturePoint)
    {
        capturePoints.Add(capturePoint);
    }

    public void LoadCapturePoint(List<CapturePointData> capturePointDatas)
    {
        foreach (var capturePointData in capturePointDatas)
            capturePoints[capturePointData.CapturePointIndex].elapsedTime = capturePointData.CoroutineData.CurrentTime;
    }

    public List<KeyValuePair<int, float>> GetCapturePoints()
    {
        var capturePointDatas = new List<KeyValuePair<int, float>>();
        for (var i = 0; i < capturePoints.Count; i++)
        {
            var data = new KeyValuePair<int, float>(i, capturePoints[i].elapsedTime);
            capturePointDatas.Add(data);
        }

        return capturePointDatas;
    }
}