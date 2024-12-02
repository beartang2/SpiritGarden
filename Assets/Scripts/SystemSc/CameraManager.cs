using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public PostProcessProfile[] mapProfiles;
    public Color[] mapColors;
    Camera cam;

    public int mapCnt;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    public void ChangeProfile(int mapIndex, int mapCount)
    {
        if (mapIndex < 0 || mapIndex >= mapProfiles.Length)
        {
            Debug.LogWarning("out of index");
            return;
        }

        // 0¹øÀº µ¿±¼ 1¹øÀº ³óÀå
        postProcessVolume.profile = mapProfiles[mapIndex];
        cam.backgroundColor = mapColors[mapCount];
    }

    public void UpdateProfile()
    {
        cam.backgroundColor = mapColors[mapCnt];
    }
}
