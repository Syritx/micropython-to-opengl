using microcontroller_opengl_test.src.game.shaders;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace microcontroller_opengl_test.src.game.renderables {
    

    class Triangle {

        Shader shader;
        int vao, vbo;

        float[] vertices = {
            0.5f, -0.5f, 0.0f,
           -0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };

        Vector3 color = new Vector3(0,1,0);

        public Triangle() {
            shader = new Shader("src/game/shaders/glsl/vertexShader.glsl", "src/game/shaders/glsl/fragmentShader.glsl");

            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length*sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3*sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader.Use();
            int colorLoc = GL.GetUniformLocation(shader.Program, "color");
            GL.Uniform3(colorLoc, color);
        }

        public void Render(float offset) {
            shader.Use();

            color = new Vector3(0,1, (float)(System.Math.Sin(offset)+1)/2);
            System.Console.WriteLine(color.Z);

            GL.BindVertexArray(vao);

            int colorLocation = GL.GetUniformLocation(shader.Program, "color");
            GL.Uniform3(colorLocation, color);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.DrawArrays(Shader.renderingMethod, 0, vertices.Length);
        }
    }
}