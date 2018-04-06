/*  IMoveble
 *  
 *  Descbybes object behavior 
 *  that can move
 */
 

namespace ConsoleTanks.Behavior
{
    interface IMoveble
    {
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        Directions GetCurrentDirection();
    }


    public enum Directions
    {
        Left,
        Right,
        Down,
        Up
    }

}
