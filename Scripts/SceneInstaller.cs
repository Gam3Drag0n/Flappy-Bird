using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _player;

    public override void InstallBindings()
    {
        PlayerInstall();
    }

    private void PlayerInstall()
    {
        Container.Bind<PlayerController>().FromInstance(_player).AsSingle();
        Container.QueueForInject(_player);
    }
}
