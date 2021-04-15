﻿using System;

using UnityEngine;


namespace CustomFloorPlugin
{
    public class PrefabLightmapData : MonoBehaviour
    {
        [SerializeField]
        public Renderer[]? m_Renderers;
        [SerializeField]
        public Vector4[]? m_LightmapOffsetScales;
        [SerializeField]
        public Texture2D[]? m_Lightmaps;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void Start()
        {
            ApplyLightmaps();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void Update()
        {
            if (m_Renderers != null && m_Renderers.Length > 0 && m_Renderers[m_Renderers.Length - 1].lightmapIndex >= LightmapSettings.lightmaps.Length)
            {
                ApplyLightmaps();
            }
        }

        private void ApplyLightmaps()
        {
            try
            {
                if (m_Renderers == null || m_LightmapOffsetScales == null || m_Lightmaps == null ||
                    m_Renderers.Length <= 0 ||
                    m_Renderers.Length != m_LightmapOffsetScales.Length ||
                    m_Renderers.Length != m_Lightmaps.Length ||
                    m_LightmapOffsetScales.Length != m_Lightmaps.Length)
                {
                    return;
                }

                LightmapData[] lightmaps = LightmapSettings.lightmaps;
                LightmapData[] combinedLightmaps = new LightmapData[m_Lightmaps.Length + lightmaps.Length];

                Array.Copy(lightmaps, combinedLightmaps, lightmaps.Length);
                for (int i = 0; i < m_Lightmaps.Length; i++)
                {
                    combinedLightmaps[lightmaps.Length + i] = new LightmapData
                    {
                        lightmapColor = m_Lightmaps[i]
                    };
                }

                ApplyRendererInfo(m_Renderers, m_LightmapOffsetScales, lightmaps.Length);
                LightmapSettings.lightmaps = combinedLightmaps;
            }
            catch { }
        }


        private static void ApplyRendererInfo(Renderer[] renderers, Vector4[] lightmapOffsetScales, int lightmapIndexOffset)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer renderer = renderers[i];
                renderer.lightmapIndex = i + lightmapIndexOffset;
                renderer.lightmapScaleOffset = lightmapOffsetScales[i];
            }
        }
    }
}