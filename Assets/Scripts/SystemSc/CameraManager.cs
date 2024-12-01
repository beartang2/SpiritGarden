using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public PostProcessProfile[] mapProfiles;

    public void ChangeProfile(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= mapProfiles.Length)
        {
            Debug.LogWarning("out of index");
            return;
        }

        postProcessVolume.profile = mapProfiles[mapIndex];
    }

    internal void ChangeProfile(object currentMapIndex)
    {
        throw new NotImplementedException();
    }
}
