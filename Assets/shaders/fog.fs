#version 330

uniform sampler2D background;

void main() 
{
    vec2 fragXY = vec2(gl_FragCoord.x, gl_FragCoord.y);
    vec4 texel = texture(background, fragXY);
    vec4 outColor = texel * vec4(0.5, 0.5, 0.5, .35);
    gl_FragColor = outColor;
}