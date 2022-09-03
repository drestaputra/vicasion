using System.Collections;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.SpatialTracking;
 
public class vrmode : MonoBehaviour
{

    /// <summary>
    /// Gets a value indicating whether the VR mode is enabled.
    /// </summary>
    [SerializeField] bool _isVrModeEnabled
    {
        get
        {
            return XRGeneralSettings.Instance.Manager.isInitializationComplete;
        }
    }
    public bool isSetEnabled = false;
    

    [SerializeField] TrackedPoseDriver trackedPoseDriver;

    
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        // Configures the app to not shut down the screen and sets the brightness to maximum.
        // Brightness control is expected to work only in iOS, see:
        // https://docs.unity3d.com/ScriptReference/Screen-brightness.html.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        // Checks if the device parameters are stored and scans them if not.
        // This is only required if the XR plugin is initialized on startup,
        // otherwise these API calls can be removed and just be used when the XR
        // plugin is started.
        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }
            Debug.Log("mode:"+isSetEnabled);
            if(isSetEnabled){
                EnterVR();
            }else{
                ExitVR();
            }
            Api.UpdateScreenParams();
        
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
       if (Api.IsCloseButtonPressed)
        {
            Debug.Log("vrmode :"+_isVrModeEnabled);
            ExitVR();
        }
    }

    // vr mode camera.target eye = both
    // 3d mode camera.target eye = none
    /// <summary>
    /// Enters VR mode.
    /// </summary>
    public void EnterVR()
    {
        // bug rotasi
        // trackedPoseDriver.enabled = true;
        // int screenWidth = Screen.width - 400;
        // Screen.SetResolution(screenWidth, Screen.height, Screen.fullScreen);
        StartCoroutine(StartXR());
        // GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
    }

    /// <summary>
    /// Exits VR mode.
    /// </summary>
    public void ExitVR()
    {
        // bug rotasi
        // trackedPoseDriver.enabled = false;
        StopXR();
        // https://github.com/googlevr/gvr-unity-sdk/issues/628
        Camera.main.ResetAspect();
        // GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
    }

    /// <summary>
    /// Initializes and starts the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    ///
    /// <returns>
    /// Returns result value of <c>InitializeLoader</c> method from the XR General Settings Manager.
    /// </returns>
    public IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
        }
    }

    /// <summary>
    /// Stops and deinitializes the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    public void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");
    }
}