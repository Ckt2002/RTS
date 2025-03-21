using System.Collections.Generic;
using System.Threading.Tasks;
using GameSave;

public class LoadCapturePointSystem
{
    public static Task LoadCapturePoint(List<CapturePointData> capturePointDatas)
    {
        if (capturePointDatas == null) return Task.CompletedTask;

        CapturePointManager.Instance.LoadCapturePoint(capturePointDatas);

        return Task.CompletedTask;
    }
}