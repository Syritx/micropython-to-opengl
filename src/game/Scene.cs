using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;

using System;
using microcontroller_opengl_test.src.game.renderables;
using microcontroller_opengl_test.src.game.shaders;

namespace microcontroller_opengl_test.src.game {

    class Scene : GameWindow {

        static float offset;
        static bool isClosed = false;

        Triangle triangle;

        public Scene(GameWindowSettings GWS, NativeWindowSettings NWS) : base(GWS, NWS) {
            Console.WriteLine("opengl");
            Run();
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            triangle.Render(offset);
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);
        }
        protected override void OnClosed()
        {
            base.OnClosed();
            isClosed = true;
            Console.WriteLine("closed");
        }

        protected override void OnLoad() {
            base.OnLoad();
            triangle = new Triangle();

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ProgramPointSize);
            GL.ClearColor(0,0,0,1.0f);
        }

        public static void InputPressed(float increase) {
            
            if (Shader.renderingMethod == PrimitiveType.Triangles) Shader.renderingMethod = PrimitiveType.Points;
            else Shader.renderingMethod = PrimitiveType.Triangles;
        }


        // other functions
        public static bool IsWindowClosed() {
            return isClosed;
        }
    }
}