/*  PlayerController
 *  
 *  Prodive identifying of each one of palyers
 *  and add control of actions
 */



using System;
using System.Collections.Generic;
using System.Timers;

namespace ConsoleTanks.Controllers
{
    class PlayerController
    {
        private static int GUID = 0;

        private int id = 0;

        private static List<ConsoleKey> globalReservedKeys = new List<ConsoleKey>();

        public ConsoleTanks.Models.Objective.Tank Tank { get;  set; }

        public ConsoleKey Foreward { get; private set; }
        public ConsoleKey Backward { get; private set; }
        public ConsoleKey Right { get; private set; }
        public ConsoleKey Left { get; private set; }
        public ConsoleKey Shout { get; private set; }

        public event EventHandler<KeyPressedState> KeyPressed = null;

        public PlayerController(ConsoleKey foreward, ConsoleKey backward, ConsoleKey right, ConsoleKey left, ConsoleKey shout)
        {
            //  chech if the key isnt reserved yet
            foreach(var key in globalReservedKeys)
            {
                if(key.Equals(foreward) || key.Equals(backward) || key.Equals(right) || key.Equals(left) || key.Equals(shout))
                {
                    throw new Exceptions.PlayerControllerException("The key is already reserved");
                }

            }

            

            //  if we are here 
            //  the keys dont reserved and we doesnt
            //  throw an error

            //  add to local reserved keys
            Foreward = foreward;
            Backward = backward;
            Right = right;
            Left = left;
            Shout = shout;

            //  add to global reserved key
            globalReservedKeys.Add(foreward);
            globalReservedKeys.Add(backward);
            globalReservedKeys.Add(right);
            globalReservedKeys.Add(left);
            globalReservedKeys.Add(shout);

            this.id = PlayerController.GUID;
            PlayerController.GUID++;

        }

        public int GetID()
        {
            return this.id;
        }

        public void Run()
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                int duration = 0;
                ConsoleKey previous = new ConsoleKey();
                ConsoleKey current = new ConsoleKey();

                while (true)
                {
                    duration = 0;

                    previous = Console.ReadKey(true).Key;

                    while(EqualsKey((Console.ReadKey(true).Key)))
                    {
                        current = Console.ReadKey(true).Key;

                        if (current == previous)
                        {
                            if(duration >= 3)
                            {
                                CallKeyPressed(current);
                                duration = 0;
                                break;
                            }
                            else
                            {
                                duration++;
                            }
                        }
                        
                    }


                }
            });

        }

        private bool EqualsKey(ConsoleKey key)
        {
            return ((key == Foreward) || (key == Backward)
                           || (key == Left) || (key == Right)
                           || (key == Shout));

        }

        private void CallKeyPressed(ConsoleKey key)
        {

            if (key == Foreward)
            {
                KeyPressed(Tank, KeyPressedState.Foreward);
                return;
            }
            if (key == Backward)
            {
                KeyPressed(Tank, KeyPressedState.Backward);
                return;
            }
            if (key == Left)
            {
                KeyPressed(Tank, KeyPressedState.Left);
                return;
            }
            if (key == Right)
            {
                KeyPressed(Tank, KeyPressedState.Right);
                return;
            }
            if (key == Shout)
            {
                KeyPressed(Tank, KeyPressedState.Shout);
                return;
            }

        }


        //  remove local keys from global reserved keys
        ~PlayerController()
        {
            globalReservedKeys.Remove(Foreward);
            globalReservedKeys.Remove(Backward);
            globalReservedKeys.Remove(Right);
            globalReservedKeys.Remove(Left);
            globalReservedKeys.Remove(Shout);
        }



    }

    public enum KeyPressedState
    {
        Foreward,
        Backward,
        Left,
        Right,
        Shout
    }

}
