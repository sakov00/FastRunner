using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Player
{
    public sealed class PlayerProvider : MonoProvider<PlayerComponent> { }

    [Serializable]
    public struct PlayerComponent { }
}
