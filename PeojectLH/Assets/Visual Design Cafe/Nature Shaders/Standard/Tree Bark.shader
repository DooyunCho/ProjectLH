Shader "Nature Shaders/Tree Bark"
{
    Properties
    {
        _HSL("Hue, Saturation, Lightness", Vector) = (0, 0, 0, 0)
        _HSLVariation("Hue, Saturation, Lightness Variation", Vector) = (0, 0, 0, 0)
        _Tint("Tint", Color) = (1, 1, 1, 1)
        _TintVariation("Tint Variation", Color) = (1, 1, 1, 1)
        _ColorVariationSpread("Color Variation Spread", Float) = 0.2
        
        [NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
        [NoScaleOffset]_Albedo("Albedo", 2D) = "white" {}
        [NoScaleOffset]_BumpMap("Normal Map", 2D) = "bump" {}
        [NoScaleOffset]_MaskMap("Mask Map", 2D) = "white" {}
        [NoScaleOffset]_MetallicGlossMap("Metallic Gloss Map", 2D) = "black" {}
        [NoScaleOffset]_OcclusionMap("Occlusion Map", 2D) = "white" {}
        _ObjectHeight("Grass Height", Float) = 0.5
        _ObjectRadius("Tree Radius", Float) = 0.5
        _VertexNormalStrength("Vertex Normal Strength", Range(0, 1)) = 1
        _TrunkBendFactor("Trunk Bending", Vector) = (2, 0.3, 0, 0)
        _WindVariation("Wind Variation", Range(0, 1)) = 0.3
        _WindStrength("Wind Strength", Range(0, 2)) = 1
        _TurbulenceStrength("Turbulence Strength", Range(0, 2)) = 1
        _GlossRemap("Remap Smoothness", Vector) = (0, 1, 0, 0)
        _OcclusionRemap("Remap Occlusion", Vector) = (0, 1, 0, 0)
        _BumpScale("Normal Map Strength", Range(0, 1)) = 1
        _Glossiness("Smoothness", Range(0, 1)) = 0.2
        _Metallic("Metallic", Range(0, 1)) = 0
        [KeywordEnum(Baked, UV, Vertex)]_WIND_CONTROL("Wind Control", Float) = 2
        [KeywordEnum(Ambient, Gust, Turbulence, None)]_DEBUG("Debug Wind", Float) = 3
    }
    SubShader
    {
        Tags
		{ 
			"RenderType" = "TransparentCutout"  
			"Queue" = "AlphaTest+0" 
			"DisableBatching" = "True"
			"NatureRendererInstancing" = "True"
		}
        LOD 200

        // Render State
        Blend One Zero, One Zero
        //Cull Off
        ZTest LEqual
        ZWrite On
        // ColorMask: <None>

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface NatureShaderSurface Standard fullforwardshadows vertex:NatureShaderVertex keepalpha addshadow nolightmap dithercrossfade
        
        // Pragmas
        #pragma multi_compile_instancing
        #pragma instancing_options procedural:SetupNatureRenderer
        #pragma target 4.0
        #pragma multi_compile_fog
    
        // Keywords
        #pragma shader_feature_local _WIND_CONTROL_BAKED _WIND_CONTROL_UV _WIND_CONTROL_VERTEX
        //#pragma shader_feature _DEBUG_AMBIENT _DEBUG_GUST _DEBUG_TURBULENCE _DEBUG_NONE
        #pragma shader_feature_local _COLOR_HSL _COLOR_TINT
        #pragma shader_feature_local _SURFACE_MAP_METALLIC_GLOSS _SURFACE_MAP_MASK
        #pragma shader_feature_local _METALLICGLOSSMAP_ON

        // Defines
        #define STANDARD_RENDER_PIPELINE
        #define _TYPE_TREE_BARK
        #define PROPERTY_Albedo
        #define PROPERTY_BumpMap
        #define PROPERTY_MaskMap
        #define PROPERTY_ObjectHeight
        #define PROPERTY_VertexNormalStrength
        #define PROPERTY_WindVariation
        #define PROPERTY_WindStrength
        #define PROPERTY_TurbulenceStrength
        #define PROPERTY_HSL
        #define PROPERTY_HSLVariation
        #define PROPERTY_GlossRemap
        #define PROPERTY_OcclusionMap
        #define PROPERTY_OcclusionRemap
        #define PROPERTY_BumpScale
        #define PROPERTY_Tint
        #define PROPERTY_TintVariation
        #define PROPERTY_Cutoff
        #define PROPERTY_TrunkBendFactor
        #define PROPERTY_ColorVariationSpread

        // Includes
        #include "../Common/Surface Shader.cginc"
        ENDCG
    }

    Dependency "OptimizedShader" = "Nature Shaders/Tree Bark" 
    Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
	CustomEditor "VisualDesignCafe.Nature.Editor.NatureMaterialEditor"
}
