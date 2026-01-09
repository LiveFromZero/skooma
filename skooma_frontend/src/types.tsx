export interface FilterData {
  year: string;
  rocketType: string;
}

export interface SummaryTextData {
  selectedYear?: string;
  averagePercent?: number;
}

export interface RocketLaunchData {
  countLaunches: number;
  moonphase: number;
  countSuccessLaunches: number;
}
