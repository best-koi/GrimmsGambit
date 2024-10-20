
//Code based on Guidev's YouTube Tutorial "How to Create a Toon Shader"
//Modifications and Adjustments by Jessie Archer

//Modifications to Tutorial Code:
//-Properties were renamed to be more intuitive
//-Additionally, the backing size property has been changed from a float to a
//range variable to ensure the intended appearance of the outline.
//-The shades range has been modified for the intended color gradient.
//-CosineAngle variable was modified for intended toon effect (originally
//had upper max bound as 0.0)
Shader "Unlit/ToonShader"
{
//Properties that can be altered in Unity
    Properties
    {
        //The color of the object
        _ObjectColor("ObjectColor", Color) = (1, 1, 1, 1)
       //The number of shades the object will have
       //More shades result in a smoother transition between colors
       //Less shades result in a more "cartoonish" appearance
        _Shades("Shades", Range(3, 20)) = 5

        //The backing color of the object
        _BackingColor("BackingColor", Color) = (0, 0, 0, 0)

        //The size of the backing outline of the object
        _BackingSize("BackingSize", Range(0, 0.05)) = 0


    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        //First Pass covers outlining the toon shader in black
        Pass
        {
            //Backing is rendered behind the object
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _BackingColor;
            float _BackingSize;





            v2f vert (appdata v)
            {
                v2f o;
                //Translate the vertex along the normal vector
                //Backing Size is positioned on object
                o.vertex = UnityObjectToClipPos(v.vertex + _BackingSize * v.normal);
                return o;
            }

            //Backing Color is rendered on object
            fixed4 frag (v2f i) : SV_Target
            {
                return _BackingColor;
            }
            ENDCG
}

        //Second Pass covers shading the objects 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float4 _ObjectColor;
            float _Shades;



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //calculate the cosine of the angle between the normal vector and the light direction
                float cosineAngle = dot(normalize(i.worldNormal),normalize(_WorldSpaceLightPos0.xyz));

                cosineAngle = max(cosineAngle, 0.2);
                
                cosineAngle = floor(cosineAngle * _Shades) / _Shades;
                //Returns the shaded texture. 
                return _ObjectColor * cosineAngle;
            }
            ENDCG
        }
    }
//Allows for shadows to appear
Fallback "VertexLit"
}
