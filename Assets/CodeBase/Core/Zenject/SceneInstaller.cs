using CodeBase.Camera;
using CodeBase.Car;
using CodeBase.Services;
using CodeBase.Spawner;
using CodeBase.Spawner.Factory;
using CodeBase.Turret;
using CodeBase.UI;
using CodeBase.UI.Panels;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Car.Car car;
        [SerializeField] private CarMovement carMovement;
        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private TurretController turretController;
        [SerializeField] private SpawnController spawnController;
        [SerializeField] private GroundSpawner groundSpawner;
        [SerializeField] private AudioService audioService;

        public override void InstallBindings()
        {
            Container.Bind<MainMenu>()
                .FromComponentInNewPrefabResource("Panels/MainMenu")
                .AsSingle()
                .NonLazy();
            Container.Bind<LooseMenu>()
                .FromComponentInNewPrefabResource("Panels/LooseMenu")
                .AsSingle()
                .NonLazy();
            Container.Bind<WinMenu>()
                .FromComponentInNewPrefabResource("Panels/WinMenu")
                .AsSingle()
                .NonLazy();
            Container.Bind<ScoreVisual>()
                .FromComponentInNewPrefabResource("Panels/HUD")
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CarMovement>().FromInstance(carMovement).AsSingle();
            Container.Bind<CanvasManager>().FromInstance(canvasManager).AsSingle();
            Container.Bind<CameraController>().FromInstance(cameraController).AsSingle();
            Container.Bind<TurretController>().FromInstance(turretController).AsSingle();
            Container.Bind<SpawnController>().FromInstance(spawnController).AsSingle();
            Container.Bind<GroundSpawner>().FromInstance(groundSpawner).AsSingle();
            Container.Bind<Car.Car>().FromInstance(car).AsSingle();
            Container.Bind<AudioService>().FromInstance(audioService).AsSingle();
            
            Container.Bind<ObjectPool.ObjectPool>().AsSingle();
            Container.Bind<StickmenFactory>().AsSingle();
            Container.Bind<ScoreService>().AsSingle();
        }
    }
}