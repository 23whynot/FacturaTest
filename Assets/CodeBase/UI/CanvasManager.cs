using CodeBase.UI.Panels;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class CanvasManager : MonoBehaviour
    {
        private GameObject _mainMenuPanel;
        private GameObject _hudMenuPanel;
        
        private MainMenu _mainMenu;

        [Inject]
        public void Construct(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        private void Awake()
        {
            LoadAndInstantiatePanels();
        }

        private void Start()
        {
            ActivateMainMenu();
            _mainMenu.OnStartGame += ActivateHudMenu;
        }

        private void LoadAndInstantiatePanels()
        {
            _mainMenuPanel = _mainMenu.gameObject;
            _mainMenuPanel.transform.SetParent(transform, false);
          //  _hudMenuPanel = Resources.Load<GameObject>($"Panels/Hud");
           // _hudMenuPanel = Instantiate(_hudMenuPanel);
        }

        private void ActivateMainMenu()
        {
         //   _hudMenuPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void ActivateHudMenu()
        {
            _mainMenuPanel.SetActive(false);
          //  _hudMenuPanel.SetActive(true);
        }

        private void OnDisable()
        {
            _mainMenu.OnStartGame -= ActivateHudMenu;;
        }
        
    }
}
