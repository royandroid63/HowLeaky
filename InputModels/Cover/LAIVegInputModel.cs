﻿using HowLeaky.CustomAttributes;
using HowLeaky.Tools.DataObjects;
using HowLeaky.XmlObjects;
using System.Xml.Serialization;

namespace HowLeaky.DataModels
{
    public enum EPlantingRules { PlantInWindow, FixedAnualPlaning, PlantFromSequenceFile };

    public class PlantingFormat : IndexData
    {
        public DayMonthData StartPlantWindow { get; set; }
        public DayMonthData EndPlantWindow { get; set; }
        public DayMonthData PlantDate { get; set; }
        public StateData ForcePlanting { get; set; }
        public StateData MultiPlantInWindow { get; set; }
        public RotationOptions RotationOptions { get; set; }
        public Sequence PlantingDates { get; set; }
        public FallowSwitch FallowSwitch { get; set; }
        public RainfallSwitch RainfallSwitch { get; set; }
        public SoilWaterSwitch SoilWaterSwitch { get; set; }
        public RatoonCrop RatoonCrop { get; set; }

        public PlantingFormat() { }
    }

    public class RotationOptions : IndexData
    {
        public int MinContinuousRotations { get; set; }
        public int MaxContinuousRotations { get; set; }
        public int MinYearsBetweenSowing { get; set; }

        public RotationOptions() { }
    }

    public class FallowSwitch : StateData
    {
        public int MinFallowLength { get; set; }

        public FallowSwitch() { }
    }

    public class RainfallSwitch : StateData
    {
        public double PlantingRain { get; set; }
        public int DaysToTotalRain { get; set; }
        public int SowingDelay { get; set; }

        public RainfallSwitch() { }
    }

    public class SoilWaterSwitch : StateData
    {
        public double MinSoilWaterRatio { get; set; }
        public double MaxSoilWaterRatio { get; set; }
        public double AvailSWAtPlanting { get; set; }
        public double SoilDepthToSumPlantingSW { get; set; }

        public SoilWaterSwitch() { }
    }

    public class RatoonCrop : StateData
    {
        public int RatoonCount { get; set; }
        public double RatoonScaleFactor { get; set; }

        public RatoonCrop() { }

    }
    public class Waterlogging : StateData
    {
        public double WaterLoggingFactor1 { get; set; }
        public double WaterLoggingFactor2 { get; set; }

        public Waterlogging() { }
    }

    [XmlRoot("VegetationType")]
    public class LAIVegInputModel : VegInputModel
    {
        //Input Parameters
        public double PotMaxLAI { get; set; }                   // The upper limit of the leaf area index (LAI) - development curve.
        public double PropGrowSeaForMaxLai { get; set; }        // The development stage for potential maximum LAI.
        public double BiomassAtFullCover { get; set; }          // The amount of dry plant residues (ie stubble, pasture litter etc) that results in complete coverage of the ground.  This parameter controls the relationship between the amount of crop residue and cover, which is used in calculating runoff and erosion.
        [Unit("mm_per_day")]
        public double DailyRootGrowth { get; set; }             // The daily increment in root depth.
        public double PropGDDEnd { get; set; }                  // Set the proportion of the growth cycle for which irrigation is possible.
        public double DaysOfStressToDeath { get; set; }         // The number of consecutive days that water supply is less than threshold before the crop is killed.
        public double PercentOfMaxLai1 { get; set; }            // Percent of Maximum LAI for the 1st development stage.
        public double PercentOfGrowSeason1 { get; set; }        // The development stage for the 1st LAI "point".
        public double PercentOfMaxLai2 { get; set; }            // Percent of Maximum LAI for the 2nd development stage.
        public double PercentOfGrowSeason2 { get; set; }        // The development stage for the 2nd LAI "point".
        [Unit("days")]
        public double DegreeDaysPlantToHarvest { get; set; }    // The sum of degree-days (temperature less the base temperature) between planting and harvest.  Controls the rate of crop development and the potential duration of the crop. Some plants develop to maturity and harvest more slowly than others - these accumulate more degree-days between plant and harvest.
        public double SenesenceCoef { get; set; }               // Rate of LAI decline after max LAI.
        public double RadUseEffic { get; set; }                 // Biomass production per unit of radiation.
        public double HarvestIndex { get; set; }                // The grain biomass (kg/ha) divided by the above-ground biomass at flowering (kg/ha)
        [Unit("oC")]
        public double BaseTemp { get; set; }                    // The lower limit of plant development and growth, with respect to temperature (the average day temperature, degrees Celsius). The base temperature of vegetation is dependent on the type of environment in which the plant has evolved, and any breeding for hot or cold conditions.
        [Unit("oC")]
        public double OptTemp { get; set; }                     // The temperature for maximum biomass production.  Biomass production is a linear function of temperature between the Base temperature and the Optimum temperature.
        [Unit("mm")]
        public double MaxRootDepth { get; set; }                // located in CustomVegObject - >The maximum depth of the roots from the soil surface.  For the LAI model, the model calculates daily root growth from the root depth increase parameter
        public double SWPropForNoStress { get; set; }           // Ratio of water supply to potential water supply that indicates a stress day
        public double MaxResidueLoss { get; set; }              //Decomposition Rate

        public DayMonthData PlantDate { get; set; }
        public PlantingFormat PlantingFormat { get; set; }
        public Waterlogging Waterlogging { get; set; }

        //Getters
        public int PlantingRulesOptions { get { return PlantingFormat.index; } }
        public DayMonthData PlantingWindowStartDate { get { return PlantingFormat.StartPlantWindow; } }
        public DayMonthData PlantingWindowEndDate { get { return PlantingFormat.EndPlantWindow; } }
        public bool ForcePlantingAtEndOfWindow { get { return PlantingFormat.ForcePlanting.State; } }
        public bool MultiPlantInWindow { get { return PlantingFormat.MultiPlantInWindow.State; } }
        public int RotationFormat { get { return PlantingFormat.RotationOptions.index; } }
        public int MinRotationCount { get { return PlantingFormat.RotationOptions.MinContinuousRotations; } }
        public int MaxRotationCount { get { return PlantingFormat.RotationOptions.MaxContinuousRotations; } }
        public int RestPeriodAfterChangingCrops { get { return PlantingFormat.RotationOptions.MinYearsBetweenSowing; } }
        public bool FallowSwitch { get { return PlantingFormat.FallowSwitch.State; } }
        [Unit("days")]
        public int MinimumFallowPeriod { get { return PlantingFormat.FallowSwitch.MinFallowLength; } }
        public bool PlantingRainSwitch { get { return PlantingFormat.RainfallSwitch.State; } }
        [Unit("mm")]
        public double RainfallPlantingThreshold { get { return PlantingFormat.RainfallSwitch.PlantingRain; } }
        [Unit("days")]
        public int RainfallSummationDays { get { return PlantingFormat.RainfallSwitch.DaysToTotalRain; } }
        public bool SoilWaterSwitch { get { return PlantingFormat.SoilWaterSwitch.State; } }
        [Unit("mm")]
        public double MinSoilWaterTopLayer { get { return PlantingFormat.SoilWaterSwitch.MinSoilWaterRatio; } }
        [Unit("mm")]
        public double MaxSoilWaterTopLayer { get { return PlantingFormat.SoilWaterSwitch.MaxSoilWaterRatio; } }
        public double SoilWaterReqToPlant { get { return PlantingFormat.SoilWaterSwitch.AvailSWAtPlanting; } }
        [Unit("mm")]
        public double DepthToSumPlantingWater { get { return PlantingFormat.SoilWaterSwitch.SoilDepthToSumPlantingSW; } set { } }
        public int SowingDelay { get { return PlantingFormat.RainfallSwitch.SowingDelay; } }
        public Sequence PlantingSequence { get { return PlantingFormat.PlantingDates; } }                           // The rate of removal of plant residues from the soil surface by decay. Fraction of current plant/crop residues that decay each day. Plant residues on the soil surface are used in calculation of soil evaporation, runoff and erosion.
        public bool WaterLoggingSwitch { get { return Waterlogging.State; } }
        public double WaterLoggingFactor1 { get { return Waterlogging.WaterLoggingFactor1; } }
        public double WaterLoggingFactor2 { get { return Waterlogging.WaterLoggingFactor2; } }
        public bool RatoonSwitch { get { return PlantingFormat.RatoonCrop.State; } }
        public int NumberOfRatoons { get { return PlantingFormat.RatoonCrop.RatoonCount; } }
        public double ScalingFactorForRatoons { get { return PlantingFormat.RatoonCrop.RatoonScaleFactor; } }

        //TODO: unmatched
        //MaxResidueLoss, WatStressForDeath
        public double MaximumResidueCover { get; set; }

        public LAIVegInputModel() { }
    }
}
