export interface FilterData {
  year: string;
  rocketType: string;
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

export interface Option {
  value: string;
  label: string;
}

export interface DynamicFilterType {
  yearOptions: Option[];
  rocketTypeOptions: Option[];
}
