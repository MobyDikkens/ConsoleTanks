/*  Vechile
 *  
 *  Describes a behavior
 *  for an abstract machine
 */

using System;
using ConsoleTanks.Behavior;


namespace ConsoleTanks.Models.Abstract
{
    abstract class Vechile : IMoveble, IAliveble
    {
        private int hp = 0;

        private char skin = '^';

        private Directions direction = Directions.Up;


        public char GetSkin()
        {
            return this.skin;
        }


        #region Constructors

        public Vechile()
        {
            this.hp = 5;
            this.skin = '^';
            this.HPChanged = null;
            this.direction = Directions.Up;
            this.Moved = null;
        }

        public Vechile(int hp, char skin)
        {
            if((hp > 0) && (skin != ' '))
            {
                this.hp = hp;
                this.skin = skin;
                this.HPChanged = null;
                this.direction = Directions.Up;
                this.Moved = null;
            }
            else
            {
                this.hp = 5;
                this.skin = '^';
                this.HPChanged = null;
                this.direction = Directions.Up;
                this.Moved = null;
            }
        }


        #endregion


        #region IAliveble

        public event EventHandler HPChanged = null;

        int IAliveble.GetHP()
        {
            return this.hp;
        }

        void IAliveble.HPDecrement()
        {
            if (this.HPChanged != null)
            {
                this.hp--;
                HPChanged(this, new EventArgs());
            }
        }

        void IAliveble.HPImprove(int value)
        {
            if (this.HPChanged != null)
            {
                this.hp += value;
                HPChanged(this, new EventArgs());
            }
        }

        void IAliveble.HPIncrement()
        {
            if (this.HPChanged != null)
            {
                this.hp++;
                HPChanged(this, new EventArgs());
            }
        }

        #endregion

        #region IMoveble

        public event EventHandler<Directions> Moved = null;

        void IMoveble.MoveDown()
        {
            if (this.Moved != null)
            {
                this.direction = Directions.Down;
                Moved(this, Directions.Down);
            }
        }

        void IMoveble.MoveLeft()
        {
            if (this.Moved != null)
            {
                this.direction = Directions.Left;
                Moved(this, Directions.Left);
            }
        }

        void IMoveble.MoveRight()
        {
            if (this.Moved != null)
            {
                this.direction = Directions.Right;
                Moved(this, Directions.Right);
            }
        }

        void IMoveble.MoveUp()
        {
            if (this.Moved != null)
            {
                this.direction = Directions.Up;
                Moved(this, Directions.Up);
            }
        }

        public Directions GetCurrentDirection()
        {
            return this.direction;
        }

        #endregion

    }
}
