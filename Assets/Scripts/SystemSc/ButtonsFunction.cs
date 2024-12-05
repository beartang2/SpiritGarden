using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ButtonsFunction : MonoBehaviour
{
    public Slider slider;
    public GameObject settingPanel;
    //public Image soundWaveImg;
    //public List<Sprite> soundWaves;
    public AudioSource audioSc; // audioSc ����
    AudioClip audioClip;

    private void Start()
    {
        
    }

    public void SingleMode()
    {
        // ���丮 ��
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        // ��Ƽ ��� �߰�
    }

    public void soundVolume()
    {
        // �����̴� ���� ���� AudioMixer�� ������ ������ ����
        float sliderValue = slider.value; // �����̴� �� (0 ~ 1)

        audioSc.volume = sliderValue;

        /*
        if (slider.value == 0) soundWaveImg.sprite = soundWaves[0];
        else if (slider.value < 0.25) soundWaveImg.sprite = soundWaves[1];
        else if (slider.value < 0.5) soundWaveImg.sprite = soundWaves[2];
        else if (slider.value >= 0.75) soundWaveImg.sprite = soundWaves[3];
        */
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

    public void Exit()
    {
        // ���� ����
#if UNITY_EDITOR //��ó����� ����Ƽ �����Ͱ� �������϶� �÷��̸� ���ߵ�����.
        UnityEditor.EditorApplication.isPlaying = false; //���ø����̼� �÷��̸� false�� ��.
#else //����Ƽ�����Ͱ� �������� �ƴҶ� �۵�
                Application.Quit(); //���ø����̼��� ����
#endif
    }
}
