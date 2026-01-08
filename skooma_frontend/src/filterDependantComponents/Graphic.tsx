import type { JSX } from "react";
import { BarChart } from "@mui/x-charts/BarChart";
import { useEffect, useState } from "react";
import type { FilterData } from "../Filter";

interface RocketLaunchData {
  countLaunches: number;
  moonphase: number;
  countSuccessLaunches: number;
}

const dummyData: RocketLaunchData[] = [
  { countLaunches: 5, moonphase: 1, countSuccessLaunches: 4 },
  { countLaunches: 3, moonphase: 2, countSuccessLaunches: 2 },
  { countLaunches: 7, moonphase: 3, countSuccessLaunches: 6 },
  { countLaunches: 2, moonphase: 4, countSuccessLaunches: 1 },
];

function Graphic({ filterDaten }: { filterDaten: FilterData }): JSX.Element {
  // In a real application, you would fetch data based on year and rocketType
  // and setDataOfRocketLaunches accordingly.
  const [dataOfRocketLaunches, setDataOfRocketLaunches] = useState<
    RocketLaunchData[]
  >([dummyData[0], dummyData[1], dummyData[2], dummyData[3]]);

  useEffect(() => {
    fetchData(filterDaten);
  }, []);

  function fetchData(filterDaten: FilterData): RocketLaunchData[] {
    // Simulate data fetching based on filters
    // filterDaten get send as parameters to backend in real application

    // Dummy implementation:
    if (!filterDaten.year && !filterDaten.rocketType) {
      return dataOfRocketLaunches;
    } else {
      // Here you would normally fetch data from the backend using filterDaten
      // setting the fetched data to state
      var fetchedData: RocketLaunchData[] = dummyData; // in production, replace with fetched data
      // here is the information from the backend set to refresh the graphic
      setDataOfRocketLaunches(fetchedData);
      return fetchedData;
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
