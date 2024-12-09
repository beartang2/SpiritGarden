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
    public AudioSource sliderAudioSc;
    private AudioSource buttonAudioSc;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        buttonAudioSc = GetComponent<AudioSource>();
    }

    public void SingleMode()
    {
        buttonAudioSc.PlayOneShot(audioClip);

        // ���丮 ��
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        // ��Ƽ ��� �߰�
    }

    public void soundVolume()
    {
        // �����̴� ���� ���� AudioMixer�� ������ ������ ����
        float sliderValue = slider.value; // �����̴� �� (0 ~ 1)

        sliderAudioSc.volume = sliderValue;

        /*
        if (slider.value == 0) soundWaveImg.sprite = soundWaves[0];
        else if (slider.value < 0.25) soundWaveImg.sprite = soundWaves[1];
        else if (slider.value < 0.5) soundWaveImg.sprite = soundWaves[2];
        else if (slider.value >= 0.75) soundWaveImg.sprite = soundWaves[3];
        */
    }

    public void OpenMenu()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        settingPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        settingPanel.SetActive(false);
    }

    public void SaveSettingData()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        print("Save");
        settingPanel.SetActive(false);
    }

    public void Exit()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        // ���� ����
#if UNITY_EDITOR //��ó����� ����Ƽ �����Ͱ� �������϶� �÷��̸� ���ߵ�����.
        UnityEditor.EditorApplication.isPlaying = false; //���ø����̼� �÷��̸� false�� ��.
#else //����Ƽ�����Ͱ� �������� �ƴҶ� �۵�
                Application.Quit(); //���ø����̼��� ����
#endif
    }
}
