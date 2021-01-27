#version 330 core

out vec4 FragColor;
in vec3 fragC;

void main() {
    FragColor = vec4(fragC, 1.0);
}