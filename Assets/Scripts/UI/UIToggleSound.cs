using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleSound : MonoBehaviour
{

    [SerializeField] private Sprite soundOn;

    [SerializeField] private Sprite soundOff;

    private string m_keySound = "prefs_key_sound";

    private Button m_btnSound;

    private Image m_sprite;

    private bool m_sound;

    private void Awake()
    {
        m_btnSound = GetComponent<Button>();
        m_sprite = GetComponent<Image>();

    }

    private void OnEnable()
    {
        m_btnSound.onClick.AddListener(OnClickSound);

        m_sound = true;
        if (PlayerPrefs.HasKey(m_keySound))
        {
            m_sound = PlayerPrefs.GetInt(m_keySound) == 1;
        }

        ToggleSound();
    }

    private void OnDisable()
    {
        if (m_btnSound) m_btnSound.onClick.RemoveAllListeners();
    }

    private void OnClickSound()
    {
        m_sound = !m_sound;
        ToggleSound();
    }

    private void ToggleSound()
    {
        PlayerPrefs.SetInt(m_keySound, m_sound ? 1 : 0);

        m_sprite.sprite = m_sound ? soundOn : soundOff;
    }
}
