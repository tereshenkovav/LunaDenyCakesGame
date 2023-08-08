using SFML.Graphics;
using SFML.Audio;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class CommonData
    {
        // Ресурсы и константы
        public static Sprite cursor;
        public static Font font;
        public static Sprite button;
        public static Sprite button_small;
        public static Sprite logo;
        public static Sprite back;
        public static Sprite checkbox_on;
        public static Sprite checkbox_off;
        public static Color color_over;
        public static Color color_norm;
        public static Music music_main;

        public static string action_switch = "action_switch";
        public static string action_apply = "action_apply";
        public static string action_sel_teleport = "action_sel_teleport";
        public static string action_sel_laser = "action_sel_laser";
        public static string action_sel_chicken = "action_sel_chicken";
        public static string action_sel_shield = "action_sel_shield";
        public static string action_left = "action_left";
        public static string action_right = "action_right";

        public static void Load()
        {
            cursor = SfmlHelper.LoadSprite(@"images/cursor.png");
            font = new Font(@"fonts/arial.ttf");
            button = SfmlHelper.LoadSprite(@"images/button.png", SpriteLoaderOptions.sloCentered);
            button_small = SfmlHelper.LoadSprite(@"images/button_small.png", SpriteLoaderOptions.sloCentered);
            logo = SfmlHelper.LoadSprite(@"images/logo.png", SpriteLoaderOptions.sloCentered);
            back = SfmlHelper.LoadSprite(@"images/back.png");
            checkbox_on = SfmlHelper.LoadSprite(@"images/checkbox_on.png");
            checkbox_off = SfmlHelper.LoadSprite(@"images/checkbox_off.png");

            music_main = new Music(@"music/music_main.ogg");
            music_main.Loop = true;

            color_over = SfmlHelper.createSFMLColor(255, 255, 255);
            color_norm = SfmlHelper.createSFMLColor(200, 200, 200);
        }
        public static void UnLoad()
        {
            
        }
    }
}
