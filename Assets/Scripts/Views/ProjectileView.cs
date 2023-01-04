﻿using Controller.EntityToGameObject;
using Model.Components.Extensions.Interfaces.Pool;
using UnityEngine;
using Zenject;

namespace Views
{
    [RequireComponent(typeof(EcsUnityProvider))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileView : GameObjectView, IPhysicsPoolObject
    {
        public Rigidbody2D Rigidbody2D { get; private set; }

        protected sealed override void Awake()
        {
            base.Awake();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public new class Factory : PlaceholderFactory<ProjectileView>
        {
        }
    }
}