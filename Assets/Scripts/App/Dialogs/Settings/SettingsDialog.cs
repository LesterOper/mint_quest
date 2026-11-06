using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using I2.Loc;
using Library.Common;
using MenuScripts;
using Swimmer.Library.Common;
using Swimmer.Library.Dialogs;
using UnityEngine;
using UnityEngine.UI;

public class SettingsDialog : Dialog
{
    [SerializeField] private Button _back;
    [SerializeField] private Button _prevLangBtn;
    [SerializeField] private Button _nextLangBtn;
    [SerializeField] private Button restore;
    [Space] 
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _vibroSlider;
    [SerializeField] private Image _soundHandleImage;
    [SerializeField] private Image _vibroHandleImage;
    [Header("SOCIAL")]
    [SerializeField] private Button _vkButton;
    [SerializeField] private Button _instaButton;
    [SerializeField] private Button _youtubeButton;
    [SerializeField] private Button _facebookButton;
    [SerializeField] private Button _contactUsButton;
    [Space]
    [SerializeField] private Button _gdpr;
    [SerializeField] private Button _premButton;
    [SerializeField] private Image premIcon;
    [SerializeField] private Sprite premIconDeactive;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _premMenu;
    [Space]
    [SerializeField] private Sprite _blackToggleSprite; 
    [SerializeField] private Sprite _mintToggleSprite;

    [SerializeField] private RectTransform panel;
    private AudioMixerController _audioMixerController;

    //------SOCIAL--------------//
    private const string FACEBOOK_URL = "https://www.facebook.com/groups/1592033661110248";
    private const string VK_URL = "https://vk.com/alexplay_net";
    private const string INSTAGRAM_URL = "https://www.instagram.com/alexplaynet_official/";
    private const string YOUTUBE_URL = "https://www.youtube.com/channel/UC3T-TUKxos87BUdZMs1EUUw";

    private const string POLICY_URL = "https://alexplay.net/privacy-policy/";

    //-------------------------//
    private const string KEY_VOLUME = "sfx_volume";
    private const string KEY_VIBRO = "key_vibro_change";

    private Animation _animation;

    void Start()
    {
        base.Start();
        if (Context.DataHelper.GetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED) == 1)
        {
            premIcon.sprite = premIconDeactive;
            _premButton.interactable = false;
        }
        else
        {
            Context.AdsHelper.UpdateBannerVisibility();
        }

        if (Context.AdsHelper.isBannerShown)
        {
            panel.DOAnchorPosY(panel.anchoredPosition.y + 120f, 0.0f);
        }
        _animation = GetComponent<Animation>();
        _animation.Play("settings_anim_start");

        _audioMixerController = FindObjectOfType<AudioMixerController>();

        restore.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            Context.PurchaseUtil.RestoreCompletedTransactions(error =>
            {
                if (error != null)
                {
                    Debug.Log("[RESTORE] : " + error);
                }
            });
        });
        
        _gdpr.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            Application.OpenURL(POLICY_URL);
        });

        _premButton.onClick.AddListener((() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            Debug.Log("PURCHASE");
            if (!isPurchasedNoAds())
            {
                Context.PurchaseUtil.MakePurchase("mint_no_ads_product", error =>
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        //AppMetrica.Instance.ReportError("NO ADS PURCHASING", error);
                        Debug.Log("[Purchase]: " + error);
                    }
                    else
                    {
                        //AppMetrica.Instance.ReportEvent("NO ADS PURCHASED");
                        Context.DataHelper.SetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED, 1);
                        Context.AdsHelper.DisableAds();
                        premIcon.sprite = premIconDeactive;
                        _premButton.interactable = false;
                    }
                });
            }
        }));

        //SOCIAL
        _vkButton.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX,PlayerPrefs.GetFloat(KEY_VOLUME));
            Application.OpenURL(VK_URL);
        });

        _instaButton.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            Application.OpenURL(INSTAGRAM_URL);
        });
        _youtubeButton.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            Application.OpenURL(YOUTUBE_URL);
        });
        _facebookButton.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            Application.OpenURL(FACEBOOK_URL);
        });
        _contactUsButton.onClick.AddListener(SendEmail);


        var soundVolume = PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME_MUSIC);
        _soundSlider.value = soundVolume;
        
        if (soundVolume < 0.25f)
            _soundHandleImage.sprite = _blackToggleSprite;
        else
            _soundHandleImage.sprite = _mintToggleSprite;

        var vibroVolume = PlayerPrefs.GetFloat(KEY_VIBRO, 2);
        _vibroSlider.value = vibroVolume < 1 ? 0 : 2;
        
        if (vibroVolume < 1)
            _vibroHandleImage.sprite = _blackToggleSprite;
        else
            _vibroHandleImage.sprite = _mintToggleSprite;
        

        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
        _vibroSlider.onValueChanged.AddListener(ChangeVibration);

        _nextLangBtn.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX,
                PlayerPrefs.GetFloat(KEY_VOLUME));
            LocalizationManager.CurrentLanguage = GetNextLanguage();
        });
        _prevLangBtn.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(KEY_VOLUME));
            LocalizationManager.CurrentLanguage = GetPrevLanguage();
        });
    }

    private void SendEmail()
    {
        var mailToString = "mailto:info@alexplay.net" +
                           "?" +
                           "subject=" +
                           "Mint%20Quest" +
                           "&" +
                           "body=" +
                           "%0D%0A%0D%0A%0D%0A" +
                           "%0D%0A%20Device:%20" + SystemInfo.deviceModel + ",%20" +
                           "%0D%0A%20OS%20version:%20" + SystemInfo.operatingSystem + ",%20" +
                           "%0D%0A%20App%20version:%20" + Application.version + ",%20" +
                           "%0D%0A%20" + ((Application.platform == RuntimePlatform.Android) ? "adid" : "idfa") +
                           ":%20" + IronSource.Agent.getAdvertiserId();
        var replace = mailToString.Replace(" ", "%20");
        Debug.Log(replace);
        Application.OpenURL(replace);
    }

    private void ChangeSoundVolume(float volume)
    {
        Debug.Log("VOLUME CHANGED SLIDER: " + volume);
        if (volume <= 0f)
        {
            //volume = -80;
            _soundHandleImage.sprite = _blackToggleSprite;
        }
        else
        {
            _soundHandleImage.sprite = _mintToggleSprite;
        }

        float volume_music = 0, volume_sound = 0;
        if (volume > 0.7f)
        {
            volume_music = 0.7f;
            volume_sound = 0.5f;
        }

        else if (volume > 0.5f)
        {
            volume_music = volume;
            volume_sound = 0.5f;
        }
        Context.SoundPlayer.UpdateEachSourceVolume(volume_sound,AudioMixerController.MasterMixerGroup.SFX);
        _audioMixerController.SetMixerGroupVolume(AudioMixerController.MasterMixerGroup.SFX, 0);
        Context.MusicPlayer.ChangeChannelVolume(MusicPlayer.Channel.Ambient, volume_music);
        PlayerPrefs.SetFloat(AppConstans.KEY_VOLUME, volume_sound);
        PlayerPrefs.SetFloat(AppConstans.KEY_VOLUME_MUSIC, volume_music);
        Debug.Log("VOLUME CHANGED SLIDER SOUND PF: " + PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        Debug.Log("VOLUME CHANGED SLIDER MUSIC PF: " + PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME_MUSIC));
    }


    private void ChangeVibration(float vibro)
    {
        if (vibro < 1)
        {
            vibro = 0;
            _vibroSlider.value = vibro;
            _vibroHandleImage.sprite = _blackToggleSprite;
        }
        else
        {
            vibro = 2;
            _vibroSlider.value = vibro;
            _vibroHandleImage.sprite = _mintToggleSprite;

        }

        PlayerPrefs.SetFloat(KEY_VIBRO, vibro);
    }


    private string GetNextLanguage()
    {
        int curLangId = LocalizationManager.GetAllLanguages().IndexOf(LocalizationManager.CurrentLanguage);
        if (curLangId == LocalizationManager.GetAllLanguages().Count - 1)
        {
            curLangId = 0;
        }
        else
        {
            curLangId++;
        }

        return LocalizationManager.GetAllLanguages()[curLangId];
    }


    private string GetPrevLanguage()
    {
        int curLangId = LocalizationManager.GetAllLanguages().IndexOf(LocalizationManager.CurrentLanguage);
        if (curLangId == 0)
        {
            curLangId = LocalizationManager.GetAllLanguages().Count - 1;
        }
        else
        {
            curLangId--;
        }

        return LocalizationManager.GetAllLanguages()[curLangId];
    }

    private IEnumerator HideWithAnimation()
    {
        _animation.Play("settings_anim_end");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AnimationController>().StartButtonsAnimation();
        FindObjectOfType<MainMenuController>().ActivateButtons();
        yield return new WaitForSeconds(0.5f);
        base.Hide();
    }

    public override void Hide()
    {
        StartCoroutine(HideWithAnimation());
    }

    private bool isPurchasedNoAds()
    {
        return Context.DataHelper.GetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED) == 1;
    }
}