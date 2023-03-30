using UnityEngine;

namespace PlatformerMVC
{
    public enum StoryType
    {
        Common,
        Resettable
    }
    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / QuestSystem / QuestStoryCfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] questsConfif;
        public QuestType type;

    }
}