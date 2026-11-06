using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.Utils
{
    public static class ViewUtils
    {
        public const float BaseScreenHalfHeight = 6.4f;
        public const float BaseScreenHeight = BaseScreenHalfHeight * 2;
        public const float BaseScreenWidth = BaseScreenHeight / 16 * 12;

        public static float ScreenHalfHeight =>
            BaseScreenHalfHeight * Mathf.Max((float) Screen.height / Screen.width / (16f / 9f), 1f);

        public static float GetOneLineTextWidth(Text text)
        {
            var totalLength = 0;

            var font = text.font;
            var characterInfo = new CharacterInfo();

            var arr = text.text.ToCharArray();
            var fontSize = text.cachedTextGenerator.fontSizeUsedForBestFit;

            foreach (var c in arr)
            {
                font.GetCharacterInfo(c, out characterInfo, fontSize);
                totalLength += characterInfo.advance;
            }

            return totalLength;
        }

        public static Color32 ConvertRgbaHexColor(uint aCol)
        {
            return new Color32
            {
                a = (byte) (aCol & 0xFF),
                b = (byte) ((aCol >> 8) & 0xFF),
                g = (byte) ((aCol >> 16) & 0xFF),
                r = (byte) ((aCol >> 24) & 0xFF)
            };
            ;
        }
    }
}