using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UIPanel
{
    [SerializeField]
    private Slider _sfx;

    [SerializeField]
    private Slider _music;

    [SerializeField]
    private Button _privacy;

    [SerializeField]
    private Button _terms;

    [SerializeField]
    private Button _back;



    public event Action<float> OnSfxChanged;

    public event Action<float> OnMusicChanged;

    public event Action OnPrivacy;

    public event Action OnTerms;

    public event Action OnBack;



    private void Awake()
    {
        _sfx.onValueChanged.AddListener((f) => OnSfxChanged?.Invoke(f));

        _music.onValueChanged.AddListener((f) => OnMusicChanged?.Invoke(f));

        _privacy.onClick.AddListener(() => OnPrivacy?.Invoke());

        _terms.onClick.AddListener(() => OnTerms?.Invoke());

        _back.onClick.AddListener(() => OnBack?.Invoke());
    }



    public void Setup(float sfx, float music)
    {
        _sfx.value = sfx;

        _music.value = music;
    }



    private void OnDestroy()
    {
        _sfx.onValueChanged.RemoveAllListeners();

        _music.onValueChanged.RemoveAllListeners();

        _privacy.onClick.RemoveAllListeners();

        _terms.onClick.RemoveAllListeners();

        _back.onClick.RemoveAllListeners();
    }
}
