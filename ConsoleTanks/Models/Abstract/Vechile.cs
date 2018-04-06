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
        }

        public Vechile(int hp, char skin)
        {
            if((hp > 0) && (skin != ' '))
            {
                this.hp = hp;
                this.skin = skin;
                this.HPChanged = null;
                this.direction = Directions.Up;
            }
            else
            {
                this.hp = 5;
                this.skin = '^';
                this.HPChanged = null;
                this.direction = Directions.Up;
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

        void IMoveble.MoveDown()
        {
            this.direction = Directions.Down;
        }

        void IMoveble.MoveLeft()
        {
            this.direction = Directions.Left;
        }

        void IMoveble.MoveRight()
        {
            this.direction = Directions.Right;
        }

        void IMoveble.MoveUp()
        {
            this.direction = Directions.Up;
        }

        public Directions GetCurrentDirection()
        {
            return this.direction;
        }

        #endregion

    }
}
