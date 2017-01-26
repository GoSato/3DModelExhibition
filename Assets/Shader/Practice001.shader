// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33216,y:32718,varname:node_4013,prsc:2|diff-2004-OUT,emission-7266-OUT;n:type:ShaderForge.SFN_Tex2d,id:1424,x:32379,y:32431,ptovrint:False,ptlb:Diffuce,ptin:_Diffuce,varname:node_1424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2004,x:32956,y:32692,varname:node_2004,prsc:2|A-5085-OUT,B-3189-OUT;n:type:ShaderForge.SFN_Vector1,id:3189,x:32762,y:32740,varname:node_3189,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Color,id:2615,x:32762,y:32842,ptovrint:False,ptlb:Glow_Color,ptin:_Glow_Color,varname:node_2615,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1,c2:0.7,c3:1,c4:0;n:type:ShaderForge.SFN_OneMinus,id:6143,x:32317,y:32772,varname:node_6143,prsc:2|IN-1424-B;n:type:ShaderForge.SFN_Power,id:8972,x:32523,y:32791,varname:node_8972,prsc:2|VAL-6143-OUT,EXP-2065-OUT;n:type:ShaderForge.SFN_Vector1,id:2065,x:32317,y:32959,varname:node_2065,prsc:2,v1:3;n:type:ShaderForge.SFN_Tex2d,id:9682,x:32523,y:33028,ptovrint:False,ptlb:Glow_Pattern,ptin:_Glow_Pattern,varname:node_9682,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-492-UVOUT;n:type:ShaderForge.SFN_Panner,id:492,x:32317,y:33097,varname:node_492,prsc:2,spu:0.1,spv:0|UVIN-4494-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4494,x:32079,y:33097,varname:node_4494,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1771,x:32774,y:33045,varname:node_1771,prsc:2|A-8972-OUT,B-9682-R,C-9019-OUT;n:type:ShaderForge.SFN_Slider,id:9019,x:32488,y:33322,ptovrint:False,ptlb:Glow_Strength,ptin:_Glow_Strength,varname:node_9019,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:5;n:type:ShaderForge.SFN_Multiply,id:7266,x:32965,y:32969,varname:node_7266,prsc:2|A-2615-RGB,B-1771-OUT;n:type:ShaderForge.SFN_Color,id:6519,x:32379,y:32265,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_6519,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:0.5;n:type:ShaderForge.SFN_Multiply,id:5085,x:32566,y:32306,varname:node_5085,prsc:2|A-6519-RGB,B-1424-RGB;proporder:1424-9682-2615-9019-6519;pass:END;sub:END;*/

Shader "Shader Forge/Practice001" {
    Properties {
        _Diffuce ("Diffuce", 2D) = "white" {}
        _Glow_Pattern ("Glow_Pattern", 2D) = "white" {}
        _Glow_Color ("Glow_Color", Color) = (0.1,0.7,1,0)
        _Glow_Strength ("Glow_Strength", Range(0, 5)) = 5
        _Color ("Color", Color) = (0,0,0,0.5)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuce; uniform float4 _Diffuce_ST;
            uniform float4 _Glow_Color;
            uniform sampler2D _Glow_Pattern; uniform float4 _Glow_Pattern_ST;
            uniform float _Glow_Strength;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diffuce_var = tex2D(_Diffuce,TRANSFORM_TEX(i.uv0, _Diffuce));
                float3 diffuseColor = ((_Color.rgb*_Diffuce_var.rgb)*0.5);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_2809 = _Time + _TimeEditor;
                float2 node_492 = (i.uv0+node_2809.g*float2(0.1,0));
                float4 _Glow_Pattern_var = tex2D(_Glow_Pattern,TRANSFORM_TEX(node_492, _Glow_Pattern));
                float3 emissive = (_Glow_Color.rgb*(pow((1.0 - _Diffuce_var.b),3.0)*_Glow_Pattern_var.r*_Glow_Strength));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuce; uniform float4 _Diffuce_ST;
            uniform float4 _Glow_Color;
            uniform sampler2D _Glow_Pattern; uniform float4 _Glow_Pattern_ST;
            uniform float _Glow_Strength;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Diffuce_var = tex2D(_Diffuce,TRANSFORM_TEX(i.uv0, _Diffuce));
                float3 diffuseColor = ((_Color.rgb*_Diffuce_var.rgb)*0.5);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
