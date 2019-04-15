Shader "Custom/S_grass"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Cutoff ("Cutoff", float) = 0.5
        _Move ("Move", Range(0,0.5)) = 0.1
		_Timeing ("Timeing", Range(0,5)) = 1
    }
    SubShader
    {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert


        sampler2D _MainTex;
		float _Move;
		float _Timeing;

        struct Input
        {
            float2 uv_MainTex;
			float3 cameraRelativeWorldPos;
			float3 worldNormal;
			INTERNAL_DATA
        };

		fixed4 _Color;

		void vert(inout appdata_full v, out Input o){
			v.vertex.y += sin(_Time.y * _Timeing)* _Move * v.color.r;

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.cameraRelativeWorldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)) - _WorldSpaceCameraPos.xyz;
		}
		
     
        void surf (Input IN, inout SurfaceOutputStandard o)
        {         
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;


			half3 flatWorldNormal = normalize(cross(ddy(IN.cameraRelativeWorldPos.xyz), ddx(IN.cameraRelativeWorldPos.xyz)));


			half3 worldT = WorldNormalVector(IN, half3(1, 0, 0));
			half3 worldB = WorldNormalVector(IN, half3(0, 1, 0));
			half3 worldN = WorldNormalVector(IN, half3(0, 0, 1));
			half3x3 tbn = half3x3(worldT, worldB, worldN);


			o.Normal = mul(tbn, flatWorldNormal);
        }
        ENDCG
    }
	FallBack "Diffuse"
}
