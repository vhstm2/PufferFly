using Google.Play.AppUpdate;
using Google.Play.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private AppUpdateManager appUpdateManager;
    private PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation;

    void Start()
    {
#if UNITY_EDITOR
#else
        appUpdateManager = new AppUpdateManager();
      appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();

        StartCoroutine(CheckForUpdate());
#endif
    }

    IEnumerator CheckForUpdate()
    {
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions(allowAssetPackDeletion: true);

            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));

        }
        else
        {

        }


    }

    IEnumerator StartImmediateUpdate(AppUpdateInfo info, AppUpdateOptions updateOptions)
    {
        var startUpdateRequest = appUpdateManager.StartUpdate(info, updateOptions);
        yield return startUpdateRequest;

        while (!startUpdateRequest.IsDone)
        {
            // For flexible flow,the user can continue to use the app while
            // the update downloads in the background. You can implement a
            // progress bar showing the download status during this time.
            yield return null;
        }
        StartCoroutine(CompleteFlexibleUpdate());
    }

    private IEnumerator CompleteFlexibleUpdate()
    {
        var result = appUpdateManager.CompleteUpdate();
        yield return result;
        if (result.IsSuccessful)
        {
            Debug.Log(result.GetResult());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
}
