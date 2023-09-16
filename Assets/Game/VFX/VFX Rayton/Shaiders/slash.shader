// Made with Amplify Shader Editor v1.9.1.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "slash"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,1,1,1)
		[HDR]_Color1("Color 1", Color) = (1,0,0,1)
		[Toggle]_ToggleSwitch0("Toggle Switch0", Float) = 0
		_Tile("Tile", Vector) = (1,1,0,0)
		_Speed("Speed", Vector) = (0,0,0,0)
		_Noisescale("Noise scale", Float) = 0
		_contrast("contrast", Float) = 2
		_Emissiv("Emissiv", Float) = 1
		_Powernoise("Power noise", Float) = 0

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float4 ase_texcoord2 : TEXCOORD2;
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					float4 ase_texcoord3 : TEXCOORD3;
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _Color1;
				uniform float4 _Color0;
				uniform float2 _Tile;
				uniform float _contrast;
				uniform float _ToggleSwitch0;
				uniform sampler2D _TextureSample0;
				uniform float2 _Speed;
				uniform float _Noisescale;
				uniform float _Powernoise;
				uniform float _Emissiv;
				float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
				float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
				float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
				float snoise( float2 v )
				{
					const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
					float2 i = floor( v + dot( v, C.yy ) );
					float2 x0 = v - i + dot( i, C.xx );
					float2 i1;
					i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
					float4 x12 = x0.xyxy + C.xxzz;
					x12.xy -= i1;
					i = mod2D289( i );
					float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
					float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
					m = m * m;
					m = m * m;
					float3 x = 2.0 * frac( p * C.www ) - 1.0;
					float3 h = abs( x ) - 0.5;
					float3 ox = floor( x + 0.5 );
					float3 a0 = x - ox;
					m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
					float3 g;
					g.x = a0.x * x0.x + h.x * x0.y;
					g.yz = a0.yz * x12.xz + h.yz * x12.yw;
					return 130.0 * dot( m, g );
				}
				


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3 = v.ase_texcoord2;

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 texCoord2 = i.texcoord.xy * _Tile + float2( 0,0 );
					float4 lerpResult78 = lerp( _Color1 , _Color0 , texCoord2.x);
					float4 temp_cast_0 = (1.0).xxxx;
					float4 texCoord1 = i.ase_texcoord3;
					texCoord1.xy = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float4 temp_cast_1 = (( texCoord1.z + texCoord1.w )).xxxx;
					float temp_output_8_0 = (2.5 + (( 1.0 + texCoord1.x ) - 0.0) * (1.0 - 2.5) / (1.0 - 0.0));
					float U5 = texCoord2.y;
					float temp_output_4_0 = (1.0 + (saturate( texCoord1.y ) - 0.0) * (0.0 - 1.0) / (1.0 - 0.0));
					float V6 = texCoord2.x;
					float2 appendResult17 = (float2(( saturate( ( ( ( temp_output_8_0 * temp_output_8_0 * temp_output_8_0 * temp_output_8_0 * temp_output_8_0 ) * U5 ) - temp_output_4_0 ) ) * ( 1.0 / (1.0 + (temp_output_4_0 - 0.0) * (0.0001 - 1.0) / (1.0 - 0.0)) ) ) , V6));
					float2 panner72 = ( 1.0 * _Time.y * _Speed + texCoord2);
					float simplePerlin2D74 = snoise( panner72*_Noisescale );
					simplePerlin2D74 = simplePerlin2D74*0.5 + 0.5;
					float4 smoothstepResult20 = smoothstep( float4( 0,0,0,0 ) , temp_cast_1 , ( tex2D( _TextureSample0, saturate( appendResult17 ) ) * saturate( pow( simplePerlin2D74 , _Powernoise ) ) ));
					float4 temp_output_22_0 = saturate( smoothstepResult20 );
					float4 appendResult68 = (float4((( ( ( pow( lerpResult78 , 2.0 ) * _contrast ) * (( _ToggleSwitch0 )?( temp_output_22_0 ):( temp_cast_0 )) * i.color ) * _Emissiv )).rgb , ( _Color0.a * (temp_output_22_0).r * i.color.a * _Color1.a )));
					

					fixed4 col = appendResult68;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19102
Node;AmplifyShaderEditor.SaturateNode;3;185,-42.5;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;540,-307.5;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;801,-293.5;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;995,-265.5;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;14;1262,-253.5;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;1285,-83.5;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;1477,-198.5;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;25;3169.577,534.8326;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;17;1436.7,80.5;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ToggleSwitchNode;62;3102.156,219.2204;Inherit;False;Property;_ToggleSwitch0;Toggle Switch0;3;0;Create;True;0;0;0;False;0;False;0;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;2916.68,160.4525;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;6;393,279.5;Inherit;False;V;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;41,-154.5;Inherit;False;2;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;64;3116.834,344.1763;Inherit;True;True;False;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;2263.968,454.3324;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;20;2488.878,214.2966;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;22;2797.657,317.8636;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;3427.616,19.85758;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;3697.361,380.4308;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;18;1646.547,22.65283;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;4;681,-32.5;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;11;995,-17.5;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;8;247,-326.5;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;2.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;69;-690.3943,12.40955;Inherit;False;Property;_Tile;Tile;4;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-427.2724,205.9739;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-13.2536,164.0052;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;27;2684.165,-349.4701;Inherit;False;Property;_Color0;Color 0;1;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;17.76264,17.76264,17.76264,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;77;2687.449,-533.144;Inherit;False;Property;_Color1;Color 1;2;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,1;17.76264,17.76264,17.76264,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;78;3188.068,-399.4006;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;3813.967,-229.3318;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;2387.759,-60.36256;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;19;2040.321,-30.88639;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;b67eaa8d5bbed154499faab7ac58acc2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;74;1767.572,-594.5219;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;7.21;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;73;1097.224,-741.3111;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;76;1403.391,-459.6561;Inherit;False;Property;_Noisescale;Noise scale;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;72;1317.1,-730.8681;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;85;2139.375,-474.0592;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;1943.183,-321.6597;Inherit;False;Property;_Powernoise;Power noise;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;87;2335.568,-382.9698;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;79;3491.169,-287.7693;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;36;3896.665,129.5684;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;81;3680.395,-27.58307;Inherit;False;Property;_contrast;contrast;7;0;Create;True;0;0;0;False;0;False;2;16.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;3750.787,117.7864;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;88;3571.787,229.7864;Inherit;False;Property;_Emissiv;Emissiv;8;0;Create;True;0;0;0;False;0;False;1;16.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;4496.403,290.6849;Float;False;True;-1;2;ASEMaterialInspector;0;11;slash;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;True;2;5;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;True;True;2;False;;False;True;True;True;True;False;0;False;;False;False;False;False;False;False;False;False;False;True;2;False;;True;3;False;;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.DynamicAppendNode;68;4170.322,296.1446;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;5;326,88.5;Inherit;False;U;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
WireConnection;3;0;1;2
WireConnection;9;0;8;0
WireConnection;9;1;8;0
WireConnection;9;2;8;0
WireConnection;9;3;8;0
WireConnection;9;4;8;0
WireConnection;10;0;9;0
WireConnection;10;1;5;0
WireConnection;12;0;10;0
WireConnection;12;1;4;0
WireConnection;14;0;12;0
WireConnection;15;1;11;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;17;0;16;0
WireConnection;17;1;6;0
WireConnection;62;0;29;0
WireConnection;62;1;22;0
WireConnection;6;0;2;1
WireConnection;7;1;1;1
WireConnection;64;0;22;0
WireConnection;21;0;1;3
WireConnection;21;1;1;4
WireConnection;20;0;75;0
WireConnection;20;2;21;0
WireConnection;22;0;20;0
WireConnection;35;0;80;0
WireConnection;35;1;62;0
WireConnection;35;2;25;0
WireConnection;34;0;27;4
WireConnection;34;1;64;0
WireConnection;34;2;25;4
WireConnection;34;3;77;4
WireConnection;18;0;17;0
WireConnection;4;0;3;0
WireConnection;11;0;4;0
WireConnection;8;0;7;0
WireConnection;2;0;69;0
WireConnection;78;0;77;0
WireConnection;78;1;27;0
WireConnection;78;2;2;1
WireConnection;80;0;79;0
WireConnection;80;1;81;0
WireConnection;75;0;19;0
WireConnection;75;1;87;0
WireConnection;19;1;18;0
WireConnection;74;0;72;0
WireConnection;74;1;76;0
WireConnection;72;0;2;0
WireConnection;72;2;73;0
WireConnection;85;0;74;0
WireConnection;85;1;86;0
WireConnection;87;0;85;0
WireConnection;79;0;78;0
WireConnection;36;0;89;0
WireConnection;89;0;35;0
WireConnection;89;1;88;0
WireConnection;0;0;68;0
WireConnection;68;0;36;0
WireConnection;68;3;34;0
WireConnection;5;0;2;2
ASEEND*/
//CHKSM=57AC66644432C892F5FAC932590C0300636D50A2