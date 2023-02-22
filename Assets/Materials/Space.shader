Shader "Unlit/Space"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Zoom("Zoom" ,float) = 0.8
        _Brightness("Brightness" , float) = 0.0015
        _Speed("Speed" ,float) = 0.010
        _Tile("Tile" ,range(0, 3)) = 0.850
        _Saturation("Saturation" ,float) = 0.850
        _Iterations("Iterations" ,int) = 17
        _DistFading("DistFading" ,float) = 0.730
        _FormUParam("FormUParam" ,float) = 0.53
        _StepSize("StepSize" ,range(0.1, 0.9)) = 0.1
        _DarkMatter("DarkMatter" ,range(-1, 2)) = 0.300
    }
    
        HLSLINCLUDE
               #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch glcore gles3
        ENDHLSL

    SubShader
    {
        
        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"

        }
        //  LOD 100
        Pass
        {
            Cull Off Lighting Off ZWrite Off
            HLSLPROGRAM
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #pragma target 2.0

            #pragma vertex vert
            #pragma fragment frag

            #pragma editor_sync_compilation

            #define volsteps 17 //dotTouch
            #define iResolution _ScreenParams
            #define unity_WorldToObject 
            #define iTime _Time.y
            half _Zoom;
            half _Speed;
            half _Brightness;
            half _Tile;
            half _Saturation;
            half _DistFading;
            half _FormUParam;
            half _StepSize;
            half _Rotate;
            half _DarkMatter;
            int _Iterations;
            half4 _Color;

            struct appdata
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;
            };

            struct v2_f
            {
                half2 uv : TEXCOORD0;
                half4 vertex : SV_POSITION;
            };

            v2_f vert(appdata v)
            {
                v2_f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            half3 mod(const half3 x, const half3 y)
            {
                return x - y * floor(x / y);
            }

            half4 frag(const v2_f IN) : SV_Target
            {
                half2 uv = IN.uv;;
                uv.y *= iResolution.y / iResolution.x;
                half3 dir = half3(uv * _Zoom, 1.);
                half time = iTime * _Speed + .25;
                time = fmod(time, 1.);
                half3 from = half3(1., .5, 0.5);
                from += half3(time * 2., time, -2.);

                half s = 0.01, fade = 1.;
                half3 clr = 0.;
                for (int r = 0; r < volsteps; r++)
                {
                    half3 p = from + s * dir * 0.5;
                    p = abs(_Tile - mod(p, _Tile * 2.));
                    half pa, a = pa = 0.;
                    for (int i = 0; i < _Iterations; i++)
                    {
                        p = abs(p);
                        p = p * (1.0 / dot(p, p)) + (-_FormUParam);
                        //p = abs(p) / dot(p, p) - _FormUParam;
                        a += abs(length(p) - pa);
                        pa = length(p);
                    }
                    half dm = max(0., _DarkMatter - a * a * .001);
                    a *= a * a;
                    if (r > 6) fade *= 1. - dm;

                    clr += fade;
                    clr += (half3(s, s * s, s * s * s) * a * _Brightness + 1.) * fade;
                    fade *= _DistFading;
                    s += _StepSize;
                }
                clr = min(clr * .01 + _Color, 1.05);
                /*half intensity = min(clr.r + clr.g + clr.b, 0.7);
                int2 sgn = (int2(clr.xy) & 1) * 2 - 1;
                half2 gradient = float2(ddx(intensity) * sgn.x, ddy(intensity) * sgn.y);
                half cutoff = max(max(gradient.x, gradient.y) - 0.1, 0.0);
                clr.rgb *= max(1.0 - cutoff * 6.0, 0.3);*/
                return half4(clr, _Color.a);;
            }
            ENDHLSL

        }
    }
    Fallback "Diffuse"
}