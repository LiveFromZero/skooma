import { useEffect, useState, type JSX } from "react";

interface SummaryTextData {
  selectedYear?: string;
  averagePercent?: number;
}

const dummyTextData: SummaryTextData = {
  selectedYear: "2024",
  averagePercent: 75.6,
};

function SummaryText({ selectedYear }: { selectedYear: string }): JSX.Element {
  const [chosenYear, setChosenYear] = useState<string>();
  const [allAveragePercent, setAllAveragePercent] = useState<number>();

  useEffect(() => {
    fetchDataFromBackend(selectedYear);
  }, []);

  function fetchDataFromBackend(selectedYear: string): void {
    // Simulate fetching summary data from backend
    // In a real application, you would use chosenYear to fetch the percentage
    if (selectedYear) {
      // Here you would normally fetch data from the backend using selectedYear
      // setting the fetched data to state
      var fetchedData: SummaryTextData = dummyTextData; // in production, replace with fetched data
      // here is the information from the backend set to refresh the summary text
      setChosenYear(fetchedData.selectedYear);
      setAllAveragePercent(fetchedData.averagePercent);
    }
  }

  return (
    <div>
      <p>
        Im Jahr {chosenYear} waren durchschnittlich {allAveragePercent}% aller
        Rakenestarts erfolgreich.
      </p>
    </div>
  );
}

export default SummaryText;
