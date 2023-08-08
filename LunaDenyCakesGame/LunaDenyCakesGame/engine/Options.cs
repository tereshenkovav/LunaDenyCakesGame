using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace SfmlNetEngine
{    
    public class OptionsParams
    {
        public bool soundon { get; set; }
        public bool musicon { get; set; }
        public bool fullscreen { get; set; }
        public string currentlang { get; set; }
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
        public KeyConfig keyconfig = new KeyConfig();
        private Type paramstype;
        
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
        protected virtual void loadCustom(Object obj)
        {

        }
        protected virtual void saveCustom(Object obj)
        {

        }
        public void LoadParams(String filename, Type type) 
        {
            configfile = filename;
            paramstype = type;
            if (File.Exists(filename))
            {
                var obj = (OptionsParams)JsonSerializer.Deserialize(File.ReadAllText(filename),type);
                soundon = obj.soundon;
                musicon = obj.musicon;
                fullscreen = obj.fullscreen;
                setCurrentLanguage(obj.currentlang);
                keyconfig.setAllKeys(obj.keys);
                loadCustom(obj);
            }
        }
        public void SaveParam()
        {
            if (configfile == "") return;
            var obj = (OptionsParams)Activator.CreateInstance(paramstype) ;
            obj.soundon = soundon;
            obj.musicon = musicon;
            obj.fullscreen = fullscreen;
            obj.currentlang = currentlang;
            obj.keys = keyconfig.getAllKeys();
            saveCustom(obj);
            File.WriteAllText(configfile, JsonSerializer.Serialize(obj,paramstype, new JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}
