using UnityEngine;

namespace Gameplay.UI
{
    public class GameplaySystemButton : MonoBehaviour
    {
        [SerializeField] private GameplaySystemsManager gameplaySystemsManager;
        [SerializeField] private GameplaySystem systemToSelect;

        public void OnClick()
        {
            gameplaySystemsManager.SetActiveSystem(systemToSelect);
        }
    }
}
