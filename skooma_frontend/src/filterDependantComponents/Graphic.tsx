import type { JSX } from "react";
import { BarChart } from "@mui/x-charts/BarChart";
import { useEffect, useState } from "react";
import type { FilterData } from "../types";
import type { RocketLaunchData } from "../types";
import { dummyRocketLaunchData } from "../mock";

function Graphic({ filterDaten }: { filterDaten: FilterData }): JSX.Element {
  const [dataOfRocketLaunches, setDataOfRocketLaunches] = useState<
    RocketLaunchData[]
  >([
    { countLaunches: 0, moonphase: 1, countSuccessLaunches: 0 },
    { countLaunches: 0, moonphase: 2, countSuccessLaunches: 0 },
    { countLaunches: 0, moonphase: 3, countSuccessLaunches: 0 },
    { countLaunches: 0, moonphase: 4, countSuccessLaunches: 0 },
  ]);

  useEffect(() => {
    fetchData(filterDaten);
  }, []);

  function fetchData(filterDaten: FilterData): void {
    if (filterDaten) {
      // fetch data from backend
      var fetchedData: RocketLaunchData[] = dummyRocketLaunchData; // TODO replace with fetch data
      setDataOfRocketLaunches(fetchedData);
    }
  }

  const moonLabels = dataOfRocketLaunches.map((item) => {
    const phases: Record<number, string> = {
      1: "Neumond",
      2: "Viertel",
      3: "Halb",
      4: "Voll",
    };
    return phases[item.moonphase] || "Unbekannt";
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
