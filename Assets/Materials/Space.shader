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
       _DarkMatter("DarkMatter" ,range(-1, 1)) = 0.300
       _OffsetX("OffsetX" ,float) = 0.300
       _OffsetY("OffsetY" ,float) = 0.300
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
           
            #define volsteps 20 //dotTouch
            
            #define iResolution _ScreenParams
            #define iTime _Time.y
            
            float _Zoom;
            float _Speed;
            float _Brightness;
            float _Tile;
            float _Saturation;
            float _DistFading;
            float _FormUParam;
            float _StepSize;
            float _DarkMatter;
            float _OffsetX;
            float _OffsetY;
            int _Iterations;
            float4 _Color;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 mod(float3 x, float3 y)
            {
                return x-y*floor(x/y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;///iResolution.xy-.5;
                uv.y*=iResolution.y/iResolution.x;
                float3 dir=float3(uv * _Zoom,1.);
                float a1=.5+_OffsetX/iResolution.x*2.;
	            float a2=.8+_OffsetY/iResolution.y*2.;
	            float2x2 rot1=fixed2x2(cos(a1),sin(a1),-sin(a1),cos(a1));
	            float2x2 rot2=fixed2x2(cos(a2),sin(a2),-sin(a2),cos(a2));
                dir.xz = mul(rot1, dir.xz) ;
                dir.xy = mul(rot2, dir.xy) ;

                
                float time=iTime*_Speed+.25;
                time = fmod(time, 10.);


                float3 from=float3(1.,.5,0.5);
                from+=float3(time*2.,time,-2.);

                float s=0.1,fade=1.;
                float3 v=0.;
                for (int r=0; r<volsteps; r++) {
                    float3 p=from+s*dir*.5;
                    p = abs(_Tile-mod(p, _Tile*2.)); 
                    float pa,a=pa=0.;
                    for (int i=0; i<_Iterations; i++) { 
                        p=abs(p)/dot(p,p)-_FormUParam; 
                        a+=abs(length(p)-pa); 
                        pa=length(p);
                    }
                    float dm=max(0.,_DarkMatter-a*a*.001); 
                    a*=a*a; 
                    if (r>6) fade*=1.-dm; 
      
                    v+=fade;
                    v+=float3(s,s*s,s*s*s*s)*a*_Brightness*fade; 
                    fade*=_DistFading; 
                    s+=_StepSize;
                }
                v=lerp(length(v),v,_Saturation); 
                float4 fragColor = float4(v*.01,1.);	

                return fragColor * _Color;
            }
            ENDCG
        }
    }
}
