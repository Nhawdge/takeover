#version 130 core

uniform sampler2D ourTexture;
uniform int playerX;
uniform int playerY;

void main() {
    vec2 fragXY = vec2(gl_FragCoord.x, gl_FragCoord.y);
    vec2 screenXY = vec2(1368, 768);
    vec2 xy = fragXY / screenXY;
    vec4 texel = texture(ourTexture, xy);
    vec2 playerXY = vec2(playerX, playerY);

    float dist = distance(fragXY, playerXY) / 100;
    if(dist <= 1) {
        texel.a = dist;
    } else {
        discard;
        texel + vec4(0.1, 0.1, 0.1, 1);
    }
    gl_FragColor = texel;

}