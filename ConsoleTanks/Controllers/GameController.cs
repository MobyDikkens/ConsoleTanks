/*  GameControlle
 *  
 *  Main brains of the game
 */

using ConsoleTanks.Models.Objective;
using ConsoleTanks.Models.Abstract;
using ConsoleTanks.Rendering.Objective;
using System.Collections.Generic;
using ConsoleTanks.Behavior;
using System.Timers;

namespace ConsoleTanks.Controllers
{
    class GameController
    {
        private Map map = null;

        //  to identify each one of tanks
        private int guid = 0;
               
        private int GUID
        {
            get
            {
                this.guid++;
                return this.guid;
            }
        }

        //  <tank id, tank on map>
        private Dictionary<int, TankOnMap> tanksCollection = null;

        //  <bullet id, bullet>
        private Dictionary<int, BulletOnMap> bulletsCollection = null;

        private Timer timer = null;

        Visualizer Visualizer = new Visualizer();

        #region Constructor

        public GameController(int xLenth, int yLenth, char wall)
        {
            if ((xLenth > 0) && (yLenth > 0) && (wall != ' '))
            {
                this.tanksCollection = new Dictionary<int, TankOnMap>();
                this.bulletsCollection = new Dictionary<int, BulletOnMap>();
                this.map = new Map(xLenth, yLenth, wall);


                timer = new Timer(41);//24 fps
                timer.AutoReset = true;
                timer.Elapsed += TimerElapsed;

            }
            else
            {
                this.tanksCollection = new Dictionary<int, TankOnMap>();
                this.bulletsCollection = new Dictionary<int, BulletOnMap>();
                this.map = new Map(20, 20, '*');

                timer = new Timer(41);//24 fps
                timer.AutoReset = true;
                timer.Elapsed += TimerElapsed;
            }
        }



        #endregion



        #region Timer Handler

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            
            foreach(var bullet in bulletsCollection)
            {
                //  check if bullet intersects with an object
                //  if true decrement hp and remove bullet
                foreach (var tank in tanksCollection)
                {
                    if((bullet.Value.Position.X == tank.Value.Position.X)
                        && (bullet.Value.Position.Y == tank.Value.Position.Y))
                    {
                        tank.Value.Tank.HPImprove((-1) * bullet.Value.Bullet.GetDamage());
                        bulletsCollection.Remove(bullet.Key);
                    }

                }

                //  chech if bullet intersects with a wall
                //  if true then remove
                if(!map.IsEmpty(bullet.Value.Position.X, bullet.Value.Position.Y))
                {
                    bulletsCollection.Remove(bullet.Key);
                }

            }


            //  render map
            Visualizer.RenderMap(map.GetMap(), map.xLenth, map.yLenth);

            //  render tanks
            foreach (var tank in tanksCollection)
            {
                Visualizer.RenderObject(tank.Value.Tank.GetSkin(), tank.Value.Position.X, tank.Value.Position.Y);
            }

            //  render bullets
            foreach(var bullet in bulletsCollection)
            {
                Visualizer.RenderObject(bullet.Value.Bullet.GetSkin(), bullet.Value.Position.X, bullet.Value.Position.Y);
            }

            Visualizer.Visualize();

        }

        #endregion

        #region Methods

        public bool AddTank(int damage, char bulletSkin, int hp, char skin)
        {
            if((bulletSkin != ' ') && (hp > 0) && (skin != ' '))
            {
                TankOnMap newTank = new TankOnMap();
                int guid = this.GUID;
                newTank.Tank = new Tank(guid, damage, bulletSkin, hp, skin);


                //  find an empty place on the map
                //
                //
                bool founded = false;// indecates if the position was found


                for(int i = 0; i < map.xLenth; i++)
                {
                    //  to stop if the position already founded
                    if(founded)
                    {
                        break;
                    }
                    for(int j = 0; j < map.yLenth; j++)
                    {
                        if(map.IsEmpty(i,j))
                        {
                            newTank.Position.X = i;
                            newTank.Position.Y = j;
                            founded = true;
                            break;
                        }
                    }
                }

                //  add to the collection
                tanksCollection.Add(guid, newTank);

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Event Handlers


        private void OnShotHandler(object sender, Directions direction)
        {
            Tank tank = sender as Tank;

            if(tank != null)
            {
                Bullet bullet = new Bullet(tank.GetHashCode(), tank.GetDamage(), tank.GetBulletSkin(), tank.GetCurrentDirection());


                foreach(var tmp in tanksCollection)
                {
                    if(tmp.Key == tank.GetHashCode())
                    {
                        Position bulletPosition = new Position();

                        GetDesirePosition(ref bulletPosition, tank, direction);

                        BulletOnMap newBullet = new BulletOnMap();
                        newBullet.Bullet = bullet;
                        newBullet.Position = bulletPosition;
                        bulletsCollection.Add(tmp.Key, newBullet);

                    }
                }

            }

        }

        private void OnMoveHandler(object sender, Directions direction)
        {
            //  check if we can move (there arent wall or another tank)

            Tank tank = sender as Tank;

            if (tank != null)
            {

                Position desirePosition = new Position();

                //  get the desire position
                if (GetDesirePosition(ref desirePosition, tank, direction))
                {

                    //  if desire position doesnt overhead the map size
                    if ((desirePosition.X < map.xLenth) && (desirePosition.X >= 0)
                        && (desirePosition.Y < map.yLenth) && (desirePosition.Y >= 0))
                    {
                        //  if desire position doesnt intersects with the map(wall)
                        if (map.IsEmpty(desirePosition.X, desirePosition.Y))
                        {
                            //  if desire position doesnt intersects with ohther tanks
                            if(!IntersectsWithObjects(desirePosition.X, desirePosition.Y))
                            {
                                //  if desire position intersects with the bullet
                                foreach(var bullet in bulletsCollection)
                                {
                                    if((bullet.Value.Position.X == desirePosition.X) && (bullet.Value.Position.Y == desirePosition.Y))
                                    {
                                        //  decrement hp
                                        tank.HPImprove(bullet.Value.Bullet.GetDamage() * (-1));
                                        bulletsCollection.Remove(bullet.Key);
                                        break;
                                    }
                                }



                                //  so desire position is OK
                                //  find the tank and change current position to desire
                                foreach(var tmp in tanksCollection)
                                {
                                    if(tmp.Key == tank.GetHashCode())
                                    {
                                        TankOnMap newValue = new TankOnMap();

                                        newValue.Position.X = desirePosition.X;
                                        newValue.Position.Y = desirePosition.Y;

                                        newValue.Tank = tmp.Value.Tank;


                                        tanksCollection[tmp.Key] = newValue;
                                    }
                                }

                            }
                        }
                    }
                }


            }


        }


        private void OnHPCHangedHandler(object sender, int newHP)
        {
            //  if hp is below 0 
            //  delete tank from the collection
            if (newHP <= 0)
            {
                Tank caller = sender as Tank;

                if (caller != null)
                {

                    foreach (var tank in tanksCollection)
                    {
                        if (tank.Key == caller.GetHashCode())
                        {
                            tanksCollection.Remove(tank.Key);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Helpers

        private bool IntersectsWithObjects(int x, int y)
        {
            foreach(var tank in tanksCollection)
            {
                if((tank.Value.Position.X == x) && (tank.Value.Position.Y == y))
                {
                    return true;
                }
            }
            return false;
        }


        private bool GetDesirePosition(ref Position desire, Tank tank, Directions direction)
        {
            if(tank != null)
            {
                //  find currect position
                //  and then change to desire
                foreach (var tmpTank in tanksCollection)
                {
                    if (tmpTank.Key == tank.GetHashCode())
                    {

                        switch (direction)
                        {
                            case Directions.Down:
                                {
                                    desire.X = tmpTank.Value.Position.X;
                                    desire.Y = tmpTank.Value.Position.Y - 1;
                                    break;
                                }
                            case Directions.Left:
                                {
                                    desire.X = tmpTank.Value.Position.X - 1;
                                    desire.Y = tmpTank.Value.Position.Y;
                                    break;
                                }
                            case Directions.Right:
                                {
                                    desire.X = tmpTank.Value.Position.X + 1;
                                    desire.Y = tmpTank.Value.Position.Y;
                                    break;
                                }
                            case Directions.Up:
                                {
                                    desire.X = tmpTank.Value.Position.X;
                                    desire.Y = tmpTank.Value.Position.Y + 1;
                                    break;
                                }
                            default: { return false; }
                        }
                        return true;
                    }
                }


                return false;
            }
            else
            {
                return false;
            }
        }


        #endregion


        #region Inside Stuctures

        //  each one of tanks have its own position
        struct Position
        {
            public int X;
            public int Y;
        }

        struct TankOnMap
        {
            public Tank Tank;
            public Position Position;
        }

        struct BulletOnMap
        {
            public Bullet Bullet;
            public Position Position;
        }

        #endregion

    }

}
