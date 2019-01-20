using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class EnemyBotsController : BaseController
    {
        private List<EnemyBot> botList = new List<EnemyBot>();
        private Transform targetTransform;

        private void Start()
        {
            targetTransform = PlayerModel.LocalPlayer.transform;

            foreach (EnemyBot item in FindObjectsOfType<EnemyBot>()) AddBot(item);
        }

        public void AddBot(EnemyBot bot)
        {
            if (botList.Contains(bot)) return;
            botList.Add(bot);
            bot.SetTarget(targetTransform);
        }

        public void RemoveBot(EnemyBot bot)
        {
            if (!botList.Contains(bot)) return;
            botList.Remove(bot);
        }
    }
}