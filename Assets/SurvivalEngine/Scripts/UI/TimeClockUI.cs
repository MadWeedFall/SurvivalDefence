using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalEngine
{

    /// <summary>
    /// Clock showing days and time
    /// </summary>

    public class TimeClockUI : MonoBehaviour
    {
        public Text day_txt;
        public Text time_txt;
        public Image clock_fill;

        public Text show_txt;

        private PlayerCharacter character;

        public const string defualtShowTxt = "Rememer Work From 9 TO 17";

        public const string lackSleepTxt = "Lack of sleep is bad for you\nDon't forget work starts at 9";

        public const string deadManCanNotWorkTxt = "Hey, Are you OK?\nObviously you have to be alive to work";

        public const string workingTxt = "You have {0} assigned tasks or bugs to process";

        void Start()
        {
            character = PlayerCharacter.GetFirst();
        }

        private void UpdateShowTxt()
        {
            if(character.IsLackOfSleep())
            {
                show_txt.text = lackSleepTxt;
            }
            else if(character.Attributes.IsDepletingHP())
            {
                show_txt.text = deadManCanNotWorkTxt;
            }
            else if(character.IsInWorkingTime())
            {
                show_txt.text = string.Format(workingTxt,TheGame.Get().levels.GetTotalEnemyCount());
            }
            else
            {
                show_txt.text = defualtShowTxt;
            }
        }

        void Update()
        {
            PlayerData pdata = PlayerData.Get();
            int time_hours = Mathf.FloorToInt(pdata.day_time);
            int time_secs = Mathf.FloorToInt((pdata.day_time * 60f) % 60f);

            day_txt.text = "DAY " + pdata.day;
            time_txt.text = time_hours + ":" + time_secs.ToString("00");

            bool clockwise = pdata.day_time <= 12f;
            clock_fill.fillClockwise = clockwise;
            if (clockwise)
            {
                float value = pdata.day_time / 12f; //0f to 1f
                clock_fill.fillAmount = value;
            }
            else
            {
                float value = (pdata.day_time - 12f) / 12f; //0f to 1f
                clock_fill.fillAmount = 1f - value;
            }

            UpdateShowTxt();
        }
    }

}