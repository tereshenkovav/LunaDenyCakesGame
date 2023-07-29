using System;
using System.Collections.Generic;
using SFML.Window;

namespace SfmlNetEngine
{
    public class KeyInfo
    {
        public string ActionName;
        public Mouse.Button mousebutton;
        public Keyboard.Key keyboardkey;
        public bool ismouse;
        // Здесь можно заменить на нулевой объект, который всегда isMatch=false и view=???
        public bool isset;
        public KeyInfo Clone()
        {
            return (KeyInfo)this.MemberwiseClone();            
        }
        public bool isMatch(EventArgs eventarg)
        {
            if (!isset) return false;
            if (ismouse)
            {
                if (eventarg is MouseButtonEventArgs mouseButtonEventArgs)
                    return mouseButtonEventArgs.Button == mousebutton;
                else
                    return false;
            }
            else
            {
                if (eventarg is KeyEventArgs keyEventArg)
                    return keyEventArg.Code == keyboardkey;
                else
                    return false;
            }
        }
        public String getView()
        {
            if (!isset) return "???";
            if (ismouse)
            {
                if (mousebutton == Mouse.Button.Left) return "LCM";
                else
                if (mousebutton == Mouse.Button.Middle) return "MCM";
                else
                if (mousebutton == Mouse.Button.Right) return "RCM";
                else
                    return "???";
            }
            else
            {
                if (keyboardkey == Keyboard.Key.A) return "A";
                if (keyboardkey == Keyboard.Key.B) return "B";
                if (keyboardkey == Keyboard.Key.C) return "C";
                if (keyboardkey == Keyboard.Key.D) return "D";
                if (keyboardkey == Keyboard.Key.E) return "E";
                if (keyboardkey == Keyboard.Key.F) return "F";
                if (keyboardkey == Keyboard.Key.G) return "G";
                if (keyboardkey == Keyboard.Key.H) return "H";
                if (keyboardkey == Keyboard.Key.I) return "I";
                if (keyboardkey == Keyboard.Key.J) return "J";
                if (keyboardkey == Keyboard.Key.K) return "K";
                if (keyboardkey == Keyboard.Key.L) return "L";
                if (keyboardkey == Keyboard.Key.M) return "M";
                if (keyboardkey == Keyboard.Key.N) return "N";
                if (keyboardkey == Keyboard.Key.O) return "O";
                if (keyboardkey == Keyboard.Key.P) return "P";
                if (keyboardkey == Keyboard.Key.Q) return "Q";
                if (keyboardkey == Keyboard.Key.R) return "R";
                if (keyboardkey == Keyboard.Key.S) return "S";
                if (keyboardkey == Keyboard.Key.T) return "T";
                if (keyboardkey == Keyboard.Key.U) return "U";
                if (keyboardkey == Keyboard.Key.V) return "V";
                if (keyboardkey == Keyboard.Key.W) return "W";
                if (keyboardkey == Keyboard.Key.X) return "X";
                if (keyboardkey == Keyboard.Key.Y) return "Y";
                if (keyboardkey == Keyboard.Key.Z) return "Z";
                if (keyboardkey == Keyboard.Key.Num0) return "Num0";
                if (keyboardkey == Keyboard.Key.Num1) return "Num1";
                if (keyboardkey == Keyboard.Key.Num2) return "Num2";
                if (keyboardkey == Keyboard.Key.Num3) return "Num3";
                if (keyboardkey == Keyboard.Key.Num4) return "Num4";
                if (keyboardkey == Keyboard.Key.Num5) return "Num5";
                if (keyboardkey == Keyboard.Key.Num6) return "Num6";
                if (keyboardkey == Keyboard.Key.Num7) return "Num7";
                if (keyboardkey == Keyboard.Key.Num8) return "Num8";
                if (keyboardkey == Keyboard.Key.Num9) return "Num9";
                if (keyboardkey == Keyboard.Key.Escape) return "Escape";
                if (keyboardkey == Keyboard.Key.LControl) return "LControl";
                if (keyboardkey == Keyboard.Key.LShift) return "LShift";
                if (keyboardkey == Keyboard.Key.LAlt) return "LAlt";
                if (keyboardkey == Keyboard.Key.LSystem) return "LSystem";
                if (keyboardkey == Keyboard.Key.RControl) return "RControl";
                if (keyboardkey == Keyboard.Key.RShift) return "RShift";
                if (keyboardkey == Keyboard.Key.RAlt) return "RAlt";
                if (keyboardkey == Keyboard.Key.RSystem) return "RSystem";
                if (keyboardkey == Keyboard.Key.Menu) return "Menu";
                if (keyboardkey == Keyboard.Key.LBracket) return "LBracket";
                if (keyboardkey == Keyboard.Key.RBracket) return "RBracket";
                if (keyboardkey == Keyboard.Key.Semicolon) return "Semicolon";                
                if (keyboardkey == Keyboard.Key.Comma) return "Comma";
                if (keyboardkey == Keyboard.Key.Period) return "Period";
                if (keyboardkey == Keyboard.Key.Quote) return "Quote";
                if (keyboardkey == Keyboard.Key.Slash) return "Slash";
                if (keyboardkey == Keyboard.Key.Backslash) return "Backslash";                
                if (keyboardkey == Keyboard.Key.Tilde) return "Tilde";
                if (keyboardkey == Keyboard.Key.Equal) return "Equal";
                if (keyboardkey == Keyboard.Key.Hyphen) return "Hyphen";                
                if (keyboardkey == Keyboard.Key.Space) return "Space";
                if (keyboardkey == Keyboard.Key.Enter) return "Enter";                
                if (keyboardkey == Keyboard.Key.Backspace) return "Backspace";                
                if (keyboardkey == Keyboard.Key.Tab) return "Tab";
                if (keyboardkey == Keyboard.Key.PageUp) return "PageUp";
                if (keyboardkey == Keyboard.Key.PageDown) return "PageDown";
                if (keyboardkey == Keyboard.Key.End) return "End";
                if (keyboardkey == Keyboard.Key.Home) return "Home";
                if (keyboardkey == Keyboard.Key.Insert) return "Insert";
                if (keyboardkey == Keyboard.Key.Delete) return "Delete";
                if (keyboardkey == Keyboard.Key.Add) return "Add";
                if (keyboardkey == Keyboard.Key.Subtract) return "Subtract";
                if (keyboardkey == Keyboard.Key.Multiply) return "Multiply";
                if (keyboardkey == Keyboard.Key.Divide) return "Divide";
                if (keyboardkey == Keyboard.Key.Left) return "Left";
                if (keyboardkey == Keyboard.Key.Right) return "Right";
                if (keyboardkey == Keyboard.Key.Up) return "Up";
                if (keyboardkey == Keyboard.Key.Down) return "Down";
                if (keyboardkey == Keyboard.Key.Numpad0) return "Numpad0";
                if (keyboardkey == Keyboard.Key.Numpad1) return "Numpad1";
                if (keyboardkey == Keyboard.Key.Numpad2) return "Numpad2";
                if (keyboardkey == Keyboard.Key.Numpad3) return "Numpad3";
                if (keyboardkey == Keyboard.Key.Numpad4) return "Numpad4";
                if (keyboardkey == Keyboard.Key.Numpad5) return "Numpad5";
                if (keyboardkey == Keyboard.Key.Numpad6) return "Numpad6";
                if (keyboardkey == Keyboard.Key.Numpad7) return "Numpad7";
                if (keyboardkey == Keyboard.Key.Numpad8) return "Numpad8";
                if (keyboardkey == Keyboard.Key.Numpad9) return "Numpad9";
                if (keyboardkey == Keyboard.Key.F1) return "F1";
                if (keyboardkey == Keyboard.Key.F2) return "F2";
                if (keyboardkey == Keyboard.Key.F3) return "F3";
                if (keyboardkey == Keyboard.Key.F4) return "F4";
                if (keyboardkey == Keyboard.Key.F5) return "F5";
                if (keyboardkey == Keyboard.Key.F6) return "F6";
                if (keyboardkey == Keyboard.Key.F7) return "F7";
                if (keyboardkey == Keyboard.Key.F8) return "F8";
                if (keyboardkey == Keyboard.Key.F9) return "F9";
                if (keyboardkey == Keyboard.Key.F10) return "F10";
                if (keyboardkey == Keyboard.Key.F11) return "F11";
                if (keyboardkey == Keyboard.Key.F12) return "F12";
                if (keyboardkey == Keyboard.Key.F13) return "F13";
                if (keyboardkey == Keyboard.Key.F14) return "F14";
                if (keyboardkey == Keyboard.Key.F15) return "F15";
                if (keyboardkey == Keyboard.Key.Pause) return "Pause";
                return "???";
            }
        }
    }

    public class KeyConfig
    {
        private List<KeyInfo> keys;
        private List<KeyInfo> keys_default;

        public KeyConfig()
        {
            keys = new List<KeyInfo>() ;
            keys_default = new List<KeyInfo>();
        }
        public bool isMatchEvent(EventArgs eventarg, ref string actionname)
        {
            var el = keys.Find((k) => k.isMatch(eventarg));
            if (el == null) return false;
            actionname = el.ActionName;
            return true;
        }
        public bool isMatchState(ref string actionname)
        {
            foreach (var key in keys)
            {
                if (!key.isset) continue;
                if (key.ismouse)
                {
                    if (Mouse.IsButtonPressed(key.mousebutton))
                    {
                        actionname = key.ActionName;
                        return true;
                    }
                }
                else
                {
                    if (Keyboard.IsKeyPressed(key.keyboardkey))
                    {
                        actionname = key.ActionName;
                        return true;
                    }
                }
            }
            return false;
        }        
        public void addKey(string actionname, Keyboard.Key Akeyboardkey) 
        {
            var key = new KeyInfo() { ActionName = actionname, ismouse = false, isset = true, keyboardkey = Akeyboardkey };
            keys.Add(key);
            keys_default.Add(key.Clone());
        }
        public void addKey(string actionname, Mouse.Button Amousebutton)
        {
            var key = new KeyInfo() { ActionName = actionname, ismouse = true, isset = true,  mousebutton = Amousebutton };
            keys.Add(key);
            keys_default.Add(key.Clone());
        }
        public int getCount()
        {
            return keys.Count;
        }
        public String getActionName(int i)
        {
            return keys[i].ActionName;
        }
        public String getKeyView(int i)
        {
            return keys[i].getView();
        }
        public void setDefault()
        {
            keys.Clear();
            foreach (var key in keys_default)
                keys.Add(key.Clone());
        }
        public bool setKeyByEvent(int i, EventArgs eventarg)
        {
            if (eventarg is MouseButtonEventArgs mouseButtonEventArgs)
            {
                keys[i].mousebutton = mouseButtonEventArgs.Button;
                keys[i].ismouse = true;
                keys[i].isset = true;
                for (int j=0; j<keys.Count; j++)
                    if ((i != j) && (keys[j].isset))
                        if ((keys[j].ismouse) && (keys[j].mousebutton == mouseButtonEventArgs.Button))
                            keys[j].isset = false;                        
                return true;
            }
            if (eventarg is KeyEventArgs keyEventArg)
            {
                keys[i].keyboardkey = keyEventArg.Code;
                keys[i].ismouse = false;
                keys[i].isset = true;
                for (int j = 0; j < keys.Count; j++)
                    if ((i != j) && (keys[j].isset))
                        if ((!keys[j].ismouse) && (keys[j].keyboardkey == keyEventArg.Code))
                            keys[j].isset = false;
                return true;
            }
            return false;
        }
    }
}
