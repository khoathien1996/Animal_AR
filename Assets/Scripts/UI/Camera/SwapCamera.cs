using UnityEngine;
using System.Collections;
using Vuforia;

public class SwapCamera : BaseClickButton {

    public VuforiaBehaviour vuforiaBehaviour;
    private bool isBack = true;
    public override void OnClicked()
    {
        base.OnClicked();
        isBack = !isBack;
        if(isBack)
        {
            // using back camera
            UseBackCamera();
        }
        else
        {
            UseFrontCamera();
        }
    }

    public void UseBackCamera()
    {
        RestartCamera(CameraDevice.CameraDirection.CAMERA_BACK);
    }

    public void UseFrontCamera()
    {
        RestartCamera(CameraDevice.CameraDirection.CAMERA_FRONT);
    }

    private void RestartCamera(CameraDevice.CameraDirection direction)
    {
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        if (CameraDevice.Instance.Init(direction))
        {
            //Debug.Log("change ok!!");
        }

        CameraDevice.Instance.Start();
    }
}
