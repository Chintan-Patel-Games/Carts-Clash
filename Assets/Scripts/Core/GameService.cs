using CartClash.Utilities;
using UnityEngine;

namespace CartClash.Core
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("UI")]
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}