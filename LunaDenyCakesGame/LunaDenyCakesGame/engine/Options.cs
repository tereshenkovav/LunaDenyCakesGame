using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace SfmlNetEngine
{
    public enum Difficult { Easy, Medi, Hard };

    public class OptionsParams
    {
        public bool soundon { get; set; }
        public bool musicon { get; set; }
        public bool fullscreen { get; set; }
        public string currentlang { get; set; }
        public int difficult { get; set; }
        public List<KeyInfo> keys { get; set; }
    }

    public class Options
    {
        private String configfile = "";

        private int window_w = 800;
        private int window_h = 600;
        private bool soundon = true;
        private bool musicon = true;
        private bool fullscreen = false;
        private string currentlang = "";
        private List<String> languages = new List<string>();
        private Difficult difficult = Difficult.Medi;
        public KeyConfig keyconfig = new KeyConfig();
        
        public void setWindowParams(int w, int h)
        {
            window_w = w;
            window_h = h;
        }
        public int getWindowWidth()
        {
            return window_w ;
        }
        public int getWindowHeigth()
        {
            return window_h;
        }
        public void invertSoundOn()
        {
            soundon = !soundon;
            SaveParam();
        }
        public bool isSoundOn()
        {
            return soundon;
        }
        public void invertMusicOn()
        {
            musicon = !musicon;
            SaveParam();
        }
        public bool isMusicOn()
        {
            return musicon;
        }
        public void invertFullScreen()
        {
            fullscreen = !fullscreen;
            SaveParam();
        }
        public bool isFullScreen()
        {
            return fullscreen;
        }
        public Difficult getDifficult()
        {
            return difficult;
        }
        public String getDifficultCode()
        {
            switch (difficult) {
                case Difficult.Easy: return "easy";
                case Difficult.Medi: return "medi";
                case Difficult.Hard: return "hard";
                default: return "medi";
            }
        }
        public void switchDifficult()
        {
            if (difficult == Difficult.Easy) difficult = Difficult.Medi; else
            if (difficult == Difficult.Medi) difficult = Difficult.Hard; else
            if (difficult == Difficult.Hard) difficult = Difficult.Easy;
            SaveParam();
        }
        public void setUsedLanguages(List<String> langs)
        {
            languages.Clear();
            foreach (var item in langs)
                languages.Add(item);
            if (languages.Count > 0) currentlang = languages[0];
        }
        public void setCurrentLanguage(String lang)
        {
            if (languages.Contains(lang))
            {
                currentlang = lang;
                SaveParam();
            }
        }
        public void switchCurrentLanguage()
        {
            if (languages.Count == 0) return;
            int idx = languages.IndexOf(currentlang);
            idx++;
            if (idx >= languages.Count) idx = 0;
            setCurrentLanguage(languages[idx]);

        }
        public String getCurrentLanguage()
        {
            return currentlang;
        }
        public String getFilenameByLanguageIfExist(String filename) {
            if (currentlang == "") return filename;
            FileInfo fi = new FileInfo(filename);
            string ext = fi.Extension;
            string langfilename = filename.Substring(0,filename.Length - ext.Length ) + "." + currentlang + ext;
            if (File.Exists(langfilename)) return langfilename;
            return filename;
        }
        public void LoadParams(String filename)
        {
            configfile = filename;
            if (File.Exists(filename))
            {
                var obj = JsonSerializer.Deserialize<OptionsParams>(File.ReadAllText(filename));
                soundon = obj.soundon;
                musicon = obj.musicon;
                fullscreen = obj.fullscreen;
                setCurrentLanguage(obj.currentlang);
                difficult = (Difficult)obj.difficult;
                keyconfig.setAllKeys(obj.keys);
            }
        }
        public void SaveParam()
        {
            if (configfile == "") return;
            var obj = new OptionsParams() { 
                soundon = soundon,
                musicon = musicon,
                fullscreen = fullscreen,
                currentlang = currentlang,
                difficult = (int)difficult,
                keys = keyconfig.getAllKeys()
            };
            File.WriteAllText(configfile, JsonSerializer.Serialize(obj,new JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}
