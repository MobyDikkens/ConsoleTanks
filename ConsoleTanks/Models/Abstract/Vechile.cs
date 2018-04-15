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

        private char skin = '▲';

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

        public event EventHandler<int> HPChanged = null;

        public int GetHP()
        {
            return this.hp;
        }

        public void HPDecrement()
        {
            if (this.HPChanged != null)
            {
                this.hp--;
                HPChanged(this, this.hp);
            }
        }

        public void HPImprove(int value)
        {
            if (this.HPChanged != null)
            {
                this.hp += value;
                HPChanged(this, this.hp);
            }
        }

        public void HPIncrement()
        {
            if (this.HPChanged != null)
            {
                this.hp++;
                HPChanged(this, this.hp);
            }
        }

        #endregion

        #region IMoveble

        public event EventHandler<Directions> Moved = null;

        public void MoveDown()
        {
            if (this.Moved != null)
            {
                this.direction = Directions.Down;
                this.skin = '▲';
                Moved(this, Directions.Down);
            }
        }

        public void MoveLeft()
        {
            if (this.Moved != null)
            {
                this.skin = '◄';
                this.direction = Directions.Left;
                Moved(this, Directions.Left);
            }
        }

        public void MoveRight()
        {
            if (this.Moved != null)
            {
                this.skin = '►';
                this.direction = Directions.Right;
                Moved(this, Directions.Right);
            }
        }

        public void MoveUp()
        {
            if (this.Moved != null)
            {
                this.skin = '▼';
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
