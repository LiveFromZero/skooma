import type { JSX } from "react";
import { BarChart } from "@mui/x-charts/BarChart";

function Graphic(): JSX.Element {
  return (
    <div>
      <h2>Graphic Component</h2>
      <BarChart
        xAxis={[
          {
            id: "barCategories",
            data: ["bar A", "bar B", "bar C"],
          },
        ]}
        series={[
          {
            data: [2, 5, 3],
          },
        ]}
        height={300}
      />
    </div>
  );
}
export default Graphic;
