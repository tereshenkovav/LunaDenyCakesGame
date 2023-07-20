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
        public static Sprite logo;
        public static Sprite back;
        public static Color color_over;
        public static Color color_norm;
        public static Music music_main;

        public static void Load()
        {
            cursor = SfmlHelper.LoadSprite(@"images/cursor.png");
            font = new Font(@"fonts/arial.ttf");
            button = SfmlHelper.LoadSprite(@"images/button.png", SpriteLoaderOptions.sloCentered);
            logo = SfmlHelper.LoadSprite(@"images/logo.png", SpriteLoaderOptions.sloCentered);
            back = SfmlHelper.LoadSprite(@"images/back.png");

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
