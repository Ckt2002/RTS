using System.Collections.Generic;
using GameSave;

public class SaveCapturePointSystem
{
    public static List<CapturePointData> SaveCapturePoint()
    {
        var lstCapturePoint = new List<CapturePointData>();
        var capturePointDatas = CapturePointManager.Instance.GetCapturePoints();
        foreach (var capturePointData in capturePointDatas)
        {
            var data = new CapturePointData
            {
                CapturePointIndex = capturePointData.Key,
                CoroutineData = new CoroutineProgressData
                {
                    CurrentTime = capturePointData.Value
                }
            };
            lstCapturePoint.Add(data);
        }

        return lstCapturePoint;
    }
}