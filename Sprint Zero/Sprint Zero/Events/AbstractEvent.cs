using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Completely_Irrelevant
{
    public abstract class AbstractEvent : IDrawable, IUpdatable
    {
        public bool IsFinished { get; set; }

        protected Level level;
        protected List<ICollidable> characterList;

        protected List<ICollidable> enemyList;
        protected List<ICollidable> collectableList;
        protected List<ICollidable> itemList;
        protected List<ICollidable> terrainList;
        protected List<IController> controllerList;
        protected List<ItemSet> itemSetList;
        protected Camera camera;
        protected AbstractAnimatedSprite background;
        protected SoundEffectInstance music;
        protected SoundEffectInstance invincibleMusic;

        public AbstractEvent()
        {
            IsFinished = false;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {

            if (background != null)
            {
                background.Draw(spriteBatch, (int)camera.Transform.M11, (int)camera.Transform.M22, Color.White, camera);
            }

            foreach (IEnemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch, camera);
            }
            foreach (ICollectable collectable in collectableList)
            {
                collectable.Draw(spriteBatch, camera);
            }
            foreach (IItem item in itemList)
            {
                item.Draw(spriteBatch, camera);
            }
            foreach (AbstractTerrain terrain in terrainList)
            {
                terrain.Draw(spriteBatch, camera);
            }
            foreach (ItemSet itemSet in itemSetList)
            {
                itemSet.Draw(spriteBatch, camera);
            }

            foreach (AbstractCharacter character in characterList)
            {
                character.Draw(spriteBatch, camera);
            }
        }

        public virtual void SetEventLists(Level level, List<ICollidable> characterList, List<ICollidable> enemyList, List<ICollidable> collectableList,
         List<ICollidable> itemList, List<ICollidable> terrainList, List<IController> controllerList, List<ItemSet> itemSetList,
         Camera camera, AbstractAnimatedSprite background, HUD hud, SoundEffectInstance music, SoundEffectInstance invincibleMusic)
        {
            this.level = level;
            this.characterList = characterList;

            this.enemyList = enemyList;
            this.collectableList = collectableList;
            this.itemList = itemList;
            this.terrainList = terrainList;
            this.controllerList = controllerList;
            this.itemSetList = itemSetList;
            this.camera = camera;
            this.background = background;
            this.music = music;
            this.invincibleMusic = invincibleMusic;
        }
    }
}
