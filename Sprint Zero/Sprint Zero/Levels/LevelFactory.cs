using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public static class LevelFactory
    {
        private static Dictionary<int,string> levels;
        private static Dictionary<int, object> typeMap;

        public static Level GetLevel(List<IController> controllers, int index)
        {
            string levelName = levels[index];
            object type = typeMap[index];
            Level level = null;

            if (levelName != null && type != null)
            {
                LevelParser parser = new LevelParser(levels[index], controllers);
                parser.Parse();

                if (type is BasicLevel)
                {
                    level = new BasicLevel();
                }

                level.SetCharacterList(parser.CharactersList);
                level.SetCollectableList(parser.CollectablesList);
                level.SetEnemyList(parser.EnemiesList);
                level.SetItemList(parser.ItemsList);
                level.SetTerrainList(parser.TerrainList);
                level.SetControllerList(controllers);
                level.SetItemSetList(parser.ItemSetList);
                level.SetBackground(parser.Background);
                level.SetMusic(parser.Music);
                

                if (parser.CamerasList.Count > 0)
                {
                    level.SetCamera(parser.CamerasList[0]);
                }
            }

            return level;
        }

        public static void Setup()
        {
            PopulateDefaultLevelMappings();
            PopulateTypeMap();
        }

        private static void PopulateDefaultLevelMappings()
        {
            levels = new Dictionary<int, string>();
            levels.Add(0, "Levels/example.xml");
            levels.Add(1, "Levels/Level 1-1/Level1A.xml");
            levels.Add(2, "Levels/Level 1-1/Level1B.xml");
            levels.Add(3, "Levels/Level 1-1/Level1C.xml");
            levels.Add(4, "Levels/Level 1-2/Level2.xml");
            levels.Add(5, "Levels/Level 1-3/Level3.xml");
            levels.Add(6, "Levels/Level 1-4/Level4.xml");
            levels.Add(7, "Levels/Level Hub/LevelHub.xml");
            
        }

        private static void PopulateTypeMap()
        {
            typeMap = new Dictionary<int, object>();
            typeMap.Add(1, new BasicLevel());
            typeMap.Add(2, new BasicLevel());
            typeMap.Add(3, new BasicLevel());
            typeMap.Add(4, new BasicLevel());
            typeMap.Add(5, new BasicLevel());
            typeMap.Add(6, new BasicLevel());
            typeMap.Add(7, new BasicLevel());
        }
    }
}
