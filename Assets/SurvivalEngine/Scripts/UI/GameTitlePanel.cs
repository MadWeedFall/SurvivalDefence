using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalEngine
{
    public class GameTitlePanel : UISlotPanel
    {

        private static GameTitlePanel _instance;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        protected override void Start()
        {
            base.Start();
            TheGame.Get().Pause();
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void Hide(bool instant = false)
        {
            base.Hide(instant);
            TheGame.Get().Unpause();
        }

        public void OnClickSave()
        {
            TheGame.Get().Save();
        }

        public void OnClickLoad()
        {
            if (PlayerData.HasLastSave())
                StartCoroutine(LoadRoutine());
            else
                StartCoroutine(NewRoutine());
        }

        public void OnClickNew()
        {
            StartCoroutine(NewRoutine());
        }

        private IEnumerator LoadRoutine()
        {
            BlackPanel.Get().Show();

            yield return new WaitForSeconds(1f);

            TheGame.Load();
        }

        private IEnumerator NewRoutine()
        {
            BlackPanel.Get().Show();

            yield return new WaitForSeconds(1f);

            TheGame.NewGame();
        }

        public void OnClickMusicToggle()
        {
            PlayerData.Get().master_volume = PlayerData.Get().master_volume > 0.1f ? 0f : 1f;
            TheAudio.Get().RefreshVolume();
        }

        public static GameTitlePanel Get()
        {
            return _instance;
        }
    }

}
