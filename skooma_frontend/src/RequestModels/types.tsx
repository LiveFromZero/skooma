export interface FilterData {
  year: number;
  chartType: string;
}

export interface RocketLaunchData {
  countLaunches: number;
  moonPhase: number;
  countSuccessLaunches: number;
}

export interface FilterSelectProps {
  label: string;
  value: string;
  options: Array<{ value: string; label: string }>;
  onChange: Function;
}

// Interfaces for Backend Models

export interface Launch {
    id: string;
    rocketName: string;
    launchDate: string; // ISO string format
    location: Location;
    status: string;
}

export interface Location {
    id: number;
    countryName: string;
    latitude?: string;
    longitude?: string;
}

export interface LaunchesPerMonthSummary {
    totalLaunches: number;
    totalSuccessful: number;
    totalFailed: number;
    overallSuccessRate: number;
}

export interface LaunchWithMoonPhase {
    launch: Launch;
    moonPhase: MoonPhases;
}

export interface MoonData {
    id: string;
    phase: MoonPhases;
    date: string; // ISO string format
}

export interface MonthData {
    month: number;
    monthName: string;
    totalLaunches: number;
    successfulLaunches: number;
    failedLaunches: number;
    successRate: number;
}

export interface MoonPhaseData {
    moonPhase: string;
    totalLaunches: number;
    successfulLaunches: number;
    failedLaunches: number;
    successRate: number;
}

export interface SuccessRateByMoonPhaseResponse {
    chartType: string;
    year: number;
    data: MoonPhaseData[];
    summary: SuccessRateSummary;
}

export interface LaunchesPerMonthResponse {
    chartType: string;
    year: number;
    data: MonthData[];
    summary: LaunchesPerMonthSummary;
}

export interface YearSummaryResponse {
    year: number;
    totalLaunches: number;
    successfulLaunches: number;
    failedLaunches: number;
    overallSuccessRate: number;
    launchesWithMoonData: number;
    uniqueLocations: number;
}

export interface SuccessRateSummary {
    totalAnalyzedLaunches: number;
    totalLaunches: number;
    launchesWithoutMoonData: number;
}

export interface ChartTypeResponse {
    chartType: string;
    description: string;
}

export const chartTypes = {
    SuccessRateByMoonPhase: "successRate",
    LaunchesPerMonth: "launchesPerMonth"
} as const; 

export const moonPhases = {
    Neumond: "Neumond",
    ZunehmenderHalbmond: "Zunehmender Halbmond",
    Vollmond: "Vollmond",
    AbnehmenderHalbmond: "Abnehmender Halbmond"
} as const;

export type MoonPhases = keyof typeof moonPhases;
