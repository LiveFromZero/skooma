import type { ChartTypeResponse } from "../RequestModels/types";

export const fetchChartData = async (
  baseUrl: string,
  type: string,
  year: number
): Promise<any> => {
  const url = new URL(`${baseUrl}/api/chart`);
  url.searchParams.append("type", type);
  url.searchParams.append("year", year.toString());

  try {
    const response = await fetch(url.toString());
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching chart data:", error);
    throw error;
  }
};

export const fetchChartTypes = async (
  baseUrl: string
): Promise<Array<ChartTypeResponse>> => {
  const url = new URL(`${baseUrl}/api/chart/types`);    
    try {
        const response = await fetch(url.toString());
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error fetching chart types:", error);
        throw error;
    }
};

