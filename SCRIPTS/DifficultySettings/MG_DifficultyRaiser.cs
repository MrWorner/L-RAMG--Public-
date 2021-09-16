////////////////////////////////////////////////////////////////////////////////
//
//	MG_DifficultyRaiser.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    public static class MG_DifficultyRaiser
    {
        #region Fields
        public static int _min_Bodyguards = 0;
        public static int _max_Bodyguards = 2;

        //public static int _min_Watchers = 0;
        public static int _max_Watchers = 0;

        public static int _watchersProbabilityPercentage = 0;

        //public static int _min_Backup = 1;
        public static int _max_Backup = 1;

        public static int _LIMIT_Bodyguards { get; set; } = 25;
        public static int _LIMIT_WatchersSquad { get; set; } = 3;
        public static int _LIMIT_WatchersProbabilityPercentage { get; set; } = 25;
        public static int _LIMIT_BackupForceSquad { get; set; } = 5;


        //По умолчанию:
        //Normal - доступен сразу
        //GangMember - после 5 убийств
        //Assasin - после 10 убийств
        //Police - после 15 убийств
        //Hacker - после 20 убийств
        //Cartel - после 25 убийств
        //Terrorist - после 30 убийств
        //Military - после 35 убийств
        public static bool _UNLOCK_ALL_TYPES { get; set; } = false;
        public static int _KILLS_NEEDED_TO_UNLOCK_GANGMEMBER { get; set; } = 5;
        public static int _KILLS_NEEDED_TO_UNLOCK_ASSASIN { get; set; } = 10;
        public static int _KILLS_NEEDED_TO_UNLOCK_POLICE { get; set; } = 15;
        public static int _KILLS_NEEDED_TO_UNLOCK_HACKER { get; set; } = 20;
        public static int _KILLS_NEEDED_TO_UNLOCK_CARTEL { get; set; } = 25;
        public static int _KILLS_NEEDED_TO_UNLOCK_TERRORIST { get; set; } = 30;
        public static int _KILLS_NEEDED_TO_UNLOCK_MILITARY { get; set; } = 35;
        #endregion Fields

        #region Properties
        #endregion Properties

        #region Constructor
        #endregion Constructor

        #region OnTick
        #endregion OnTick

        #region OnKeyDown
        #endregion OnKeyDown

        #region Public Methods

        public static int GenerateNumberBodyguards()
        {
            //2.Бодигуард MAX = 25
            //В начале: min = 0 max = 2
            //формула увеличения: max увеличивается с 2 каждым убийством. Копы, военные только максимум их может быть 15


            int success = MG_Statistic.TotalTargetsEliminated;

            if (success > 1)
            {
                decimal max = (success / 2);
                _max_Bodyguards = 2 + (int)Math.Floor(max);
            }


            //if (success > 4)
            //{
            //    decimal min = (success / 5);
            //    _min_Bodyguards = (int)Math.Floor(min);
            //}

            //if (success > 9)
            //{             
            //    decimal min = (success / 5);
            //    _min_Bodyguards = (int)Math.Floor(min);
            //    //_WatchersProbabilityPercentage
            //}

            if (_max_Bodyguards > _LIMIT_Bodyguards)
            {
                _max_Bodyguards = _LIMIT_Bodyguards;
            }

            if (_min_Bodyguards > _LIMIT_Bodyguards - 1)
            {
                _min_Bodyguards = _LIMIT_Bodyguards - 1;
            }

            int result = MG_Random.Random(_min_Bodyguards, _max_Bodyguards + 1);

            TargetType type = MG_Target.Type;

            //ДОПОЛНИТЕЛЬНЫЕ БОНУСНЫЕ!
            if (type.Equals(TargetType.GangMember))
            {
                result += 5;
            }
            else if (type.Equals(TargetType.Cartel))
            {
                result += 10;
            }
            else if (type.Equals(TargetType.Military))
            {
                result += 12;
            }
            else if (type.Equals(TargetType.Police))
            {
                result += 2;
            }
            //ДОПОЛНИТЕЛЬНЫЕ БОНУСНЫЕ!

            if (result > _LIMIT_Bodyguards)
            {
                result = _LIMIT_Bodyguards;
            }


            return result;
        }

        public static int GenerateNumberWatcherSquads()
        {
            int success = MG_Statistic.TotalTargetsEliminated;

            //if (success > 29)
            //{
            //    decimal min = (success / 30);
            //    _min_Watchers = (int)Math.Floor(min);
            //}
            if (success > 14)
            {
                decimal max = (success / 15);
                _max_Watchers = (int)Math.Floor(max);
            }




            if (_max_Watchers > _LIMIT_WatchersSquad)
            {
                _max_Watchers = _LIMIT_WatchersSquad;
            }

            int totalWillBeSpawned;

            //if (_min_Watchers > _LIMIT_WatchersSquad)
            //{
            //    _min_Watchers = _LIMIT_WatchersSquad;
            //    totalWillBeSpawned = _LIMIT_WatchersSquad;
            //}
            //else
            //{
            //    if (_min_Watchers > _max_Watchers)
            //    {
            //        _max_Watchers = _min_Watchers;
            //    }
            //    totalWillBeSpawned = MG_Random.Random(1, _max_Watchers + 1);
            //}
            totalWillBeSpawned = MG_Random.Random(1, _max_Watchers + 1);

            if (success > 9)
            {
                if (_max_Watchers == 0) _max_Watchers = 1;

                _watchersProbabilityPercentage = 5;
                if (success > 12)
                {
                    decimal add = ((success - 9) / 3);
                    _watchersProbabilityPercentage += (int)Math.Floor(add);
                }
            }

            if (_watchersProbabilityPercentage > _LIMIT_WatchersProbabilityPercentage)
            {
                _watchersProbabilityPercentage = _LIMIT_WatchersProbabilityPercentage;
            }

            if (MG_Random.Random(100) <= _watchersProbabilityPercentage)
            {
                return totalWillBeSpawned;
            }
            else
            {
                return 0;
            }
        }

        public static int GenerateNumberBackupForceSquad()
        {
            //По умолчанию 1. Максимум 3. Max увеличивается с каждым 15 убийством. 

            //public static int _max_Backup = 1;
            //public static int _LIMIT_Backup = 3;

            int success = MG_Statistic.TotalTargetsEliminated;

            //if (success > 29)
            //{
            //    decimal min = (success / 30);
            //    _min_Backup = 1 + (int)Math.Floor(min);
            //}

            if (success > 14)
            {
                decimal max = (success / 15);
                _max_Backup = 1 + (int)Math.Floor(max);
            }

            if (_LIMIT_BackupForceSquad < _max_Backup)
            {
                _max_Backup = _LIMIT_BackupForceSquad;
            }

            //if (_LIMIT_BackupForceSquad < _min_Backup)
            //{
            //    _min_Backup = _LIMIT_BackupForceSquad;
            //}

            //if (_min_Backup == _max_Backup)
            //{
            //    return _min_Backup;
            // }
            //else
            //{
            return MG_Random.Random(1, _max_Backup + 1);
            //}
        }

        public static TargetType GenerateTargetType()
        {
            //По умолчанию:
            //Normal - доступен сразу
            //GangMember - после 5 убийств
            //Assasin - после 10 убийств
            //Police - после 15 убийств
            //Hacker - после 20 убийств
            //Cartel - после 25 убийств
            //Terrorist - после 30 убийств
            //Military - после 35 убийств

            int success = MG_Statistic.TotalTargetsEliminated;
            List<TargetType> listOfTargetType = Enum.GetValues(typeof(TargetType)).Cast<TargetType>().ToList();

            if (_UNLOCK_ALL_TYPES == false)
            {
                foreach (var item in listOfTargetType.ToArray())
                {
                    switch (item)
                    {
                        case TargetType.Normal:
                            for (int i = 0; i < 2; i++)
                            {
                                listOfTargetType.Add(TargetType.Normal);
                            }
                            break;
                        case TargetType.Assasin:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_ASSASIN) listOfTargetType.Remove(item);
                            break;
                        case TargetType.Terrorist:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_TERRORIST) listOfTargetType.Remove(item);
                            break;
                        case TargetType.Police:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_POLICE) listOfTargetType.Remove(item);
                            break;
                        case TargetType.Cartel:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_CARTEL) listOfTargetType.Remove(item);
                            break;
                        case TargetType.Hacker:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_HACKER) listOfTargetType.Remove(item);
                            break;
                        case TargetType.Military:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_MILITARY) listOfTargetType.Remove(item);
                            break;
                        case TargetType.GangMember:
                            if (success < _KILLS_NEEDED_TO_UNLOCK_GANGMEMBER) listOfTargetType.Remove(item);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    listOfTargetType.Add(TargetType.Normal);
                }
            }
            TargetType chosen = listOfTargetType[MG_Random.Random(listOfTargetType.Count)];
            return chosen;
        }

        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

    }
}
