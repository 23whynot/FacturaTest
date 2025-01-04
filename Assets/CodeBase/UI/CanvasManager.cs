using CodeBase.UI.Panels;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class CanvasManager : MonoBehaviour
    {
        private GameObject _mainMenuPanel;
        private GameObject _looseMenuPanel;
        private GameObject _winMenuPanel;
        
        private MainMenu _mainMenu;
        private LooseMenu _looseMenu;
        private WinMenu _winMenu;
        private Car.Car _car;

        [Inject]
        public void Construct(MainMenu mainMenu, Car.Car car, LooseMenu looseMenu, WinMenu winMenu)
        {
            _mainMenu = mainMenu;
            _car = car;
            _looseMenu = looseMenu;
            _winMenu = winMenu;
        }

        private void Awake()
        {
            LoadAndInstantiatePanels();
        }

        private void Start()
        {
            ActivateMainMenu();
            _mainMenu.OnStartGame += ActivateHudMenu;
            _car.OnDeath += ActivateDeathMenu;
            _car.OnEndLevel += ActivateWinMenu;

            Time.timeScale = 1f;
        }

        private void LoadAndInstantiatePanels()
        {
            _mainMenuPanel = _mainMenu.gameObject;
            _mainMenuPanel.transform.SetParent(transform, false);
            _looseMenuPanel = _looseMenu.gameObject;
            _looseMenuPanel.transform.SetParent(transform, false);
            _winMenuPanel = _winMenu.gameObject;
            _winMenuPanel.transform.SetParent(transform, false);
          
        }

        private void ActivateMainMenu()
        {
            _looseMenuPanel.SetActive(false);
            _winMenuPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void ActivateHudMenu()
        {
            _mainMenuPanel.SetActive(false);
            _looseMenuPanel.SetActive(false);
            _winMenuPanel.SetActive(false);
        }

        private void ActivateDeathMenu()
        {
            _winMenuPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _looseMenuPanel.SetActive(true);

            Time.timeScale = 0f;
        }

        private void ActivateWinMenu()
        {
            _looseMenuPanel.SetActive(false);
            _winMenuPanel.SetActive(false);
            _winMenuPanel.SetActive(true);

            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            _mainMenu.OnStartGame -= ActivateHudMenu;
            _car.OnDeath -= ActivateDeathMenu;
            _car.OnEndLevel -= ActivateWinMenu;
        }
        
    }
}
