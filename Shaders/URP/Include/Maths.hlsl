#ifndef DTT_URP_MATHS
#define DTT_URP_MATHS

#define RADTODEG 57.29577
#define DEGTORAD 0.01745

inline float LinearRelation(float x, float xmin, float xmax, float ymin, float ymax)
{
	float m = (ymax - ymin) / (xmax - xmin);
	float b = ymin - (m * xmin);
	return clamp((x * m) + b, xmin, xmax);
}

inline float3 LinearRelation(float x, float xmin, float xmax, float3 ymin, float3 ymax)
{
	return float3(LinearRelation(x, xmin, xmax, ymin.x, ymax.x),
		LinearRelation(x, xmin, xmax, ymin.y, ymax.y),
		LinearRelation(x, xmin, xmax, ymin.z, ymax.z));
}

#endif // DTT_URP_MATHS