﻿using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Object
{
    public sealed class CollisionProvider : MonoProvider<CollisionComponent> { }

    public struct CollisionComponent
    {
        public List<EcsEntity> CollisionEntity;
    }
}