import type { JSX } from "react";
import { BarChart } from "@mui/x-charts/BarChart";
import { useEffect, useState } from "react";
import type { FilterData } from "../types";
import type { RocketLaunchData } from "../types";

function Graphic({ filterDaten }: { filterDaten: FilterData }): JSX.Element {
  const [dataOfRocketLaunches, setDataOfRocketLaunches] = useState<
    RocketLaunchData[]
  >([
    { countLaunches: 0, moonPhase: 1, countSuccessLaunches: 0 },
    { countLaunches: 0, moonPhase: 2, countSuccessLaunches: 0 },
    { countLaunches: 0, moonPhase: 3, countSuccessLaunches: 0 },
    { countLaunches: 0, moonPhase: 4, countSuccessLaunches: 0 },
  ]);

  useEffect(() => {
    fetchData(filterDaten);
  }, [filterDaten.year, filterDaten.rocketType]);

  async function fetchData(filterDaten: FilterData): Promise<void> {
    const response = await fetch(
      "http://localhost:5000/rocket-starts/rocketlaunchdata?year=" +
        filterDaten.year +
        "&rocketType=" +
        filterDaten.rocketType
    );
    console.log("Response status:", response.status);
    const data = await response.json();
    console.log("Fetched data:", data);
    var fetchedData: RocketLaunchData[] = data;
    setDataOfRocketLaunches(fetchedData);
  }

  const moonLabels = dataOfRocketLaunches.map((item) => {
    const phases: Record<number, string> = {
      1: "Neumond",
      2: "Viertelmond",
      3: "Halbmond",
      4: "Vollmond",
    };
    return phases[item.moonPhase] || "Unbekannt";
  });

  const seriesDataAllLaunches: number[] = dataOfRocketLaunches.map(
    (item) => item.countLaunches
  );

  const seriesDataSuccessfulLaunches: number[] = dataOfRocketLaunches.map(
    (item) => item.countSuccessLaunches
  );

  return (
    <div>
      <h2>Statistik</h2>
      <BarChart
        sx={{ alignSelf: "flex-start" }}
        xAxis={[
          {
            scaleType: "band",
            data: moonLabels,
          },
        ]}
        series={[
          {
            data: seriesDataAllLaunches,
            label: "Anzahl Starts",
          },
          {
            data: seriesDataSuccessfulLaunches,
            label: "Erfolgreiche Starts",
          },
        ]}
        height={300}
        width={1000}
      />
    </div>
  );
}
export default Graphic;
