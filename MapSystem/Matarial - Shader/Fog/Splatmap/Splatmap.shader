Shader"Custom/SplatmapPainter"
{
    Properties
    {
        _MainTex ("Splatmap", 2D) = "black" {}
        _BrushColor ("Brush Color", Color) = (1, 0, 0, 1)
        _BrushSize ("Brush Size", Float) = 0.1
        _BrushStrength ("Brush Strength", Float) = 0.5
        _BrushFalloff ("Brush Falloff", Float) = 2.0
        _Coordinates ("Coordinates", Vector) = (0.5, 0.5, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex; 
            float4 _BrushColor; 
            float4 _Coordinates; 
            float _BrushSize;  
            float _BrushStrength; 
            float _BrushFalloff; 

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }


            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float dist = distance(i.uv, _Coordinates.xy);
                float draw = pow(saturate(1.0 - dist / _BrushSize), _BrushFalloff);
                fixed4 drawColor = _BrushColor * (draw * _BrushStrength);
    
                return max(col , saturate(drawColor));
            }
            ENDCG
        }
    }
}
