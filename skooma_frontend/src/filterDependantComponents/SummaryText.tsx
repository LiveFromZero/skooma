import { useEffect, useState, type JSX } from "react";

const dummyData: number = 76.5;

function SummaryText({ selectedYear }: { selectedYear: string }): JSX.Element {
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
      var fetchedData: number = dummyData; // in production, replace with fetched data
      // here is the information from the backend set to refresh the summary text
      setAllAveragePercent(fetchedData);
    }
  }

  return (
    <div>
      <p>
        Im Jahr {selectedYear} waren durchschnittlich {allAveragePercent}% aller
        Rakenestarts erfolgreich.
      </p>
    </div>
  );
}

export default SummaryText;
