Shader "Custom/PhongSpecular"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.2, 0.2, 0.2, 1)
        _DiffuseColor ("Diffuse Color", Color) = (0.7, 0.7, 0.7, 1)
        _SpecColor ("Specular Color", Color) = (1, 1, 1, 1)

        _Kd ("Diffuse Strength", Range(0,1)) = 0.3
        _Ks ("Specular Strength", Range(0,2)) = 1.0
        _Shininess ("Shininess", Range(1,128)) = 32

        _LightPosition ("Light Position", Vector) = (0, 2, 2, 1)
        _LightColorCustom ("Light Color", Color) = (1, 1, 1, 1)
        _CameraPositionCustom ("Camera Position", Vector) = (0, 0, 0, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _BaseColor;
            fixed4 _DiffuseColor;
            fixed4 _SpecColor;

            float _Kd;
            float _Ks;
            float _Shininess;

            float4 _LightPosition;
            fixed4 _LightColorCustom;
            float4 _CameraPositionCustom;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 N = normalize(i.worldNormal);
                float3 L = normalize(_LightPosition.xyz - i.worldPos);
                float3 V = normalize(_CameraPositionCustom.xyz - i.worldPos);
                float3 R = normalize(reflect(-L, N));

                float NdotL = max(0.0, dot(N, L));
                float RdotV = max(0.0, dot(R, V));

                float diffuse = _Kd * NdotL;
                float specular = _Ks * pow(RdotV, _Shininess);

                float3 color =
                    _BaseColor.rgb +
                    _DiffuseColor.rgb * diffuse * _LightColorCustom.rgb +
                    _SpecColor.rgb * specular * _LightColorCustom.rgb;

                return fixed4(saturate(color), 1.0);
            }
            ENDCG
        }
    }
}