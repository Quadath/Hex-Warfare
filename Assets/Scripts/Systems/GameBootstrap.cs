using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Systems
{
    public class GameBootstrap : MonoBehaviour
    {
        public static GameBootstrap Instance { get; private set; }
        [SerializeField] List<UnitySystem> Systems;

        public Game Game { get; private set; }

        void Awake()
        {
            Instance = this;
            Game = new Game(this);
            foreach(var system in Systems)
                system.Init(Game);
        }
        

        private void FixedUpdate()
        {
           Game.Tick(Time.deltaTime, this);
        }
    }
}