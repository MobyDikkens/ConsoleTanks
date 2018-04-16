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
using System.Threading.Tasks;
using System;

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

        private PlayerController playerController = null;

        //  all right
        #region Constructor


        //  basic initializations
        //  parametrized constructor
        public GameController(int xLenth, int yLenth, char wall)
        {
            if ((xLenth > 0) && (yLenth > 0) && (wall != ' '))
            {
                this.tanksCollection = new Dictionary<int, TankOnMap>();
                this.bulletsCollection = new Dictionary<int, BulletOnMap>();
                this.map = new Map(xLenth, yLenth, wall, ConsoleColor.DarkGray);


                timer = new Timer(41);//24 fps
                timer.AutoReset = true;
                timer.Elapsed += TimerElapsed;

            }
            else
            {
                this.tanksCollection = new Dictionary<int, TankOnMap>();
                this.bulletsCollection = new Dictionary<int, BulletOnMap>();
                this.map = new Map(20, 20, '*',ConsoleColor.DarkGray);

                timer = new Timer(36);//24 fps
                timer.AutoReset = true;
                timer.Elapsed += TimerElapsed;
            }

            
            Visualizer.Render(map.GetMap(), map.xLenth, map.yLenth, map.GetColor());
        }



        #endregion

        //  all right
        #region Player Controller Handler

        //  player controll key pressed handler
        private void OnKeyPressed(object sender, KeyPressedState type)
        {
            var tank = sender as Tank;
            if (tank != null && tanksCollection.ContainsKey(tank.GetHashCode()))
            {
                var tmp = tanksCollection[tank.GetHashCode()];

                switch (type)
                {
                    case KeyPressedState.Backward: { tmp.Tank.MoveDown(); break; }
                    case KeyPressedState.Foreward: { tmp.Tank.MoveUp(); break; }
                    case KeyPressedState.Left: { tmp.Tank.MoveLeft(); break; }
                    case KeyPressedState.Right: { tmp.Tank.MoveRight(); break; }
                    case KeyPressedState.Shout: { tmp.Tank.Fire(); break; }
                }
            }
        }

        #endregion

        //  all right
        #region Timer Handler

        //  called each time to controll bullets movement
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (bulletsCollection.Count > 0)
            {
                // to collect bullets and delete it after foreach
                List<BulletOnMap> toDelete = new List<BulletOnMap>();

                //  to collect bullets and then change it in bulletsCollection
                List<BulletOnMap> toChange = new List<BulletOnMap>();

                foreach (var bullet in bulletsCollection)
                {

                    //  check if bullet intersects with an object
                    //  if true decrement hp and remove bullet
                    foreach (var tank in tanksCollection)
                    {
                        if ((bullet.Value.Position.X == tank.Value.Position.X)
                            && (bullet.Value.Position.Y == tank.Value.Position.Y)
                            && (bullet.Key != tank.Key))
                        {
                            tank.Value.Tank.HPImprove((-1) * bullet.Value.Bullet.GetDamage());


                            toDelete.Add(bullet.Value);
                        }

                    }

                    //  delete current position and change it to the new
                    toDelete.Add(bullet.Value);

                    //  object to change
                    BulletOnMap tmp = bullet.Value;

                    switch (bullet.Value.Bullet.GetDirection())
                    {

                        case Directions.Down:
                            {
                                tmp.Position.Y--; break;
                            }
                        case Directions.Up:
                            {
                                tmp.Position.Y++; break;
                            }
                        case Directions.Right:
                            {
                                tmp.Position.X++; break;
                            }
                        case Directions.Left:
                            {
                                tmp.Position.X--; break;
                            }

                    }

                   
                    




                    //  chech if bullet intersects with a wall
                    //  if true then remove
                    if (map.IsEmpty(tmp.Position.X, tmp.Position.Y))
                    {
                        //  intersect bounds
                        if (map.xLenth > tmp.Position.X && map.yLenth > tmp.Position.Y
                            && tmp.Position.X >= 0 && tmp.Position.Y >= 0)
                        {
                            toChange.Add(tmp);
                        }
                    }
                    else
                    {
                        //  delete bullet from the map
                        toDelete.Add(bullet.Value);
                    }

                   


                }

                //  remove all bullets from toDelete list
                foreach (var remove in toDelete)
                {
                    if(bulletsCollection.Remove(remove.Bullet.GetHashCode()))
                    {
                        //  delete bullet from the map
                        Visualizer.Render(' ', remove.Position.X, remove.Position.Y, System.Console.BackgroundColor);
                    }
                }

                //  change bullets
                foreach(var change in toChange)
                {
                    bulletsCollection[change.Bullet.GetHashCode()] = change;
                    Visualizer.Render(change.Bullet.GetSkin(), change.Position.X, change.Position.Y, change.Bullet.GetColor());
                }

            }

        }


        #endregion

        #region Methods

        public bool AddPlayer(int damage, char bulletSkin, int hp, char skin, ConsoleColor color,
            System.ConsoleKey foreward, System.ConsoleKey backward, System.ConsoleKey left,
            System.ConsoleKey right, System.ConsoleKey shout)
        {
            if((bulletSkin != ' ') && (hp > 0) && (skin != ' '))
            {
                TankOnMap newTank = new TankOnMap();
                int guid = this.GUID;
                newTank.Tank = new Tank(guid, damage, bulletSkin, hp, skin, color);


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

                //  add event handlers
                newTank.Tank.OnShot += OnShotHandler;
                newTank.Tank.Moved += OnMoveHandler;
                newTank.Tank.HPChanged += OnHPCHangedHandler;

                //  add to the collection
                tanksCollection.Add(guid, newTank);
                playerController = new PlayerController(foreward, backward, right, left, shout);
                playerController.Tank = newTank.Tank;
                playerController.KeyPressed += OnKeyPressed;
                

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Start()
        {
            this.playerController.Run();
            this.timer.Start();
            //task.Wait();
        }

        #endregion

        #region Event Handlers


        private void OnShotHandler(object sender, Bullet bullet)
        {
            Tank tank = sender as Tank;

            if(tank != null)
            {

                foreach(var tmp in tanksCollection)
                {
                    if(tmp.Key == tank.GetHashCode())
                    {
                        Position bulletPosition = new Position();

                        GetDesirePosition(ref bulletPosition, tank, bullet.GetDirection());

                        BulletOnMap newBullet = new BulletOnMap();
                        newBullet.Bullet = bullet;
                        newBullet.Position = bulletPosition;

                        //  check if the bullet doesnt exist in the collection
                        if(!bulletsCollection.ContainsKey(tmp.Key))
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
                            if(!IntersectsWithObjects(tank, desirePosition.X, desirePosition.Y))
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
                                

                                if (tanksCollection.ContainsKey(tank.GetHashCode()))
                                {
                                    var tmp = tanksCollection[tank.GetHashCode()];

                                    if (tmp.Tank.GetHashCode() == tank.GetHashCode())
                                    {
                                        TankOnMap newValue = new TankOnMap();

                                        newValue.Position.X = desirePosition.X;
                                        newValue.Position.Y = desirePosition.Y;

                                        newValue.Tank = tmp.Tank;

                                        //  Render
                                        //  Delete old position from map
                                        Visualizer.Render(' ', tmp.Position.X, tmp.Position.Y, System.Console.BackgroundColor);
                                        //  Add new position
                                        Visualizer.Render(newValue.Tank.GetSkin(), newValue.Position.X, newValue.Position.Y, newValue.Tank.GetColor());


                                        tanksCollection[tmp.Tank.GetHashCode()] = newValue;

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

        private bool IntersectsWithObjects(Tank sender, int x, int y)
        {
            foreach(var tank in tanksCollection)
            {
                if((tank.Value.Position.X == x) && (tank.Value.Position.Y == y) && (tank.Key != sender.GetHashCode()))
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
