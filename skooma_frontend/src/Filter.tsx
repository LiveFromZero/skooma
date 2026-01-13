import { useState, type JSX } from "react";
import Graphic from "./filterDependantComponents/Graphic";
import type { FilterData } from "./RequestModels/types";
import type { FilterSelectProps } from "./RequestModels/types";
import SummaryTextForGivenYear from "./filterDependantComponents/SummaryTextForGivenYear";

const spanStyle: React.CSSProperties = {
  paddingLeft: 10,
  paddingRight: 10,
};

function FilterSelect({
  label,
  value,
  options,
  onChange,
}: FilterSelectProps): JSX.Element {
  return (
    <div>
      <label>{label}</label>
      <select value={value} onChange={(e) => onChange(e.target.value)}>
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </select>
    </div>
  );
}

function Filter(): JSX.Element {
  const [dynamicFilterData, setDynamicFilterData] = useState<FilterData>({
    year: "2020",
    chartType: "Type0",
  });

  const YEAR_OPTIONS = [
    { value: "2020", label: "2020" },
    { value: "2021", label: "2021" },
    { value: "2022", label: "2022" },
    { value: "2023", label: "2023" },
    { value: "2024", label: "2024" },
  ];

  const CHART_OPTIONS = [
    { value: "successRate", label: "Erfolgsrate nach Mondphase" },
    { value: "Type1", label: "Type 1" },
    { value: "Type2", label: "Type 2" },
    { value: "Type3", label: "Type 3" },
  ];

  return (
    <div>
      <FilterSelect
        label="Startjahr wählen:"
        value={dynamicFilterData.year}
        options={YEAR_OPTIONS}
        onChange={(value: string) =>
          setDynamicFilterData({ ...dynamicFilterData, year: value })
        }
      />
      <span style={spanStyle} />
      <FilterSelect
        label="Raketentyp wählen:"
        value={dynamicFilterData.chartType}
        options={CHART_OPTIONS}
        onChange={(value: string) =>
          setDynamicFilterData({ ...dynamicFilterData, chartType: value })
        }
      />
      <Graphic filterDaten={dynamicFilterData} />
      <SummaryTextForGivenYear selectedYear={dynamicFilterData.year} />
    </div>
  );
}

export default Filter;
