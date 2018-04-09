/*  IAliveble
 *  
 *  Add health behavior to 
 *  an object
 */

namespace ConsoleTanks.Behavior
{
    interface IAliveble
    {
        int GetHP();
        void HPDecrement();
        void HPIncrement();
        void HPImprove(int value);
        event System.EventHandler<int> HPChanged;
    }

}
