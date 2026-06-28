using System;
using Core;
using UnityEngine;

namespace Systems
{
    public class GameBootstrap : MonoBehaviour
    {
        public static GameBootstrap Instance { get; private set; }

        public Game Game { get; private set; }

        void Awake()
        {
            Instance = this;
            Game = new Game();
        }

        private void Start()
        {
            
        }
    }
}