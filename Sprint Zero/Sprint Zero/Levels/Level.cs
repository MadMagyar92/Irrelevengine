using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public abstract class Level
    {
        public bool IsCompleted { get; set; }
        public bool IsFailed { get; set; }
        public string TransitionType { get; set; }
        public bool IsEventActivated { get; protected set; }
        public int NextLevel { get; protected set; }
        public List<ICollidable> characterList;

        protected List<ICollidable> enemyList;
        protected List<ICollidable> collectableList;
        protected List<ICollidable> itemList;
        protected List<ICollidable> terrainList;
        protected List<IController> controllerList;
        protected List<ItemSet> itemSetList;
        protected Camera camera;
        protected AbstractAnimatedSprite background;
        protected HUD hud;
        protected SoundEffectInstance music, invincibleMusic;
        protected AbstractEvent abstractEvent;

        public Level()
        {
            this.IsCompleted = false;
            this.TransitionType = Constants.NO_LEVEL_STRING;
            this.IsFailed = false;
            this.IsEventActivated = false;
            this.camera = new Camera();
            this.characterList = new List<ICollidable>();
            this.enemyList = new List<ICollidable>();
            this.collectableList = new List<ICollidable>();
            this.itemList = new List<ICollidable>();
            this.terrainList = new List<ICollidable>();
            this.controllerList = new List<IController>();

            CollisionDetector.ClearDetector();
        }

        public virtual void SetBackground(AbstractAnimatedSprite background)
        {
            this.background = (AbstractAnimatedSprite)background;
        }

        public virtual void SetMusic(SoundEffectInstance music)
        {
            this.music = (SoundEffectInstance)music;
            this.invincibleMusic = SoundFactory.GetInvincibleSong();
            if(music != null)
                music.Play();
        }

        public virtual void SetCamera(Camera camera)
        {
            this.camera = camera;
        }

        public virtual void SetCharacterList(List<AbstractCharacter> list)
        {
            foreach (AbstractCharacter c in list)
            {
                ICollidable collidable = (ICollidable)c;
                characterList.Add(collidable);
                CollisionDetector.AddCollidable(collidable);
            }
        }

        public virtual void SetEnemyList(List<IEnemy> list)
        {
            foreach (IEnemy c in list)
            {
                ICollidable collidable = (ICollidable)c;
                enemyList.Add(collidable);
                CollisionDetector.AddCollidable(collidable);
            }
        }

        public virtual void SetCollectableList(List<ICollectable> list)
        {
            foreach (ICollectable c in list)
            {
                ICollidable collidable = (ICollidable)c;
                collectableList.Add(collidable);
                CollisionDetector.AddCollidable(collidable);
            }
        }

        public virtual void SetItemList(List<IItem> list)
        {
            foreach (IItem c in list)
            {
                ICollidable collidable = (ICollidable)c;
                itemList.Add(collidable);
                CollisionDetector.AddCollidable(collidable);
            }
        }

        public virtual void SetTerrainList(List<AbstractTerrain> list)
        {
            foreach (AbstractTerrain c in list)
            {
                ICollidable collidable = (ICollidable)c;
                terrainList.Add(collidable);
                CollisionDetector.AddCollidable(collidable);
            }
        }

        public virtual void SetItemSetList(List<ItemSet> list)
        {
            itemSetList = list;
            foreach (ItemSet set in list)
            {
                CollisionDetector.AddCollidableList(set.Collidables);
            }
        }

        public virtual void SetControllerList(List<IController> list)
        {
            controllerList = list;
        }

        public virtual void Update()
        {
            //Update all objects' positions, then check for collisions.
            //Unnecessary collision checks are skipped.
            if (IsEventActivated && abstractEvent.IsFinished == false)
            {
                abstractEvent.Update();
                return;
            }
            else
            {
                abstractEvent = null;
                IsEventActivated = false;
            }

            foreach (AbstractCharacter character in characterList)
            {
                character.Update();

            }
            foreach (IEnemy enemy in enemyList)
            {
                enemy.Update();
            }
            foreach (ICollectable collectable in collectableList)
            {
                collectable.Update();
            }
            foreach (IItem item in itemList)
            {
                item.Update();

                if (item is Door && ((Door)item).ShouldSwitchToNextLevel)
                {
                    TransitionType = ((Door)item).MessageToReceivers;
                    NextLevel = ((Door)item).NextLevel;
                }
            }
            foreach (ItemSet itemSet in itemSetList)
            {
                itemSet.Update();

                foreach (IReceiver receiver in itemSet.Receivers)
                {
                    if (receiver is Door && ((Door)receiver).ShouldSwitchToNextLevel)
                    {
                        TransitionType = ((Door)receiver).MessageToReceivers;
                        NextLevel = ((Door)receiver).NextLevel;
                    }
                }

                foreach (ISender sender in itemSet.Senders)
                {
                    if (sender is Door && ((Door)sender).ShouldSwitchToNextLevel)
                    {
                        TransitionType = ((Door)sender).MessageToReceivers;
                        NextLevel = ((Door)sender).NextLevel;
                    }
                }
            }
            foreach (AbstractTerrain terrain in terrainList)
            {
                terrain.Update();
            }

            camera.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsEventActivated && abstractEvent.IsFinished == false)
            {
                abstractEvent.Draw(spriteBatch, camera);
                return;
            }

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

            hud.Draw(spriteBatch, camera);
        }

        public virtual void StopMusic()
        {
            if (music != null && invincibleMusic != null)
            {
                music.Stop();
                invincibleMusic.Stop();
            }
        }

        public virtual void RegisterEvent(AbstractEvent abstractEvent)
        {
            this.abstractEvent = abstractEvent;
            this.IsEventActivated = true;
            abstractEvent.SetEventLists(this, characterList, enemyList, collectableList,
                itemList, terrainList, controllerList, itemSetList,
                camera, background, hud, music, invincibleMusic);
        }
    }
}
