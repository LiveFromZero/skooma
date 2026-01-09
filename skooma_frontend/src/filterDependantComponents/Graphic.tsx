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

  return (
    <div>
      <h2>Statistik</h2>
      <BarChart
        sx={{ alignSelf: "flex-start" }}
        xAxis={[{ data: ["Neumond", "Viertelmond", "Halbmond", "Vollmond"] }]}
        series={[
          {
            data: [
              dataOfRocketLaunches[0].countLaunches,
              dataOfRocketLaunches[1].countLaunches,
              dataOfRocketLaunches[2].countLaunches,
              dataOfRocketLaunches[3].countLaunches,
            ],
            label: "Anzahl Starts",
          },
          {
            data: [
              dataOfRocketLaunches[0].countSuccessLaunches,
              dataOfRocketLaunches[1].countSuccessLaunches,
              dataOfRocketLaunches[2].countSuccessLaunches,
              dataOfRocketLaunches[3].countSuccessLaunches,
            ],
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
