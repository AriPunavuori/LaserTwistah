 Shader "Sprites/Diffuse Flash"
 {
     Properties
     {
         [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
         _SelfIllum ("Self Illumination",Range(0.0,1.0)) = 0.0
         _FlashAmount ("Flash Amount",Range(0.0,1.0)) = 0.0
         _Color ("Tint", Color) = (1,1,1,1)
		 _FlashColor ("Flash Color", Color) = (1,1,1,1)

         [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
     }
 
     SubShader
     {
         Tags
         { 
             "Queue"="Transparent" 
             "IgnoreProjector"="True" 
             "RenderType"="Transparent" 
             "PreviewType"="Plane"
             "CanUseSpriteAtlas"="True"
         }
 
         Cull Off
         Lighting Off
         ZWrite Off
         Fog { Mode Off }
         Blend SrcAlpha OneMinusSrcAlpha
 
         CGPROGRAM
         #pragma surface surf Lambert alpha vertex:vert
         #pragma multi_compile DUMMY PIXELSNAP_ON
 
         sampler2D _MainTex;
         fixed4 _Color, _FlashColor;
         float _FlashAmount,_SelfIllum;
         
         struct Input
         {
             float2 uv_MainTex;
             fixed4 color;
			 //fixed4 flashColor;
         };
         
         void vert (inout appdata_full v, out Input o)
         {
             #if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
             v.vertex = UnityPixelSnap (v.vertex);
             #endif
             v.normal = float3(0,0,-1);
             
             UNITY_INITIALIZE_OUTPUT(Input, o);
             o.color = _Color;
			 //o.flashColor = _FlashColor;
         }
 
         void surf (Input IN, inout SurfaceOutput o)
         {
             fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
             //o.Albedo = 
             //o.Emission = lerp(c.rgb,float3(1.0,1.0,1.0),_FlashAmount) * _SelfIllum;
			 o.Emission = lerp(c.rgb, _FlashColor.rgb, _FlashAmount);
			 o.Alpha = c.a;
         }
         ENDCG
     }
 
 Fallback "Transparent/VertexLit"
 }