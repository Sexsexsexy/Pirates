using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pirates
{
    public static class Input
    {
        static KeyboardState currState;
        static KeyboardState prevState;

        static MouseState curmouseState;
        static MouseState prevmouseState;

        public static KeyAssignment gameKeys = new KeyAssignment();

        public static bool IsKeyDown(Keys key)
        {
            return currState.IsKeyDown(key);
        }
        public static bool WasKeyPressed(Keys key)
        {
            return currState.IsKeyUp(key) && prevState.IsKeyDown(key);
        }

        static List<GamePadState> currGStates = new List<GamePadState>();
        static List<GamePadState> prevGStates = new List<GamePadState>();

        public static void Initialize()
        {
            for (int i = 0; i < 4; i++)
            {
                currGStates.Add(new GamePadState());
                prevGStates.Add(new GamePadState());
            }
        }

        public static  string PressedKey()
        {
            string key = "";

            if (!AnyKeyPressed())
                return "";
            else
            {
                for (int i = 65; i <= 90; i++)
                    if (WasKeyPressed((Keys)i))
                        key += ((Keys)i).ToString();

                for (int i = 48; i < 65; i++)
                    if (WasKeyPressed((Keys)i))
                        key += ((Keys)i).ToString().Substring(1,1);
            }
            return key;
        }

        public static bool Back()
        {
            return WasKeyPressed(Keys.Back);
        }

        public static bool IsButtonDown(Buttons button)
        {
            if (!Engine.Active)
                return false;

            return currGStates[0].IsButtonDown(button);
        }

        public static bool IsButtonDown(Buttons button, PlayerIndexExt playerindex)
        {
            if (!Engine.Active)
                return false;

            return currGStates[(int)(playerindex )].IsButtonDown(button);
        }

        public static bool WasButtonPressed(Buttons button)
        {
            if (!Engine.Active)
                return false;

            return currGStates[0].IsButtonUp(button) && prevGStates[0].IsButtonDown(button);
        }

        public static bool WasButtonPressed(Buttons button, PlayerIndexExt playerindex)
        {
            if (!Engine.Active)
                return false;

            return currGStates[(int)(playerindex )].IsButtonUp(button) && prevGStates[(int)(playerindex)].IsButtonDown(button);
        }

        public static Vector2 ValueLS()
        {
            if (!Engine.Active)
                return Vector2.Zero;

            return new Vector2(currGStates[0].ThumbSticks.Left.X, currGStates[0].ThumbSticks.Left.Y); 
        }

        public static Vector2 ValueLS(PlayerIndex index)
        {
            if (!Engine.Active)
                return Vector2.Zero;

            return new Vector2(currGStates[(int)(index)].ThumbSticks.Left.X, currGStates[(int)(index)].ThumbSticks.Left.Y); 
        }

        public static float LeftTrigger(PlayerIndexExt index)
        {
            if (!Engine.Active)
                return 0;

            return currGStates[(int)(index)].Triggers.Left;
        }

        public static float RightTrigger(PlayerIndexExt index)
        {
            if (!Engine.Active)
                return 0;


            return currGStates[(int)(index)].Triggers.Right;
        }

        public static void Update()
        {
            prevState = currState;
            currState = Keyboard.GetState();

            //prevGState = currGState;
            //currGState = GamePad.GetState(PlayerIndex.One);

            for (int i = 0; i < currGStates.Count; i++)
            {
                prevGStates[i] = currGStates[i];
                currGStates[i] = GamePad.GetState((PlayerIndex)i);
            }

            prevmouseState = curmouseState;
            curmouseState = Mouse.GetState();
        }

        public static Vector2 MouseXY()
        {
            return new Vector2(curmouseState.X, curmouseState.Y);
        }

        public static bool MouseLeftDown()
        {
            if (!Engine.Active)
                return false;

            if (curmouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                return true;

            return false;
        }

        public static bool MouseRightDown()
        {
            if (!Engine.Active)
                return false;

            if (curmouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                return true;

            return false;
        }

        public static bool MouseLeftPressed()
        {
            if (!Engine.Active)
                return false;

            if (curmouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && prevmouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                return true;

            return false;
        }

        public static bool MouseRightPressed()
        {
            if (!Engine.Active)
                return false;

            if (curmouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released && prevmouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                return true;

            return false;
        }

        public static bool AnyKeyPressed()
        {
            if (!Engine.Active)
                return false;

            if (prevState.GetPressedKeys().Length > 0 && currState.GetPressedKeys().Length == 0)
                return true;

            if ((curmouseState.LeftButton == ButtonState.Released && prevmouseState.LeftButton == ButtonState.Pressed) 
                || (curmouseState.RightButton == ButtonState.Released && prevmouseState.RightButton == ButtonState.Pressed))
                return true;

            return false;
        }

        public static bool AnyButtonPressedDown()
        {
            if (!Engine.Active)
                return false;

            foreach (var item in Enum.GetValues(typeof(Buttons)))
                if ((Buttons)item != Buttons.B)
                    if (WasButtonPressed((Buttons)item))
                        return true;

            return false;
        }

        public static bool AnyPressedDown(PlayerIndexExt index)
        {
            if(Left(index))
                return true;
            if (Right(index))
                return true;
            if(Up(index))
                return true;
            if(Down(index))
                return true;

            return false;
        }

        public static bool AnyPressedDown(Buttons exception)
        {
            if (!Engine.Active)
                return false;

            foreach (var item in Enum.GetValues(typeof(Buttons)))
                if ((Buttons)item != exception)
                    if (WasButtonPressed((Buttons)item))
                        return true;

            return false;
        }

        public static bool Left(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Left, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Left, index));
        }

        public static bool Right(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Right, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Right, index));
        }

        public static bool Up(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Up, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Up, index));
        }

        public static bool Down(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Down, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Down, index));
        }

        public static bool One(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Button1, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Button1, index));
        }

        public static bool Two(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Button2, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Button2, index));
        }

        public static bool Three(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Button3, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Button3, index));
        }

        public static bool Four(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.IsButtonDown(gameKeys.GetButton(KeyTypes.Button4, index), index);
            else
                return Input.IsKeyDown(gameKeys.GetKey(KeyTypes.Button4, index));
        }
        
        public static bool Five(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.WasButtonPressed(gameKeys.GetButton(KeyTypes.Button5, index), index);
            else
                return Input.WasKeyPressed(gameKeys.GetKey(KeyTypes.Button5, index));
        }

        public static bool Six(PlayerIndexExt index)
        {
            if ((int)index < 4)
                return Input.WasButtonPressed(gameKeys.GetButton(KeyTypes.Button6, index), index);
            else
                return Input.WasKeyPressed(gameKeys.GetKey(KeyTypes.Button6, index));
        }
    }

    public enum PlayerIndexExt { XboxOne, XBoxTwo, XboxThree, XboxFour, KeyboardOne, KeyboardTwo, KeyboardThree, KeyboardFour, None, AI }

    public struct KeyAssignment
    {
        const Keys KeyOne = Keys.D1;
        const Keys KeyTwo = Keys.D2;
        const Keys KeyThree = Keys.D3;
        const Keys KeyFour = Keys.D4;
        const Keys KeyFive = Keys.D5;
        const Keys KeySix = Keys.D6;
        //const Keys KeyFive = Keys.D7;

        const Keys LeftKeyOne = Keys.A;
        const Keys RightKeyOne = Keys.D;
        const Keys UpKeyOne = Keys.W;
        const Keys DownKeyOne = Keys.S;

        const Buttons OneButton = Buttons.A;
        const Buttons TwoButton = Buttons.B;
        const Buttons ThreeButton = Buttons.X;
        const Buttons FourButton = Buttons.Y;
        const Buttons FiveButton = Buttons.RightShoulder;
        const Buttons SixButton = Buttons.LeftShoulder;
        //const Buttons SevenButton = Buttons.LeftShoulder;

        const Buttons LeftButton = Buttons.LeftTrigger;
        const Buttons RightButton = Buttons.RightTrigger;
        const Buttons UpButton = Buttons.LeftThumbstickUp;
        const Buttons DownButton = Buttons.LeftThumbstickDown;

        public Keys GetKey(KeyTypes type, PlayerIndexExt index)
        {
                switch (type)
                {
                    case KeyTypes.Up:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return UpKeyOne;
                            }
                            break;
                        }
                    case KeyTypes.Down:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return DownKeyOne;
                            }
                            break;
                        }
                    case KeyTypes.Left:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return LeftKeyOne;
                            }
                            break;
                        }
                    case KeyTypes.Right:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return RightKeyOne;
                            }
                            break;
                        }
                    case KeyTypes.Button1:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return KeyOne;
                            }
                            break;
                        }
                    case KeyTypes.Button2:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return KeyTwo;
                            }
                            break;
                        }
                    case KeyTypes.Button3:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return KeyThree;
                            }
                            break;
                        }
                    case KeyTypes.Button4:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return KeyFour;
                            }
                            break;
                        }
                    case KeyTypes.Button5:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return KeyFive;
                            }
                            break;
                        }
                    case KeyTypes.Button6:
                        {
                            switch (index)
                            {
                                case PlayerIndexExt.KeyboardOne: return KeySix;
                            }
                            break;
                        }
                }

                return Keys.A;
        }

        public Buttons GetButton(KeyTypes type, PlayerIndexExt index)
        {
            switch (type)
            {
                case KeyTypes.Up: return UpButton;
                case KeyTypes.Down: return DownButton;
                case KeyTypes.Left: return LeftButton;
                case KeyTypes.Right: return RightButton;
                case KeyTypes.Button1: return OneButton;
                case KeyTypes.Button2: return TwoButton;
                case KeyTypes.Button3: return ThreeButton;
                case KeyTypes.Button4: return FourButton;
                case KeyTypes.Button5: return FiveButton;
                case KeyTypes.Button6: return SixButton;
            }

            return Buttons.A;
        }
    }

    public enum KeyTypes { Up, Down, Left, Right, Button1, Button2, Button3, Button4, Button5, Button6 }
}
