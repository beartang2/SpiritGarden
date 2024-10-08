using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField] private Text dayTimeUI;

    [SerializeField, Range(0, 23)] private float timeOfDay;
    public float timeSpeed = 0.5f;
    public int dayCount = 1;
    public bool isCounted = false;

    public event Action OnDayPassed; // 하루가 지날 때 발생하는 이벤트

    private void Start()
    {
        Debug.Log("Day " + dayCount);
    }

    private void Update()
    {
        if(preset == null) //예외처리
        {
            return;
        }

        if(Application.isPlaying) //프로그램이 실행중이면
        {
            timeOfDay += Time.deltaTime * timeSpeed; //시간이 흐르도록 함
            timeOfDay %= 24; //1-24 시간
            UpdateLighting(timeOfDay / 24); //빛 위치 갱신
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }

        CheckDay();
        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        dayTimeUI.text
            = "날짜 : " + dayCount.ToString() + "일\n"
            + "시간 : " + ((int)(timeOfDay % 24)).ToString() + "시";
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

    public void CheckDay()
    {
        if(!isCounted && (int)(timeOfDay % 24) == 0)
        {            
            if (OnDayPassed != null)
            {
                OnDayPassed(); // 하루가 지나면 구독자들에게 알림
            }

            dayCount++;
            isCounted = true;
            Debug.Log("Day " + dayCount);
        }
        else if(isCounted && (int)(timeOfDay % 24) > 8)
        {
            isCounted = false;
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
