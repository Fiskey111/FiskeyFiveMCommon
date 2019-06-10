﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

// ReSharper disable InconsistentNaming

namespace CommonClient.Objects
{
    public class Scenario : BaseScript
    {
        public string ShortName { get; set; }
        public ScenarioList ScenarioName { get; set; }

        private Task _task;
        
        public Scenario() { }

        public Scenario(string shortName, string scenarioName)
        {
            ShortName = shortName;
            ScenarioName = (ScenarioList) Enum.Parse(typeof(ScenarioList), scenarioName);
        }

        public Scenario(string shortName, ScenarioList scenarioName)
        {
            ShortName = shortName;
            ScenarioName = scenarioName;
        }
        
        public void Play(Entity ped, bool loop = false)
        {
            if (ped == null || !ped.Exists()) return;

            if (loop)
            {
                _task = new Task(() => Process(ped));
                _task.Start();
            }
            else
            {
                API.TaskStartScenarioInPlace(ped.Handle, ScenarioName.ToString(), 0, true);
            }

        }

        private async void Process(Entity ped)
        {
            while (true)
            {
                if (API.IsPedUsingAnyScenario(ped.Handle) 
                    || API.IsPedActiveInScenario(ped.Handle) 
                    || API.IsPedUsingScenario(ped.Handle, ScenarioName.ToString())) await Delay(0);

                Play(ped);
            }
        }

        public void Stop()
        {
           Game.PlayerPed.Task.ClearAll();
            _task?.Dispose();
        }

        public enum ScenarioList
        {
            WORLD_HUMAN_AA_COFFEE,
            WORLD_HUMAN_AA_SMOKE,
            WORLD_HUMAN_BINOCULARS,
            WORLD_HUMAN_BUM_FREEWAY,
            WORLD_HUMAN_BUM_SLUMPED,
            WORLD_HUMAN_BUM_STANDING,
            WORLD_HUMAN_BUM_WASH,
            WORLD_HUMAN_CAR_PARK_ATTENDANT,
            WORLD_HUMAN_CHEERING,
            WORLD_HUMAN_CLIPBOARD,
            WORLD_HUMAN_CONST_DRILL,
            WORLD_HUMAN_COP_IDLES,
            WORLD_HUMAN_DRINKING,
            WORLD_HUMAN_DRUG_DEALER,
            WORLD_HUMAN_DRUG_DEALER_HARD,
            WORLD_HUMAN_MOBILE_FILM_SHOCKING,
            WORLD_HUMAN_GARDENER_LEAF_BLOWER,
            WORLD_HUMAN_GARDENER_PLANT,
            WORLD_HUMAN_GOLF_PLAYER,
            WORLD_HUMAN_GUARD_PATROL,
            WORLD_HUMAN_GUARD_STAND,
            WORLD_HUMAN_GUARD_STAND_ARMY,
            WORLD_HUMAN_HAMMERING,
            WORLD_HUMAN_HANG_OUT_STREET,
            WORLD_HUMAN_HIKER_STANDING,
            WORLD_HUMAN_HUMAN_STATUE,
            WORLD_HUMAN_JANITOR,
            WORLD_HUMAN_JOG_STANDING,
            WORLD_HUMAN_LEANING,
            WORLD_HUMAN_MAID_CLEAN,
            WORLD_HUMAN_MUSCLE_FLEX,
            WORLD_HUMAN_MUSCLE_FREE_WEIGHTS,
            WORLD_HUMAN_MUSICIAN,
            WORLD_HUMAN_PAPARAZZI,
            WORLD_HUMAN_PARTYING,
            WORLD_HUMAN_PICNIC,
            WORLD_HUMAN_PROSTITUTE_HIGH_CLASS,
            WORLD_HUMAN_PROSTITUTE_LOW_CLASS,
            WORLD_HUMAN_PUSH_UPS,
            WORLD_HUMAN_SEAT_LEDGE,
            WORLD_HUMAN_SEAT_LEDGE_EATING,
            WORLD_HUMAN_SEAT_STEPS,
            WORLD_HUMAN_SEAT_WALL,
            WORLD_HUMAN_SEAT_WALL_EATING,
            WORLD_HUMAN_SEAT_WALL_TABLET,
            WORLD_HUMAN_SECURITY_SHINE_TORCH,
            WORLD_HUMAN_SIT_UPS,
            WORLD_HUMAN_SMOKING,
            WORLD_HUMAN_SMOKING_POT,
            WORLD_HUMAN_STAND_FIRE,
            WORLD_HUMAN_STAND_FISHING,
            WORLD_HUMAN_STAND_IMPATIENT,
            WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT,
            WORLD_HUMAN_STAND_MOBILE,
            WORLD_HUMAN_STAND_MOBILE_UPRIGHT,
            WORLD_HUMAN_STRIP_WATCH_STAND,
            WORLD_HUMAN_STUPOR,
            WORLD_HUMAN_SUNBATHE,
            WORLD_HUMAN_SUNBATHE_BACK,
            WORLD_HUMAN_SUPERHERO,
            WORLD_HUMAN_SWIMMING,
            WORLD_HUMAN_TENNIS_PLAYER,
            WORLD_HUMAN_TOURIST_MAP,
            WORLD_HUMAN_TOURIST_MOBILE,
            WORLD_HUMAN_VEHICLE_MECHANIC,
            WORLD_HUMAN_WELDING,
            WORLD_HUMAN_WINDOW_SHOP_BROWSE,
            WORLD_HUMAN_YOGA,
            WORLD_BOAR_GRAZING,
            WORLD_CAT_SLEEPING_GROUND,
            WORLD_CAT_SLEEPING_LEDGE,
            WORLD_COW_GRAZING,
            WORLD_COYOTE_HOWL,
            WORLD_COYOTE_REST,
            WORLD_COYOTE_WANDER,
            WORLD_CHICKENHAWK_FEEDING,
            WORLD_CHICKENHAWK_STANDING,
            WORLD_CORMORANT_STANDING,
            WORLD_CROW_FEEDING,
            WORLD_CROW_STANDING,
            WORLD_DEER_GRAZING,
            WORLD_DOG_BARKING_ROTTWEILER,
            WORLD_DOG_BARKING_RETRIEVER,
            WORLD_DOG_BARKING_SHEPHERD,
            WORLD_DOG_SITTING_ROTTWEILER,
            WORLD_DOG_SITTING_RETRIEVER,
            WORLD_DOG_SITTING_SHEPHERD,
            WORLD_DOG_BARKING_SMALL,
            WORLD_DOG_SITTING_SMALL,
            WORLD_FISH_IDLE,
            WORLD_GULL_FEEDING,
            WORLD_GULL_STANDING,
            WORLD_HEN_PECKING,
            WORLD_HEN_STANDING,
            WORLD_MOUNTAIN_LION_REST,
            WORLD_MOUNTAIN_LION_WANDER,
            WORLD_PIG_GRAZING,
            WORLD_PIGEON_FEEDING,
            WORLD_PIGEON_STANDING,
            WORLD_RABBIT_EATING,
            WORLD_RATS_EATING,
            WORLD_SHARK_SWIM,
            PROP_BIRD_IN_TREE,
            PROP_BIRD_TELEGRAPH_POLE,
            PROP_HUMAN_ATM,
            PROP_HUMAN_BBQ,
            PROP_HUMAN_BUM_BIN,
            PROP_HUMAN_BUM_SHOPPING_CART,
            PROP_HUMAN_MUSCLE_CHIN_UPS,
            PROP_HUMAN_MUSCLE_CHIN_UPS_ARMY,
            PROP_HUMAN_MUSCLE_CHIN_UPS_PRISON,
            PROP_HUMAN_PARKING_METER,
            PROP_HUMAN_SEAT_ARMCHAIR,
            PROP_HUMAN_SEAT_BAR,
            PROP_HUMAN_SEAT_BENCH,
            PROP_HUMAN_SEAT_BENCH_DRINK,
            PROP_HUMAN_SEAT_BENCH_DRINK_BEER,
            PROP_HUMAN_SEAT_BENCH_FOOD,
            PROP_HUMAN_SEAT_BUS_STOP_WAIT,
            PROP_HUMAN_SEAT_CHAIR,
            PROP_HUMAN_SEAT_CHAIR_DRINK,
            PROP_HUMAN_SEAT_CHAIR_DRINK_BEER,
            PROP_HUMAN_SEAT_CHAIR_FOOD,
            PROP_HUMAN_SEAT_CHAIR_UPRIGHT,
            PROP_HUMAN_SEAT_CHAIR_MP_PLAYER,
            PROP_HUMAN_SEAT_COMPUTER,
            PROP_HUMAN_SEAT_DECKCHAIR,
            PROP_HUMAN_SEAT_DECKCHAIR_DRINK,
            PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS,
            PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS_PRISON,
            PROP_HUMAN_SEAT_SEWING,
            PROP_HUMAN_SEAT_STRIP_WATCH,
            PROP_HUMAN_SEAT_SUNLOUNGER,
            PROP_HUMAN_STAND_IMPATIENT,
            CODE_HUMAN_COWER,
            CODE_HUMAN_CROSS_ROAD_WAIT,
            CODE_HUMAN_PARK_CAR,
            PROP_HUMAN_MOVIE_BULB,
            PROP_HUMAN_MOVIE_STUDIO_LIGHT,
            CODE_HUMAN_MEDIC_KNEEL,
            CODE_HUMAN_MEDIC_TEND_TO_DEAD,
            CODE_HUMAN_MEDIC_TIME_OF_DEATH,
            CODE_HUMAN_POLICE_CROWD_CONTROL,
            CODE_HUMAN_POLICE_INVESTIGATE,
            CODE_HUMAN_STAND_COWER,
            EAR_TO_TEXT,
            EAR_TO_TEXT_FAT
        }
    }
}
