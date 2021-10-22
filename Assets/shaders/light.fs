#version 330 core

uniform sampler2D ourTexture;
uniform int playerX;
uniform int playerY;

void main() {
    vec2 fragXY = vec2(gl_FragCoord.x, gl_FragCoord.y);
    vec2 screenXY = vec2(1368, 768);
    vec2 xy = fragXY / screenXY;
    vec4 texel = texture(ourTexture, xy);
    vec2 playerXY = vec2(playerX, playerY);

    float dist = distance(fragXY, playerXY);
    if(dist < 100) {
        texel.a = dist / 100;
    } else {
        texel.a = 1;
        //gl_FragColor = texel * vec4(0.75, 0.75, 0.75, 0.15);
    }
    gl_FragColor = texel;

}