using Library.Common;
using Swimmer.Library.Data;
using Swimmer.Library.L10n;
using UnityEngine;

namespace Swimmer.Library.Common
{
    [RequireComponent(typeof(MusicPlayer))]
    [RequireComponent(typeof(SoundPlayer))]
    //[RequireComponent(typeof(ServicesHelper))]
    //[RequireComponent(typeof(SavingHelper))]
    [RequireComponent(typeof(AdsHelper))]
    //[RequireComponent(typeof(PremPurchaseHelper))]
    [RequireComponent(typeof(LocaleHelper))]
    [RequireComponent(typeof(ThreadHelper))]
    [RequireComponent(typeof(SceneData))]
    [RequireComponent(typeof(InterstitalDetector))]
    [RequireComponent(typeof(PurchaseUtils))]
    [DisallowMultipleComponent]
    public class Context : MonoBehaviour
    {
        protected static Context instance;

        public static MusicPlayer MusicPlayer { get; private set; }
        public static SoundPlayer SoundPlayer { get; private set; }
        //public static ServicesHelper ServicesHelper { get; private set; }
        //public static SavingHelper SavingHelper { get; private set; }
        //public static PremPurchaseHelper PremPurchaseHelper { get; private set; }
        public static AdsHelper AdsHelper { get; private set; }
        public static LocaleHelper LocaleHelper { get; private set; }
        public static DataHelper DataHelper { get; private set; }
        public static ThreadHelper ThreadHelper { get; private set; }
        public static SceneData SceneData { get; private set; }
        
        public static InterstitalDetector InterDetect { get; private set; }
        
        public static PurchaseUtils PurchaseUtil { get; private set; }

        public bool Redundant { get; set; }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Redundant = true;
                Destroy(gameObject);
            }
            else
            {
                Redundant = false;
                instance = this;

                
                MusicPlayer = GetComponent<MusicPlayer>();
                SoundPlayer = GetComponent<SoundPlayer>();
                //ServicesHelper = GetComponent<ServicesHelper>();
                //SavingHelper = GetComponent<SavingHelper>();
                //PremPurchaseHelper = GetComponent<PremPurchaseHelper>();
                AdsHelper = GetComponent<AdsHelper>();
                LocaleHelper = GetComponent<LocaleHelper>();
                DataHelper = GetComponent<DataHelper>();
                ThreadHelper = GetComponent<ThreadHelper>();
                SceneData = GetComponent<SceneData>();
                InterDetect = GetComponent<InterstitalDetector>();
                PurchaseUtil = GetComponent<PurchaseUtils>();

                //ServicesHelper.OnSignInChanged += OnSignedChanged;
                //PremPurchaseHelper.OnApplyPurchase += OnApplyPurchase;
                if (PlayerPrefs.GetInt(AppConstans.IS_FIRST_LAUNCH) == 0)
                {
                    PlayerPrefs.SetFloat(AppConstans.KEY_VOLUME, 0.5f);
                    PlayerPrefs.SetFloat(AppConstans.KEY_VOLUME_MUSIC, 0.7f);
                    PlayerPrefs.SetInt(AppConstans.IS_FIRST_LAUNCH, 1);
                }

                MusicPlayer.PlayMusic("mint_music", MusicPlayer.Channel.Ambient, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME_MUSIC), false);
                
                Debug.Log("[Context] Awake : initialized");
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
                DontDestroyOnLoad(gameObject);
            }

            Debug.Log("[StaticContainer] Awake: ");
        }

        protected virtual void OnApplyPurchase(string sku)
        {
        }

        protected virtual void OnSignedChanged(bool signed)
        {
        }
    }
}