#version 330 core

uniform sampler2D ourTexture;
uniform vec2 player;

void main()
{
    vec2 fragXY = vec2(gl_FragCoord.x, gl_FragCoord.y);
    vec2 screenXY = vec2(1368, 768);
    vec2 xy = fragXY / screenXY;
    vec4 texColor = texture(ourTexture, xy);

    gl_FragColor = texColor;
}