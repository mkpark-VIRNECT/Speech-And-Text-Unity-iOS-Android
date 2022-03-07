using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine(CheckPermission());
    }
    IEnumerator CheckPermission ()
    {
        yield return new WaitForEndOfFrame();
        int permissionCheckCount = 0;
        var permissionCallbacks = new PermissionCallbacks();
        permissionCallbacks.PermissionDenied += s =>
        {
            Debug.LogError($"Permission Denied : {s}");
            permissionCheckCount++;
        };
        permissionCallbacks.PermissionDeniedAndDontAskAgain += s =>
        {
            Debug.LogError($"Permission Denied and dont ask again : {s}");
            permissionCheckCount++;
        };
        permissionCallbacks.PermissionGranted += s =>
        {
            Debug.Log($"Permission Granted : {s}");
            permissionCheckCount++;
        };

        Permission.RequestUserPermissions(new string[] { Permission.ExternalStorageRead, Permission.ExternalStorageWrite, Permission.Camera }, permissionCallbacks);

        while ( permissionCheckCount < 3 )
        {
            yield return null;
        }
    }
}
