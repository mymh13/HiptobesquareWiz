import { useState, useEffect } from 'react';
import './App.css'

// Backend API URL
const API_URL = "http://localhost:5000/squares";

const generateRandomColour = (prevColour) => {
    let colour;
    do {
        colour = `#${Math.floor(Math.random() * 16777215).toString(16).padStart(6, '0')}`.toUpperCase();
    } while (colour === prevColour); // Ensure it differs from the last one
    return colour;
};

function App() {
    const [squares, setSquares] = useState([]);

    // Log state updates every time squares change
    useEffect(() => {
        console.log("Updated squares state:", squares);
    }, [squares]);
    
    // Function to fetch squares on page load
    useEffect(() => {
        let ignore = false;
        fetch(API_URL)
            .then((response) => response.json())
            .then((data) => {
                if (!ignore) {
                    console.log("Fetched squares:", data);
                    setSquares(data);
                }
            })
            .catch((error) => console.error("Error fetching squares:", error));

        return () => { ignore = true; }; // Cleanup function to prevent multiple calls
    }, []);

    const calculateNextPosition = (squares) => {
        const count = squares.length;
        if (count === 0) return { x: 0, y: 0 }; // First square at (0,0)

        let level = Math.floor(Math.sqrt(count)); // Defines the level of the square
        let x, y;

        if (count < (level + 1) ** 2 - level) {
            // Fill horizontally first
            x = count - level ** 2;
            y = level;
        } else {
            // Then fill vertically downward
            x = level;
            y = count - ((level + 1) ** 2 - level);
        }

        console.log(`Calculated Position: x=${x}, y=${y}`);

        return { x, y };
    };

    const addSquare = async () => {
        const prevColour = squares.length > 0 ? squares[squares.length - 1].Colour : null;
        const { x, y } = calculateNextPosition(squares);

        const newSquare = {
            Id: `${x}-${y}`,
            Colour: generateRandomColour(prevColour),
            PositionX: x,
            PositionY: y,
        };

        console.log("Adding square:", newSquare);

        try {
            await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newSquare),
            });

            setSquares((prevSquares) => {
                const updatedSquares = [...prevSquares, newSquare];
                console.log("Updated squares state:", updatedSquares);
                return updatedSquares;
            });
            setSquares(squares => [...squares]);  // Force React re-render

        } catch (error) {
            console.error("Error saving square:", error);
        }
    };
    
    // Clear all squares
    const clearSquares = async () => {
        await fetch(API_URL, { method: "DELETE" });
        setSquares([]); // Update the squares state to remove all squares
    };

    console.log("Rendering squares:", squares.map(s => `Id: ${s.Id}, PosX: ${s.PositionX}, PosY: ${s.PositionY}`));

    return (
        <div className="container flex flex-col items-center min-h-screen px-6">
        {/* Top section, for top people */}
            <div className="w-full max-w-4xl text-center py-6 px-8 bg-[#292929] rounded-xl shadow-lg">
                <a
                    href="https://mymh.dev/"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-4xl font-bold transition-transform duration-200 
                    hover:scale-110 hover:text-[#00d0ff] hover:shadow-md mt-4 mb-6 block"
                >
                    hiptobesquare @ mymh.dev
                </a>

                {/* Buttons BUTTONS EVERYWHERE! */}
                <div className="mt-6 flex gap-8 justify-center border border-[#4ec9b0] px-4 py-4">
                    <button className="bg-[#1a1a1a] text-[#4ec9b0] px-10 py-4 rounded-xl font-bold shadow-lg text-2xl
                    hover:bg-[#333333] transition transform hover:scale-105" onClick={addSquare}>
                        Add square
                    </button>
                    <button className="bg-[#1a1a1a] text-[#4ec9b0] px-10 py-4 rounded-xl font-bold shadow-lg text-2xl
                    hover:bg-[#333333] transition transform hover:scale-105" onClick={clearSquares}>
                        Clear
                    </button>
                </div>
            </div>

            {/* Separator */}
            <div className="h-4"></div>

            {/* Squareing up */}
            <div className="relative w-full h-full bg-[#292929] rounded-xl shadow-lg mt-4">
            {/*<div className="w-full overflow-x-auto h-full flex flex-wrap justify-center items-start p-4 bg-[#292929] rounded-xl shadow-lg mt-4 gap-2">*/}
                { squares.map((square) => (
                    <div
                        key={square.Id}
                        className="w-16 h-16 border border-black rounded-md absolute"
                        style={{
                            backgroundColor: square.Colour || "#ff0000", // Fallback color
                            left: `${square.PositionX * 72}px`,  // Use `left` instead of translate
                            top: `${square.PositionY * 72}px`,   // Use `top` instead of translate
                            transition: "top 0.2s ease-in-out, left 0.2s ease-in-out",
                        }}
                    />
                ))}
            </div>
        </div>
    );
}

export default App;