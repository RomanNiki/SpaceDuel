half3 mod(const half3 x, const half3 y)
{
    return x - y * floor(x / y);
}

half3 loop_half(const half3 from, const half3 dir, out half3 v)
{
    half s = 0.1, fade = 1.;
    v = half3(0.0, 0., 0.);
    half3 tile32 = half3(_Tile * 2., _Tile * 2., _Tile * 2.);
    half3 tile3 = half3(_Tile, _Tile, _Tile);
    for (int r = 0; r < _Volsteps; r++)
    {
        half3 p = from + s * dir * .5;
        p = abs(tile3 - fmod(p, tile32));
        half pa, a = pa = 0.;
        for (int i = 0; i < _Iterations; i++)
        {
            half3 p_abs = abs(p);
            half3 p_dot = dot(p, p);
            half3 p_div = p_abs / p_dot;
            half3 p_length = length(p);
            half3 p_length_diff = abs(p_length - pa);
    
            a += p_length_diff;
            pa = p_length;
    
            p = half3(p_div.x, p_div.y, p_div.z) - _FormUParam;
        }
        const half a_square =  a * a;
        half dm = max(0., _DarkMatter - a_square * .001);
        a *= a_square;
        if (r > 6) fade *= 1. - dm;
        v += fade;
        half s_square = s * s;
        v += half3(s, s_square, s_square * s_square) * a * _Brightness * fade;
        fade *= _DistFading;
        s += _StepSize;
    }
    return v;
}
