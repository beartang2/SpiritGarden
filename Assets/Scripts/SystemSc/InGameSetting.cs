using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSetting : MonoBehaviour
{
    public Slider slider;
    public GameObject settingPanel;
    //public Image soundWaveImg;
    //public List<Sprite> soundWaves;
    public AudioSource audioSc; // audioSc 참조
    AudioClip audioClip;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    public void soundVolume()
    {
        // 슬라이더 값에 따라 AudioMixer의 마스터 볼륨을 조정
        float sliderValue = slider.value; // 슬라이더 값 (0 ~ 1)

        audioSc.volume = sliderValue;
    }

    public void OpenMenu()
    {
        settingPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        settingPanel.SetActive(false);
    }

    public void SaveSettingData()
    {
        print("Save");
        settingPanel.SetActive(false);
    }
}
