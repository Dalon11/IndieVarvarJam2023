// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Impart_transparency"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,0.4481132,0.4481132,1)
		[HDR]_Color1("Color 1", Color) = (0,0.1950307,1,1)
		[Toggle(_COLORRCOLOR_ON)] _ColorRColor("Color/RColor", Float) = 0
		_Emissive("Emissive", Float) = 1
		_SpeedXYScaleZPowerW("Speed XY/Scale Z/ Power W", Vector) = (0,0,3.68,5.05)
		[Toggle(_KEYWORD0_ON)] _Keyword0("Keyword 0", Float) = 0
		[Toggle(_RG_ON)] _RG("R/G", Float) = 0

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha One
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
				#pragma shader_feature_local _KEYWORD0_ON
				#pragma shader_feature_local _RG_ON
				#pragma shader_feature_local _COLORRCOLOR_ON


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float4 ase_texcoord1 : TEXCOORD1;
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
				uniform float4 _SpeedXYScaleZPowerW;
				uniform sampler2D _TextureSample0;
				uniform float _Emissive;
				uniform float4 _Color0;
				uniform float4 _Color1;
						float2 voronoihash27( float2 p )
						{
							
							p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
							return frac( sin( p ) *43758.5453);
						}
				
						float voronoi27( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
						{
							float2 n = floor( v );
							float2 f = frac( v );
							float F1 = 8.0;
							float F2 = 8.0; float2 mg = 0;
							for ( int j = -1; j <= 1; j++ )
							{
								for ( int i = -1; i <= 1; i++ )
							 	{
							 		float2 g = float2( i, j );
							 		float2 o = voronoihash27( n + g );
									o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
									float d = 0.5 * dot( r, r );
							 		if( d<F1 ) {
							 			F2 = F1;
							 			F1 = d; mg = g; mr = r; id = o;
							 		} else if( d<F2 ) {
							 			F2 = d;
							
							 		}
							 	}
							}
							return F1;
						}
				


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3 = v.ase_texcoord1;

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

					float time27 = 3.37;
					float2 voronoiSmoothId27 = 0;
					float2 appendResult44 = (float2(_SpeedXYScaleZPowerW.x , _SpeedXYScaleZPowerW.y));
					float2 panner24 = ( 1.0 * _Time.y * appendResult44 + float2( 0,0 ));
					float2 texCoord22 = i.texcoord.xy * float2( 1,1 ) + panner24;
					float2 coords27 = texCoord22 * _SpeedXYScaleZPowerW.z;
					float2 id27 = 0;
					float2 uv27 = 0;
					float voroi27 = voronoi27( coords27, time27, id27, uv27, 0, voronoiSmoothId27 );
					float4 texCoord42 = i.ase_texcoord3;
					texCoord42.xy = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float clampResult37 = clamp( ( pow( ( voroi27 * 3.25 ) , _SpeedXYScaleZPowerW.w ) + texCoord42.z ) , 0.0 , 1.0 );
					#ifdef _KEYWORD0_ON
					float staticSwitch47 = clampResult37;
					#else
					float staticSwitch47 = 1.0;
					#endif
					float2 texCoord1 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float4 tex2DNode2 = tex2D( _TextureSample0, texCoord1 );
					#ifdef _RG_ON
					float staticSwitch48 = tex2DNode2.g;
					#else
					float staticSwitch48 = tex2DNode2.r;
					#endif
					float smoothstepResult21 = smoothstep( 0.0 , texCoord42.w , staticSwitch48);
					float4 lerpResult12 = lerp( _Color0 , _Color1 , texCoord1.y);
					#ifdef _COLORRCOLOR_ON
					float4 staticSwitch18 = lerpResult12;
					#else
					float4 staticSwitch18 = _Color0;
					#endif
					

					fixed4 col = ( ( ( staticSwitch47 * smoothstepResult21 ) * _Emissive ) * i.color * staticSwitch18 );
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18934
-1433;27;1440;809;3339.59;500.7015;1;True;False
Node;AmplifyShaderEditor.Vector4Node;43;-4161.901,-756.6542;Inherit;False;Property;_SpeedXYScaleZPowerW;Speed XY/Scale Z/ Power W;5;0;Create;True;0;0;0;False;0;False;0,0,3.68,5.05;0,0,3.68,5.05;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;44;-3823.904,-734.5541;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;24;-3655.565,-788.8615;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-3466.56,-801.1721;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;27;-3237.74,-803.1373;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;3.37;False;2;FLOAT;6.09;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-3067.303,-802.0058;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;3.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-3006.404,-540.3541;Inherit;True;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;34;-2854.376,-791.7676;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;3.98;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-3118.802,-194.8844;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-2901.538,-224.7529;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;7fedf685cb801a948b88083a7adb231e;7fedf685cb801a948b88083a7adb231e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-2597.612,-780.1404;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;-0.27;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;37;-2375.211,-773.4012;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-2130.503,-503.9641;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;48;-2500.59,-230.7015;Inherit;False;Property;_RG;R/G;7;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;21;-2180.324,-267.9413;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;47;-1872.293,-639.6519;Inherit;False;Property;_Keyword0;Keyword 0;6;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;-1815.651,570.4274;Inherit;False;Property;_Color1;Color 1;2;1;[HDR];Create;True;0;0;0;False;0;False;0,0.1950307,1,1;0,0.1950307,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-1812.749,387.0496;Inherit;False;Property;_Color0;Color 0;1;1;[HDR];Create;True;0;0;0;False;0;False;1,0.4481132,0.4481132,1;1,0.4481132,0.4481132,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;12;-1443.466,474.761;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1482.877,-68.18304;Inherit;False;Property;_Emissive;Emissive;4;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-1503.249,-540.3477;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1305.884,-117.6138;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;18;-1139.957,380.3421;Inherit;False;Property;_ColorRColor;Color/RColor;3;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;3;-1456.611,91.27665;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-899.0748,22.14807;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;480,-91;Float;False;True;-1;2;ASEMaterialInspector;0;7;Impart_transparency;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;True;8;5;False;-1;1;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;44;0;43;1
WireConnection;44;1;43;2
WireConnection;24;2;44;0
WireConnection;22;1;24;0
WireConnection;27;0;22;0
WireConnection;27;2;43;3
WireConnection;32;0;27;0
WireConnection;34;0;32;0
WireConnection;34;1;43;4
WireConnection;2;1;1;0
WireConnection;35;0;34;0
WireConnection;35;1;42;3
WireConnection;37;0;35;0
WireConnection;48;1;2;1
WireConnection;48;0;2;2
WireConnection;21;0;48;0
WireConnection;21;2;42;4
WireConnection;47;1;46;0
WireConnection;47;0;37;0
WireConnection;12;0;5;0
WireConnection;12;1;11;0
WireConnection;12;2;1;2
WireConnection;38;0;47;0
WireConnection;38;1;21;0
WireConnection;19;0;38;0
WireConnection;19;1;20;0
WireConnection;18;1;5;0
WireConnection;18;0;12;0
WireConnection;4;0;19;0
WireConnection;4;1;3;0
WireConnection;4;2;18;0
WireConnection;0;0;4;0
ASEEND*/
//CHKSM=851821DD02454221192D7BBAFFE7A32B41AA4E70