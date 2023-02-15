using System;
using Voody.UniLeo.Lite;

namespace Ecs.Components
{
    public sealed class Monster : MonoProvider<MonsterComponent>
    {
    }

    [Serializable]
    public struct MonsterComponent
    {
    }
}