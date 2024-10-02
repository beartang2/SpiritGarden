using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;

    //Variables
    [SerializeField, Range(1, 24)] private float timeOfDay;
    public float timeSpeed = 0.5f;
    private int dayCount = 1;

    private void Start()
    {
        Debug.Log("Day " + dayCount);
    }

    private void Update()
    {
        if(preset == null) //����ó��
        {
            return;
        }

        if(Application.isPlaying) //���α׷��� �������̸�
        {
            timeOfDay += Time.deltaTime * timeSpeed; //�ð��� �帣���� ��
            timeOfDay %= 24; //1-24 �ð�
            UpdateLighting(timeOfDay / 24); //�� ��ġ ����
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }

        CheckDay(timeOfDay);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) - 90f, -45f, 0f));
        }
    }

    private void CheckDay(float time)
    {
        if(time == 0)
        {
            dayCount++;
            Debug.Log("Day " + dayCount);
        }
    }

    private void OnValidate()
    {
        if(directionalLight != null)
        {
            return;
        }

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();

            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
