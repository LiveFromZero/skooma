import type { JSX } from "react";
import { BarChart } from "@mui/x-charts/BarChart";
import { useEffect, useState } from "react";
import type { FilterData } from "../RequestModels/types";
import { fetchChartData, fetchChartTypes } from "../Services/fetchData";
const API_URL = import.meta.env.VITE_API_URL_BACKEND;

interface ChartData {
  label: string;
  value: number;
}

interface ChartType {
  id: string;
  name: string;
}

function Graphic({
  filterDaten,
  setFilterDaten,
}: {
  filterDaten: FilterData;
  setFilterDaten: (data: FilterData) => void;
}): JSX.Element {
  const [chartData, setChartData] = useState<ChartData[]>([]);
  const [chartTypes, setChartTypes] = useState<ChartType[]>([]);

  useEffect(() => {
    // Fetch available chart types on mount
    fetchAvailableChartTypes();
  }, []);

  useEffect(() => {
    // Fetch chart data whenever filterDaten changes
    fetchData(filterDaten);
  }, [filterDaten]);

  async function fetchAvailableChartTypes(): Promise<void> {
    try {
      const types = await fetchChartTypes(API_URL);
      console.log("Fetched chart types:", types);
      setChartTypes(types);

      // Automatically set the first chart type in the filter if not already set
      if (types.length > 0 && !filterDaten.chartType) {
        setFilterDaten({ ...filterDaten, chartType: types[0].id });
      }
    } catch (error) {
      console.error("Failed to fetch chart types:", error);
    }
  }

  async function fetchData(filterDaten: FilterData): Promise<void> {
    try {
      const data = await fetchChartData(
        API_URL,
        filterDaten.chartType,
        filterDaten.year
      );
      console.log("Fetched chart data:", data);

      // Transform the data to match the expected structure
      const transformedData = data.map((item: any) => ({
        label: item.name || item.label, // Use 'name' or fallback to 'label'
        value: item.value,
      }));

      setChartData(transformedData);
    } catch (error) {
      console.error("Failed to fetch chart data:", error);
    }
  }

  const labels = chartData.map((item) => item.label);
  const values = chartData.map((item) => item.value);

  return (
    <div>
      <h2>Statistik</h2>
      <BarChart
        sx={{ alignSelf: "flex-start" }}
        xAxis={[
          {
            scaleType: "band",
            data: labels,
          },
        ]}
        series={[
          {
            data: values,
            label: "Chart Values",
          },
        ]}
        height={300}
        width={1000}
      />
    </div>
  );
}
export default Graphic;
