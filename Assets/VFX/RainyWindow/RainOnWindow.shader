Shader "MyShader/RainOnWindow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size ("Size", float) = 1
        _T ( "Time", Range(0,10)) = 1
        _Distor ( "Dis", Range(-10,10)) = 1
        _Blur ( "Blur", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent"}
        LOD 100

        GrabPass {"_GrabTexture"}
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #define S(a,b,x) smoothstep(a, b, x);

            #include "UnityCG.cginc"

            sampler2D _MainTex, _GrabTexture;
            float4 _MainTex_ST;
            float _Size, _T;
            float _Distor,_Blur;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 grabUv : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.grabUv = UNITY_PROJ_COORD(ComputeGrabScreenPos(o.vertex));
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float rand( float2 p){
                p = frac( p * float2( 3.2,3.15));
                p += dot( p, p+2.25);

                return frac(p.x * p.y);
            }

            float3 rainLayer(float2 UV, float t){
                float2 aspect = float2(2, 1);
                float2 uv = UV * _Size * aspect;
                uv.y += t * 0.25;
                float2 gv = frac( uv) - 0.5;
                float2 id = floor(uv);

                float n = rand(id);
                t += n*6.2831;

                float w = UV.y * 10;
                float x = ( n - 0.5) * 0.8;
                x += ( 0.4 - abs(x) ) * sin( 3*w) * pow(sin(w), 6) * .5;

                float y = -sin( t + sin( t + sin(t) *0.5)) * 0.45;
                y -= (gv.x-x) * (gv.x-x);

                float2 dropPos = ( gv - float2( x, y)) / aspect;
                float drop = S( 0.05, .03, length( dropPos));

                float2 trailPos = ( gv - float2( x,  t * 0.25)) / aspect;
                trailPos.y = ( frac( trailPos.y * 8 )-.5) / 8;
                float trail = S( .03, .01, length( trailPos));
                float fogTrail = S( -.05, 0.05, dropPos.y);
                fogTrail *= S(-0.05, .05, dropPos.y);
                fogTrail *= S( .5, y, gv.y);
                trail *= fogTrail;
                fogTrail *= S(.05, .04, abs(dropPos.x));

                float2 offs = drop * dropPos + trail * trailPos;

                return float3(offs, fogTrail);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float t= fmod( _Time.y * 1.0 + _T, 7200);

                fixed4 col = .0;

                //drop
                float3 drops = rainLayer(i.uv,t);
                drops += rainLayer(i.uv * 1.1 + 7.54, t);
                drops += rainLayer(i.uv * 1.3 + 665.54, t);
                drops += rainLayer(i.uv * 0.6 - 56.54, t);

                //by distance fix Distor
                float fade = 1 - saturate( fwidth(i.uv) * 50);

                float blur = _Blur * (1 - drops.z * fade);

                //get camer project uv
                float2 projUv = i.grabUv.xy / i.grabUv.w;
                projUv += drops.xy * _Distor * fade;

                //Blur Component
                const float numSamples = 512;
                float bDis = rand(i.uv=9.43) *6.28;

                for( float i = 0; i < numSamples; i++){
                    float2 offs = float2( sin(bDis), cos(bDis)) * blur;
                    float d = frac( sin((i+1) * 546.59) *6536);
                    offs *= sqrt(d);

                    col += tex2D(_GrabTexture, projUv + offs);

                    bDis++;
                }

                col /= numSamples;

                return col;
            }
            ENDCG
        }
    }
}
