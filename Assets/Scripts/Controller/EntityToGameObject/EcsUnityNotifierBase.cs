﻿using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Controller.EntityToGameObject
{
    public abstract class EcsUnityNotifierBase : MonoBehaviour
    {
        protected ref EcsEntity Entity => ref Provider.Entity;

        private EcsUnityProvider Provider
        {
            get
            {
                if (_provider != null) return _provider;
                if (!TryGetComponent(out _provider)) throw new Exception();
                return _provider;
            }
        }

        private EcsUnityProvider _provider;
    }
}