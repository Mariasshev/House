using System;
using System.Collections.Generic;

namespace BuildingHouse
{
    interface IPart
    {
        string Complete();
        bool IsCompleted();
    }

    interface IWorker
    {
        string Work(IPart part);
    }

    class House : IPart
    {
        private Basement basement = new Basement();
        private Walls[] walls;
        private Door door = new Door();
        private Window[] windows;
        private Roof roof = new Roof();

        public House()
        {
            walls = new Walls[4];
            windows = new Window[4];
            for (int i = 0; i < 4; i++)
            {
                walls[i] = new Walls();
            }
            for (int i = 0; i < 4; i++)
            {
                windows[i] = new Window();
            }
        }

        public string Complete()
        {
            string result = "Строительство дома:\n";
            result += basement.Complete() + "\n";
            foreach (var wall in walls)
                result += wall.Complete() + "\n";
            result += door.Complete() + "\n";
            foreach (var window in windows)
                result += window.Complete() + "\n";
            result += roof.Complete() + "\n";
            return result;
        }

        public bool IsCompleted()
        {
            if (!basement.IsCompleted())
            {
                return false;
            }

            foreach (var wall in walls)
            {
                if (!wall.IsCompleted())
                {
                    return false;
                }
            }

            if (!door.IsCompleted())
            {
                return false;
            }

            foreach (var window in windows)
            {
                if (!window.IsCompleted())
                {
                    return false;
                }
            }

            return roof.IsCompleted();
        }

    }

    class Basement : IPart
    {
        private bool completed = false;

        public string Complete()
        {
            if (!completed)
            {
                completed = true;
                return "Фундамент завершён.";
            }
            return "Фундамент уже построен.";
        }

        public bool IsCompleted() => completed;
    }

    class Walls : IPart
    {
        private bool completed = false;

        public string Complete()
        {
            if (!completed)
            {
                completed = true;
                return "Стена завершена.";
            }
            return "Стена уже построена.";
        }

        public bool IsCompleted() => completed;
    }

    class Door : IPart
    {
        private bool completed = false;

        public string Complete()
        {
            if (!completed)
            {
                completed = true;
                return "Дверь завершена.";
            }
            return "Дверь уже построена.";
        }

        public bool IsCompleted() => completed;
    }

    class Window : IPart
    {
        private bool completed = false;

        public string Complete()
        {
            if (!completed)
            {
                completed = true;
                return "Окно завершено.";
            }
            return "Окно уже построено.";
        }

        public bool IsCompleted() => completed;
    }

    class Roof : IPart
    {
        private bool completed = false;

        public string Complete()
        {
            if (!completed)
            {
                completed = true;
                return "Крыша завершена.";
            }
            return "Крыша уже построена.";
        }

        public bool IsCompleted() => completed;
    }

    class Worker : IWorker
    {
        public string Work(IPart part)
        {
            return part.Complete();
        }
    }

    class Team : IWorker
    {
        private Worker[] workers;

        public Team(int numberOfWorkers)
        {
            workers = new Worker[numberOfWorkers];
            for (int i = 0; i < numberOfWorkers; i++)
            {
                workers[i] = new Worker(); 
            }
        }

        public string Work(IPart part)
        {
            string report = "";
            foreach (var worker in workers)
            {
                report += worker.Work(part) + "\n";
            }
            return report;
        }
    }

    class TeamLeader : IWorker
    {
        public string Work(IPart part)
        {
            if (part.IsCompleted())
            {
                return $"{part.GetType().Name} завершён!";
            }
            return $"{part.GetType().Name} не завершён.";
        }

        public string Report(House house)
        {
            if (house.IsCompleted())
            {
                return "Дом завершён!";
            }
            return "Дом не завершён. Продолжайте работу.";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            House house = new House();
            Team team = new Team(3);
            TeamLeader leader = new TeamLeader();

            Console.WriteLine("Начало строительства:\n");

            Console.WriteLine(team.Work(house));
            Console.WriteLine(leader.Work(house)); 

            Console.WriteLine("Продолжаем строительство:\n");
            Console.WriteLine(team.Work(house)); 

            Console.ReadLine();
        }
    }
}
