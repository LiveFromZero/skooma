import type { JSX } from "react";
import { BarChart } from "@mui/x-charts/BarChart";
import { useState } from "react";

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

function Graphic(): JSX.Element {
  const [dataOfRocketLaunches, setDataOfRocketLaunches] = useState<
    RocketLaunchData[]
  >([dummyData[0], dummyData[1], dummyData[2], dummyData[3]]);

  return (
    <div>
      <h2>Graphic Component</h2>
      <BarChart
        xAxis={[
          {
            id: "barCategories",
            data: ["Neumond", "Viertelmond", "Halbmond", "Vollmond"],
          },
        ]}
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
        ]}
        height={300}
      />
    </div>
  );
}
export default Graphic;
