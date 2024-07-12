using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public sealed class PlayerProvider : MonoProvider<PlayerComponent> { }

    [Serializable]
    public struct PlayerComponent { }
}
