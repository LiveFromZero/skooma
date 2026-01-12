import { useState, type JSX } from "react";
import Graphic from "./filterDependantComponents/Graphic";
import type { FilterData } from "./types";
import type { FilterSelectProps } from "./types";
import SummaryTextForGivenYear from "./filterDependantComponents/SummaryTextForGivenYear";
import { useEffect } from "react";
const API_URL = import.meta.env.VITE_API_URL_BACKEND;

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
    rocketType: "Type0",
  });

  useEffect(() => {
    fetchData();
  }, []);

  async function fetchData(): Promise<void> {
    const response = await fetch(`${API_URL}/rocket-starts/rocketfilterdata`);
    console.log("Response status:", response.status);
    const data = await response.json();
    console.log("Fetched data:", data);
    var fetchedData: FilterData = data;
    setDynamicFilterData(fetchedData);
  }

  const YEAR_OPTIONS = [
    { value: "2020", label: "2020" },
    { value: "2021", label: "2021" },
    { value: "2022", label: "2022" },
    { value: "2023", label: "2023" },
    { value: "2024", label: "2024" },
  ];

  const ROCKET_OPTIONS = [
    { value: "Type0", label: "Alle Typen" },
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
        value={dynamicFilterData.rocketType}
        options={ROCKET_OPTIONS}
        onChange={(value: string) =>
          setDynamicFilterData({ ...dynamicFilterData, rocketType: value })
        }
      />
      <Graphic filterDaten={dynamicFilterData} />
      <SummaryTextForGivenYear selectedYear={dynamicFilterData.year} />
    </div>
  );
}

export default Filter;
