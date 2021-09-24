#version 330 core

uniform sampler2D ourTexture;
uniform int playerX;
uniform int playerY;

void main()
{
    vec2 fragXY = vec2(gl_FragCoord.x, gl_FragCoord.y);
    vec2 screenXY = vec2(1368, 768);
    vec2 xy = fragXY / screenXY;
    vec4 texColor = texture(ourTexture, xy);
    vec2 playerXY = vec2(playerX, playerY);
    
    //float dist = distance(fragXY, playerXY) / 10;    
    //if (dist < 100) {
       // texColor.a = dist;
    //}
    
    texColor.a = distance(fragXY, playerXY) / 200;
    gl_FragColor = texColor;
}