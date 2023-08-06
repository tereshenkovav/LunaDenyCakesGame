using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace SfmlNetEngine
{
    public abstract class Achievement
    {
        protected bool iscompleted;
        
        public abstract String getCode();
        public abstract void Update(Object obh);
        public virtual void Reset()
        {
            iscompleted = false;
        }
        public bool isCompleted()
        {
            return iscompleted;
        }
    }

    public class AchievementStore
    {
        private Dictionary<String, bool> completed;
        private List<Achievement> detectors;
        private string storefile ;

        public AchievementStore()
        {
            completed = new Dictionary<String, bool>();
            detectors = new List<Achievement>();
            storefile = "";
        }
        public void addAchievement(Achievement obj)
        {
            detectors.Add(obj);
        }
        public void ResetAchievements()
        {
            completed.Clear();
            SaveAchievements();
        }
        public void ResetDetector()
        {
            foreach (var item in detectors)
                item.Reset();
        }
        public void Update(Object obj)
        {
            foreach (var item in detectors) { 
                if (!item.isCompleted()) item.Update(obj);
                if (item.isCompleted())
                {
                    if (!completed.ContainsKey(item.getCode()))
                    {
                        completed.Add(item.getCode(), true);
                        SaveAchievements();
                    }
                    else
                    {
                        if (!completed[item.getCode()])
                        {
                            completed[item.getCode()] = true;
                            SaveAchievements();
                        }
                    }
                }
            }
        }
        public int getCount()
        {
            return detectors.Count;
        }
        public int getCompletedCount()
        {
            int r = 0;
            foreach (var item in detectors)
                if (completed.ContainsKey(item.getCode()))
                    if (completed[item.getCode()]) r++;
            return r;
        }
        public String getName(int i)
        {
            return ObjModule.texts.getText("achievement_" + detectors[i].getCode());
        }
        public bool isCompleted(int i)
        {
            if (!completed.ContainsKey(detectors[i].getCode())) return false;
            return completed[detectors[i].getCode()];
        }
        public void LoadAchievements(String filename)
        {
            storefile = filename;
            if (File.Exists(filename))
                completed = JsonSerializer.Deserialize<Dictionary<string,bool>>(File.ReadAllText(filename));
        }
        public void SaveAchievements()
        {
            if (storefile != "")
                File.WriteAllText(storefile, JsonSerializer.Serialize(completed, new JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}
