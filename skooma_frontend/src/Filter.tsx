import { useState, type JSX } from "react";
import Graphic from "./filterDependantComponents/Graphic";
import SummaryText from "./filterDependantComponents/SummaryText";
import type { FilterData } from "./types";

const spanStyle: React.CSSProperties = {
  paddingLeft: 10,
  paddingRight: 10,
};

function Filter(): JSX.Element {
  const [selectedYear, setSelectedYear] = useState<string>("2020");
  const [selectedRocketType, setSelectedRocketType] = useState<string>("Type1");

  const filterDataToBeSendToBackend: FilterData = {
    year: selectedYear,
    rocketType: selectedRocketType,
  };

  return (
    <div>
      <label>
        Jahr:
        <select
          name="selectedYear"
          value={selectedYear}
          onChange={(e) => setSelectedYear(e.target.value)}
        >
          <option value="2020">2020</option>
          <option value="2021">2021</option>
          <option value="2022">2022</option>
          <option value="2023">2023</option>
          <option value="2024">2024</option>
          <option value="2025">2025</option>
        </select>
      </label>
      <span style={spanStyle}> </span>
      <label>
        Raketentyp:
        <select
          name="selectedRocketType"
          value={selectedRocketType}
          onChange={(e) => setSelectedRocketType(e.target.value)}
        >
          <option value="Type1">Type1</option>
          <option value="Type2">Type2</option>
          <option value="Type3">Type3</option>
        </select>
      </label>
      <div style={spanStyle}> </div>
      <Graphic filterDaten={filterDataToBeSendToBackend} />
      <SummaryText selectedYear={filterDataToBeSendToBackend.year} />
    </div>
  );
}

export default Filter;
