using ConsoleTanks.Models.Abstract;
using ConsoleTanks.Behavior;

namespace ConsoleTanks.Models.Objective
{
    class Tank : Vechile, IFireble
    {
        private int id = 0;

        private int damage = 0;

        private char bulletSkin = '.';

        public override int GetHashCode()
        {
            return this.id;
        }

        #region IFireble

        public void Fire()
        {
            throw new System.NotImplementedException();
        }

        public int GetDamage()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
