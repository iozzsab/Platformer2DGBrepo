using UnityEngine;

namespace PlatformerMVC.Model
{
    public class QuestCoinModel  : IQuestModel
    {
        public bool TryComplete(GameObject actor)
        {
            return actor.CompareTag("QuestCoin");
        }
    }
}