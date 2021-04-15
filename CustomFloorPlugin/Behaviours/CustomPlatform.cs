﻿using System;

using UnityEngine;


namespace CustomFloorPlugin
{
    public class CustomPlatform : MonoBehaviour, IComparable<CustomPlatform>
    {
        [Header("Platform Description")]
        public string platName = "MyCustomPlatform";
        public string platAuthor = "MyName";
        public Sprite? icon;
        [Space]
        [Header("Hide Environment")]
        public bool hideHighway = false;
        public bool hideTowers = false;
        public bool hideDefaultPlatform = false;
        public bool hideEQVisualizer = false;
        public bool hideSmallRings = false;
        public bool hideBigRings = false;
        public bool hideBackColumns = false;
        public bool hideBackLasers = false;
        public bool hideDoubleColorLasers = false;
        public bool hideRotatingLasers = false;
        public bool hideTrackLights = false;

        [SerializeField] internal string platHash = "";
        [SerializeField] internal string fullPath = "";
        [SerializeField] internal bool isDescriptor = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public int CompareTo(CustomPlatform platform) => platName.CompareTo(platform.platName);
    }
}