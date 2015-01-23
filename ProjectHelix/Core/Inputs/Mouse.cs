using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectHelix.Core.Commands;
using ProjectHelix.Render;

namespace ProjectHelix.Core.Inputs
{
    class MyMouse
    {
        public static readonly MyMouse Instance = new MyMouse();

        public static Vector2 ConvertToWorldCoord(Vector2 coord)
        {
            var v = RenderManager.Instance.GetViewport();
            var x = 1920*coord.X/v.X;
            var y = 1080*coord.Y/v.Y;
            return new Vector2(x,y);
        }

        public MyMouse()
        {
            PressPos = new Vector2();
            CurrentPos = new Vector2();
            PrevPos = new Vector2();
            LeftDown = false;
            PrevLeftDown = false;
        }
        public bool PrevLeftDown { get; private set; }
        public bool LeftDown { get; private set; }

        public Vector2 CurrentPos { get; private set; }
        public Vector2 PrevPos { get; private set; }
        public Vector2 PressPos { get; private set; }

        public void Update(GameTime gameTime)
        {
            //true if pressed, false if not pressed
            var leftDown = Mouse.GetState().LeftButton == ButtonState.Pressed;
            var currentPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            //Left button
            if (leftDown && !PrevLeftDown)
            {
                PrevLeftDown = true;
                LeftDown = true;
                MainCore.Instance.CommandQueue.Enqueue(new MousePressedCmd(CurrentPos));
                return;
            }
            if (!leftDown && PrevLeftDown)
            {
                PrevLeftDown = false;
                LeftDown = false;
                MainCore.Instance.CommandQueue.Enqueue(new MouseReleasedCmd(CurrentPos));
                return;
            }

            //No moves
            if (currentPos == PrevPos) return;
            PrevPos = CurrentPos;
            CurrentPos = currentPos;
            //Move
            if (!LeftDown)
            {
                MainCore.Instance.CommandQueue.Enqueue(new MouseMoveCmd(CurrentPos - PrevPos));
            }
            else //Drag
            {
                MainCore.Instance.CommandQueue.Enqueue(new MouseDragCmd(CurrentPos - PrevPos));
            }
        }
    }
}
