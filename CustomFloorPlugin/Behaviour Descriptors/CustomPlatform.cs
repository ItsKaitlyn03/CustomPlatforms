﻿using System;

using UnityEngine;


namespace CustomFloorPlugin
{
    public class CustomPlatform : MonoBehaviour, IComparable<CustomPlatform>
    {
        public string platName = "MyCustomPlatform";
        public string platAuthor = "MyName";
        public Sprite icon;

        public bool hideHighway = false;
        public bool hideTowers = false;
        public bool hideDefaultPlatform = false;
        public bool hideEQVisualizer = false;
        public bool hideSmallRings = false;
        public bool hideBigRings = false;
        public bool hideBackColumns = false;
        public bool hideBackLasers = false;
        public bool hideDoubleLasers = false;
        public bool hideDoubleColorLasers = false;
        public bool hideRotatingLasers = false;
        public bool hideTrackLights = false;

        internal string platHash;
        internal string fullPath;

        public void Awake()
        {
            gameObject.SetActive(false);
        }

        public int CompareTo(CustomPlatform platform)
        {
            return platName.CompareTo(platform.platName);
        }
    }
}