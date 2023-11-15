half3 mod(const half3 x, const half3 y)
{
    return x - y * floor(x / y);
}

float3 loop_float(const half3 from, const half3 dir, out half3 v)
{
    float s = 0.1, fade = 1.;
    v = half3(0.0, 0., 0.);
    for (int r = 0; r < _Volsteps; r++)
    {
        half3 p = from + s * dir * .5;
        p = abs(half3(_Tile, _Tile, _Tile) - fmod(p, half3(_Tile * 2., _Tile * 2., _Tile * 2.)));
        float pa, a = pa = 0.;
        for (int i = 0; i < _Iterations; i++)
        {
            p = abs(p) / dot(p, p) - _FormUParam;
            a += abs(length(p) - pa);
            pa = length(p);
        }
        float dm = max(0., _DarkMatter - a * a * .001);
        a *= a * a;
        if (r > 6) fade *= 1. - dm;
        v += fade;
        v += half3(s, s * s, s * s * s * s) * a * _Brightness * fade;
        fade *= _DistFading;
        s += _StepSize;
    }
    return v;
}
